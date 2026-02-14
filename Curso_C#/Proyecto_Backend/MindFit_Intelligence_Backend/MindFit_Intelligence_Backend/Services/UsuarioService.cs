using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.Repository;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.DTOs.Personas;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MindFit_Intelligence_Backend.Services
{
    public class UsuarioService : ICommonService<UsuarioResponsableDto, UsuarioResponsableInsertDto, UsuarioResponsableUpdateDto>
    {
        private IUsuarioRepository _usuarioRepository;
        private IPersonaResponsableRepository _personaResponsableRepository;
        private IMapper _mapper;
        private IAuthService _authService; // Servicio de autenticación (JWT)

        public UsuarioService(
            IAuthService authService,
            IUsuarioRepository usuarioRepository,
            IPersonaResponsableRepository personaResponsableRepository,
            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _personaResponsableRepository = personaResponsableRepository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<IEnumerable<UsuarioResponsableDto>> Get()
        {
            IEnumerable<Usuario> usuarios = await _usuarioRepository.Get();

            IEnumerable<UsuarioResponsableDto> usuariosDto = _mapper.Map<IEnumerable<UsuarioResponsableDto>>(usuarios);

            return usuariosDto;
        }

        public async Task<UsuarioResponsableDto?> GetById(int id)
        {
            Usuario? usuario = await _usuarioRepository.GetById(id);

            if (usuario is null)
                return null;

            UsuarioResponsableDto usuarioDto = _mapper.Map<UsuarioResponsableDto>(usuario);

            return usuarioDto;
        }

        public async Task<UsuarioResponsableDto> Add(UsuarioResponsableInsertDto usuarioResponsableInsertDto)
        {
            Usuario usuarioResponsable = _authService.SetPasswordHash(usuarioResponsableInsertDto);

            PersonaResponsable responsable = _mapper.Map<PersonaResponsable>(usuarioResponsableInsertDto.PersonaResponsableInsertDto);

            // Navegacion Bidreccional para EF sepa que 1..1 y asigne el mimsmo IdUsuario a ambas tablas
            usuarioResponsable.PersonaResponsable = responsable;
            responsable.Usuario = usuarioResponsable;

            await _usuarioRepository.Add(usuarioResponsable);
            await _usuarioRepository.Save();

            UsuarioResponsableDto usuarioResponsableDto = _mapper.Map<UsuarioResponsableDto>(usuarioResponsable);

            return usuarioResponsableDto;
        }

        public async Task<UsuarioResponsableDto?> Update(int id, UsuarioResponsableUpdateDto usuarioResponsableUpdateDto)
        {
            // chequear con FluentValidation que usuarioUpdateDto.PersonaResponsableUpdateDt no sea null
            Usuario? usuario = await _usuarioRepository.GetById(id);
            PersonaResponsable? personaResponsable = await _personaResponsableRepository.GetById(id);

            if (usuario == null || personaResponsable == null)
                return null;

            _mapper.Map(usuarioResponsableUpdateDto, usuario);
            _mapper.Map(usuarioResponsableUpdateDto.PersonaResponsableUpdateDto, personaResponsable);

            await _usuarioRepository.Save();

            UsuarioResponsableDto usuarioResponsableDto = _mapper.Map<UsuarioResponsableDto>(usuario);
            usuarioResponsableDto.PersonaResponsableDto = _mapper.Map<PersonaResponsableDto>(personaResponsable);

            return usuarioResponsableDto;
        }

        public async Task<UsuarioResponsableDto?> Delete(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario != null) 
            {
                _usuarioRepository.Delete(usuario);
                await _usuarioRepository.Save();

                var usuarioDto = _mapper.Map<UsuarioResponsableDto>(usuario);
                return usuarioDto;
            }
            return null;
        }
    }
}
