using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MindFit_Intelligence_Backend.DTOs.Gyms;
using MindFit_Intelligence_Backend.Models.Master;
using MindFit_Intelligence_Backend.Repository.Interfaces;
using MindFit_Intelligence_Backend.Services.Interfaces;
using System.Globalization;
using System.Text;

namespace MindFit_Intelligence_Backend.Services
{
    public class GymPublicoService : IGymPublicoService
    {
        private readonly IGymRepository _gymRepository;
        private readonly IUsuarioMasterRepository _usuarioMasterRepo;
        private readonly IConfiguration _configuration;
        private readonly MindFitMasterContext _masterContext;
        private readonly TenantProvisioningService _tenantProvisioningService;
        private readonly ILogger<GymPublicoService> _logger;

        public GymPublicoService(
            IGymRepository gymRepository,
            IUsuarioMasterRepository usuarioMasterRepo,
            IConfiguration configuration,
            MindFitMasterContext masterContext,
            TenantProvisioningService tenantProvisioningService,
            ILogger<GymPublicoService> logger)
        {
            _gymRepository = gymRepository;
            _usuarioMasterRepo = usuarioMasterRepo;
            _configuration = configuration;
            _masterContext = masterContext;
            _tenantProvisioningService = tenantProvisioningService;
            _logger = logger;
        }

        public async Task<IEnumerable<GymPublicoDTO>> GetAllGymsActivosAsync()
        {
            return await _gymRepository.GetAllGymsActivosAsync();
        }

        public async Task<int> RegistrarNuevoGymAsync(NuevoGymRequestDto dto)
        {
            string nombreGymNormalizado = NormalizarNombreGym(dto.NombreGym);

            if (await _gymRepository.ExistsByNombreAsync(nombreGymNormalizado))
                throw new GymOnboardingConflictException("Ya existe un gimnasio registrado con ese nombre.");

            if (await _usuarioMasterRepo.ExistsByUsernameAsync(dto.UsuarioMaster.Username))
                throw new GymOnboardingConflictException("El nombre de usuario ya se encuentra registrado.");

            string nombreBaseDatos = CrearNombreBaseDatos(nombreGymNormalizado);
            var template = _configuration.GetConnectionString("TenantTemplate")
                ?? throw new InvalidOperationException("TenantTemplate no configurado.");
            string connectionString = string.Format(template, nombreBaseDatos);

            var nuevoGym = new Gym
            {
                NombreGym = nombreGymNormalizado,
                ConnectionString = connectionString,
                Activo = false,
                FechaCreacion = DateTime.UtcNow
            };

            var personaDto = dto.UsuarioMaster.PersonaResponsable;
            var persona = new PersonaResponsableMaster
            {
                Nombre = personaDto.Nombre,
                Apellido = personaDto.Apellido,
                Email = personaDto.Email,
                Telefono = personaDto.Telefono,
                Direccion = personaDto.Direccion,
                Ciudad = personaDto.Ciudad,
                TipoDocumento = personaDto.TipoDocumento,
                NroDocumento = personaDto.NroDocumento,
                Genero = personaDto.Genero,
                FechaNacimiento = personaDto.FechaNacimiento
            };

            var usuarioDto = dto.UsuarioMaster;
            var usuario = new UsuarioMaster
            {
                Username = usuarioDto.Username,
                Gym = nuevoGym,
                FechaRegistro = DateTime.UtcNow,
                PersonaResponsableMaster = persona
            };

            usuario.PasswordHash = new PasswordHasher<UsuarioMaster>()
                .HashPassword(usuario, usuarioDto.Password);

            int idGym;
            await using var transaction = await _masterContext.Database.BeginTransactionAsync();

            try
            {
                idGym = await _gymRepository.AddGymAsync(nuevoGym);
                await _usuarioMasterRepo.Add(usuario);
                await _usuarioMasterRepo.Save();

                await transaction.CommitAsync();
            }
            catch (DbUpdateException ex) when (EsViolacionDeUnicidad(ex))
            {
                await transaction.RollbackAsync();
                throw new GymOnboardingConflictException(
                    "Ya existe un gimnasio o nombre de usuario con esos datos.",
                    ex);
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException(
                    "Error al guardar el gimnasio y su usuario maestro.",
                    ex);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException(
                    "Error al registrar el gimnasio en la base maestra.",
                    ex);
            }

            try
            {
                await _tenantProvisioningService.CrearYAplicarMigracionesAsync(connectionString);
                await _tenantProvisioningService.InicializarDatosYAdministradorAsync(
                    connectionString,
                    dto.UsuarioMaster);
                await _gymRepository.ActualizarActivoAsync(idGym, true);

                _logger.LogInformation(
                    "Onboarding completado y gimnasio {IdGym} activado.",
                    idGym);

                return idGym;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Fallo el aprovisionamiento del gimnasio {IdGym}. El gimnasio permanece inactivo.",
                    idGym);

                throw new InvalidOperationException(
                    "No se pudo crear y preparar la base del gimnasio. El gimnasio permanece inactivo.",
                    ex);
            }
        }

        private static string NormalizarNombreGym(string nombreGym)
        {
            if (string.IsNullOrWhiteSpace(nombreGym))
                throw new ArgumentException("El nombre del gimnasio es obligatorio.", nameof(nombreGym));

            string nombreNormalizado = string.Join(' ', nombreGym
                .Trim()
                .Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));

            if (nombreNormalizado.Length > 150)
                throw new ArgumentException(
                    "El nombre del gimnasio no puede superar los 150 caracteres.",
                    nameof(nombreGym));

            return nombreNormalizado;
        }

        private static string CrearNombreBaseDatos(string nombreGym)
        {
            string sinDiacriticos = nombreGym.Normalize(NormalizationForm.FormD);
            string slug = new string(sinDiacriticos
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .Where(char.IsLetterOrDigit)
                .ToArray());

            if (string.IsNullOrEmpty(slug))
                slug = "Gym";

            slug = slug.Length > 80 ? slug[..80] : slug;
            string sufijoUnico = Guid.NewGuid().ToString("N")[..8];

            return $"MindFit_{slug}_{sufijoUnico}";
        }

        private static bool EsViolacionDeUnicidad(DbUpdateException exception)
        {
            return exception.InnerException is SqlException sqlException
                && sqlException.Number is 2601 or 2627;
        }
    }
}
