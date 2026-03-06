using AutoMapper;
using MindFit_Intelligence_Backend.DTOs.Cuota;
using MindFit_Intelligence_Backend.DTOs.Dia;
using MindFit_Intelligence_Backend.DTOs.Socios;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.Models.Enums;
using MindFit_Intelligence_Backend.Repository.Interfaces;
using MindFit_Intelligence_Backend.Services.Interfaces;

namespace MindFit_Intelligence_Backend.Services
{
    public class SocioService : ISocioService
    {
        private readonly ISocioRepository _socioRepository;
        private readonly IPersonaSocioRepository _personaSocioRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IPersonaSocioService _personaSocioService;
        private readonly IDiaRepository _diaRepository;
        private readonly ICuotaRepository _cuotaRepository;
        public List<string> Errors { get; } = new();

        public SocioService(
            ISocioRepository socioRepository,
            IPersonaSocioRepository personaSocioRepository,
            IMapper mapper,
            IAuthService authService,
            IPersonaSocioService personaSocioService,
            IDiaRepository diaRepository,
            ICuotaRepository cuotaRepository)
        {
            _socioRepository = socioRepository;
            _personaSocioRepository = personaSocioRepository;
            _mapper = mapper;
            _authService = authService;
            _personaSocioService = personaSocioService;
            _diaRepository = diaRepository;
            _cuotaRepository = cuotaRepository;
        }

        public async Task<IEnumerable<DiaDto>> GetDias()
        {
            IEnumerable<Dia> dias = await _diaRepository.GetAll();
            return _mapper.Map<IEnumerable<DiaDto>>(dias);
        }

        public async Task<List<UsuarioGridDto>> GetSociosGrid()
        {
            List<Usuario> socios = await _socioRepository.GetSociosGrid();
            return _mapper.Map<List<UsuarioGridDto>>(socios);
        }

        public async Task<UsuarioDto?> GetSocioById(int id)
        {
            Usuario? usuario = await _socioRepository.GetSocioCompletoById(id);
            if (usuario == null) return null;
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto?> Add(SocioInsertDto dto)
        {
            Errors.Clear();

            Usuario usuario = _mapper.Map<Usuario>(dto);
            _authService.SetPasswordHash(usuario, dto.Password);

            await _personaSocioService.SetSocioNuevoAsync(
                usuario.PersonaSocio!,
                dto.PersonaSocio.DiasActivosIds
            );

            Cuota cuotaInicial = _personaSocioService.CrearCuotaInicial(
                usuario.PersonaSocio!,
                dto.PersonaSocio.Cuota.Plan,
                dto.PersonaSocio.Cuota.Monto
            );

            await _socioRepository.Add(usuario);
            await _socioRepository.Save();

            await _personaSocioService.EnviarEmailBienvenidaAsync(usuario, cuotaInicial);

            return await GetSocioById(usuario.IdUsuario);
        }

        public async Task<UsuarioDto?> Update(int id, SocioUpdateDto dto)
        {
            Errors.Clear();

            Usuario? usuario = await _socioRepository.GetById(id);
            if (usuario == null) return null;

            PersonaSocio? ps = await _personaSocioRepository.GetById(id);
            if (ps == null) return null;

            usuario.PersonaSocio = ps;
            usuario.PersonaResponsable = null;
            ps.Usuario = usuario;

            await _personaSocioService.SetSocioActualizadoAsync(usuario.PersonaSocio, dto.PersonaSocio.DiasActivosIds);

            Cuota? cuotaActualizada = null;
            if (dto.PersonaSocio.Cuota.renueva)
                cuotaActualizada = _personaSocioService.ActualizarCuota(
                    usuario.PersonaSocio,
                    dto.PersonaSocio.Cuota.Plan,
                    dto.PersonaSocio.Cuota.Monto
                );

            _mapper.Map(dto, usuario);

            if (dto.IdGrupos != null)
                await _socioRepository.ReplaceUsuarioGrupos(id, dto.IdGrupos);

            await _socioRepository.Save();

            if (cuotaActualizada != null)
                await _personaSocioService.EnviarEmailBienvenidaAsync(usuario, cuotaActualizada);

            UsuarioDto? usuarioDto = await GetSocioById(id);
            if (usuarioDto == null)
            {
                Errors.Add("Error inesperado al obtener el socio actualizado de la BD");
                return null;
            }

            return usuarioDto;
        }

        public async Task<UsuarioDto?> SoftDeleteSocio(int id)
        {
            PersonaSocio? personaSocio = await _personaSocioRepository.GetById(id);
            if (personaSocio == null) return null;

            personaSocio.EstadoSocio = EstadoSocio.Eliminado;
            await _socioRepository.Save();

            return await GetSocioById(id);
        }

        public async Task<UsuarioDto?> AutoSoftDeleteSocio(int id)
        {
            Cuota? cuotaSocio = await _cuotaRepository.GetUltimaCuotaSocio(id);
            if (cuotaSocio == null) return null;

            if (cuotaSocio.FechaFinPeriodo.AddDays(30) <= DateTime.Now)
            {
                PersonaSocio? personaSocio = await _personaSocioRepository.GetById(id);
                if (personaSocio == null) return null;

                personaSocio.EstadoSocio = EstadoSocio.Eliminado;
                await _socioRepository.Save();

                return await GetSocioById(id);
            }

            return null;
        }

        public async Task<UsuarioDto?> RecoverSoftDeletedSocio(int id)
        {
            PersonaSocio? personaSocio = await _personaSocioRepository.GetById(id);
            if (personaSocio == null) return null;

            personaSocio.EstadoSocio = EstadoSocio.Actualizado;
            await _socioRepository.Save();

            return await GetSocioById(id);
        }

        public async Task<UsuarioDto?> Delete(int id)
        {
            Usuario? usuarioRestore = await _socioRepository.GetSocioCompletoById(id);
            if (usuarioRestore == null) return null;
            UsuarioDto usuarioDto = _mapper.Map<UsuarioDto>(usuarioRestore);

            Usuario? usuarioDelete = await _socioRepository.GetById(id);
            if (usuarioDelete == null) return null;

            _socioRepository.Delete(usuarioDelete);
            await _socioRepository.Save();

            return usuarioDto;
        }

        public async Task<bool> ValidateDelete(int id)
        {
            Errors.Clear();

            IEnumerable<Cuota> cuotas = await _cuotaRepository.GetBySocio(id);
            bool tieneCuotaPagada = cuotas.Any(c => c.EstadoCuota == EstadoCuota.Pagado);

            if (tieneCuotaPagada)
                Errors.Add("No se puede eliminar el socio con la cuota al día");

            return !Errors.Any();
        }

        public async Task<bool> ValidateRecover(int id)
        {
            Errors.Clear();

            PersonaSocio? personaSocio = await _personaSocioRepository.GetById(id);
            if (personaSocio == null)
            {
                Errors.Add("El socio no existe");
                return false;
            }

            if (personaSocio.EstadoSocio != EstadoSocio.Eliminado)
                Errors.Add("El socio no se encuentra en estado Eliminado");

            return !Errors.Any();
        }
    }
}
