using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MindFit_Intelligence_Backend.DTOs.Grupos;
using MindFit_Intelligence_Backend.DTOs.Permisos;
using MindFit_Intelligence_Backend.DTOs.Personas;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.Repository;
using System.Collections.Generic;

namespace MindFit_Intelligence_Backend.Services
{
    public class UsuarioService : IUsuarioService
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

        public async Task<Usuario?> GetById(int id)
        {
            return await _usuarioRepository.GetById(id);
        }

        // Acá el front obtiene info esencial para la grilla de usuarios, después con el método GetUsuarioResponsableById obtiene el detalle completo del usuario responsable seleccionado
        public async Task<List<UsuarioGridDto>> GetUsuariosGrid()
        {
            List<Usuario> usuarios = await _usuarioRepository.GetUsuariosResponsablesYSocios();

            List < UsuarioGridDto > usuariosGridDto = _mapper.Map<List<UsuarioGridDto>>(usuarios);

            return usuariosGridDto;
        }

        // Con este metodo el front obtiene toda la info del usuario responsable cuando lo selecciona de la grilla
        public async Task<UsuarioDto?> GetUsuarioById(int id)
        {
            Usuario? usuario = await _usuarioRepository.GetUsuarioDetalleConGruposPermisosById(id);
            if (usuario is null) return null;

            // AutoMapper se encarga de recorrer los objetos y colecciones anidadas
            UsuarioDto usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return usuarioDto;
        }

        public async Task<UsuarioDto> Add(UsuarioInsertDto dto)
        {
            // 1) TipoPersona válido
            if (dto.TipoPersona != "Responsable" && dto.TipoPersona != "Socio")
                throw new Exception("TipoPersona inválido");

            // 2) Exactamente una persona
            bool tieneResp = dto.PersonaResponsable != null;
            bool tieneSocio = dto.PersonaSocio != null;

            if (!tieneResp && !tieneSocio)
                throw new Exception("Debe venir PersonaResponsable o PersonaSocio");

            if (tieneResp && tieneSocio)
                throw new Exception("No puede tener ambas personas");

            // 3) Coherencia con TipoPersona
            if (dto.TipoPersona == "Responsable" && !tieneResp)
                throw new Exception("Falta PersonaResponsable");

            if (dto.TipoPersona == "Socio" && !tieneSocio)
                throw new Exception("Falta PersonaSocio");

            Usuario usuario = _mapper.Map<Usuario>(dto);

            _authService.SetPasswordHash(usuario, dto);

            await _usuarioRepository.Add(usuario);
            await _usuarioRepository.Save();

            UsuarioDto? usuarioDto = await GetUsuarioById(usuario.IdUsuario);

            if (usuarioDto == null)
                throw new Exception("Error al obtener el usuario creado");

            return usuarioDto!;
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
