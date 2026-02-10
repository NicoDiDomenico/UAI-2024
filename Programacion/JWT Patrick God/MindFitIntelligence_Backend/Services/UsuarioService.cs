using MindFitIntelligence_Backend.Models;
using MindFitIntelligence_Backend.Repository;
using MindFitIntelligence_Backend.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MindFitIntelligence_Backend.Services
{
    public class UsuarioService : ICommonService<UsuarioDto, InsertUsuarioDto, UpdateUsuarioDto, LoginUsuarioDto>
    {
        private IRepository<Usuario> _usuarioRepository;
        private IMapper _mapper;
        private IAuthService _authService; // Servicio de autenticación (JWT)
        public List<string> Errors { get; } // No implementado aún

        public UsuarioService(
            [FromKeyedServices("authService")] IAuthService authService,
            [FromKeyedServices("usuarioRepository")] IRepository<Usuario> usuarioRepository,
            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _authService = authService;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<UsuarioDto>> GetAll()
        {
            var usaurios = await _usuarioRepository.GetAll();

            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usaurios);

            return usuariosDto;
        }

        public async Task<UsuarioDto?> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario is null)
                return null;

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return usuarioDto;
        }

        public async Task<UsuarioDto> Register(InsertUsuarioDto insertUsuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(insertUsuarioDto);

            usuario.PasswordHash = new PasswordHasher<Usuario>()
                .HashPassword(usuario, insertUsuarioDto.Password);

            await _usuarioRepository.Register(usuario);
            await _usuarioRepository.Save();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return usuarioDto;
        }

        public async Task<string?> Login(LoginUsuarioDto loginUsuarioDto)
        {
            // Busco el usuario por username
            var usuario = await _usuarioRepository.GetByUsername(loginUsuarioDto.Username);

            if (usuario == null)
                return null; // usuario no existe

            // Verifico la contraseña
            var result = new PasswordHasher<Usuario>()
                .VerifyHashedPassword(usuario, usuario.PasswordHash, loginUsuarioDto.Password);

            if (result == PasswordVerificationResult.Failed)
                return null; // contraseña incorrecta

            // Usando JWT
            string token = _authService.CreateToken(usuario);

            return token;
        }

        public async Task<UsuarioDto?> Update(int id, UpdateUsuarioDto updateUsuarioDto)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario != null)
            {
                usuario = _mapper.Map(updateUsuarioDto, usuario);

                _usuarioRepository.Update(usuario);
                await _usuarioRepository.Save();

                var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
                return usuarioDto;
            }
            return null;
        }

        public async Task<UsuarioDto?> Delete(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario != null) 
            {
                _usuarioRepository.Delete(usuario);
                await _usuarioRepository.Save();

                var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
                return usuarioDto;
            }
            return null;
        }

        public bool IsNull(UsuarioDto? usuarioDto) // Según chatgpt no va eso aca porque no es un error de negocio
        {
            if (usuarioDto == null)
            {
                Errors.Add("No se encontró un usuario");
                return true;
            }
            return false;
        }
    }
}
