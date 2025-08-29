using MindFitIntelligence_Backend.Models;
using MindFitIntelligence_Backend.Repository;
using MindFitIntelligence_Backend.DTOs;
using AutoMapper;

namespace MindFitIntelligence_Backend.Services
{
    public class UsuarioService : ICommonService<UsuarioDto, InsertUsuarioDto, UpdateUsuarioDto>
    {
        private IRepository<Usuario> _usuarioRepository;
        private IMapper _mapper;
        public List<string> Errors { get; } // No implementado aún

        public UsuarioService(
            [FromKeyedServices("usuarioRepository")] IRepository<Usuario> usuarioRepository,
            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
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

        public async Task<UsuarioDto> Add(InsertUsuarioDto usaurioDto)
        {
            var usaurio = _mapper.Map<Usuario>(usaurioDto);

            await _usuarioRepository.Add(usaurio);
            await _usuarioRepository.Save();

            var beerDto = _mapper.Map<UsuarioDto>(usaurio);

            return beerDto;
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
