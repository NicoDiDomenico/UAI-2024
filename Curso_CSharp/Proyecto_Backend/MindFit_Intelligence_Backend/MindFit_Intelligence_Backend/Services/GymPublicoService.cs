using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MindFit_Intelligence_Backend.DTOs.Gyms;
using MindFit_Intelligence_Backend.Models.Master;
using MindFit_Intelligence_Backend.Repository.Interfaces;
using MindFit_Intelligence_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MindFit_Intelligence_Backend.Services
{
    public class GymPublicoService : IGymPublicoService
    {
        private readonly IGymRepository _gymRepository;
        private readonly IUsuarioMasterRepository _usuarioMasterRepo;
        private readonly IConfiguration _configuration;

        public GymPublicoService(
            IGymRepository gymRepository,
            IUsuarioMasterRepository usuarioMasterRepo,
            IConfiguration configuration)
        {
            _gymRepository = gymRepository;
            _usuarioMasterRepo = usuarioMasterRepo;
            _configuration = configuration;
        }

        public async Task<IEnumerable<GymPublicoDTO>> GetAllGymsActivosAsync()
        {
            return await _gymRepository.GetAllGymsActivosAsync();
        }

        public async Task<int> RegistrarNuevoGymAsync(NuevoGymRequestDto dto)
        {
            // 1. Preparar el ConnectionString
            string nombreDbSugerido = "MindFit_" + dto.NombreGym.Replace(" ", "");
            var template = _configuration.GetConnectionString("TenantTemplate")
                          ?? throw new InvalidOperationException("TenantTemplate no configurado.");
            string connString = string.Format(template, nombreDbSugerido);

            // 2. Mapear y Guardar el Gimnasio
            var nuevoGym = new Gym
            {
                NombreGym = dto.NombreGym,
                ConnectionString = connString,
                Activo = false,
                FechaCreacion = DateTime.UtcNow
            };

            var idGym = await _gymRepository.AddGymAsync(nuevoGym);

            // 3. Mapear la Persona (Los datos del dueño)
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

            // 4. Mapear el Usuario y vincularlo con la Persona y el Gym
            var usuarioDto = dto.UsuarioMaster;
            var usuario = new UsuarioMaster
            {
                Username = usuarioDto.Username,
                IdGym = idGym, // Lo vinculamos al gym que acabamos de crear
                FechaRegistro = DateTime.UtcNow,
                PersonaResponsableMaster = persona // ACÁ ESTÁ LA MAGIA DE EF CORE
            };

            // Hasheamos la contraseña usando TU estilo (igual que en AuthService)
            usuario.PasswordHash = new PasswordHasher<UsuarioMaster>()
                .HashPassword(usuario, usuarioDto.Password);

            // 5. Guardar en la Base Maestra
            try
            {
                await _usuarioMasterRepo.Add(usuario);
                await _usuarioMasterRepo.Save();
                return idGym;
            }
            catch (Exception ex)
            {
                // Recomendado: eliminar gym creado o manejar compensación si lo considerás necesario.
                throw new InvalidOperationException("Error al crear el usuario maestro asociado al gym.", ex);
            }
        }
    }
}