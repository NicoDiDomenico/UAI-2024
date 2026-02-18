using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Services
{
    public interface IUsuarioService
    {
        public Task<Usuario?> GetById(int id);
        public Task<List<UsuarioGridDto>> GetUsuariosGrid();
        public Task<UsuarioDto?> GetUsuarioById(int id);
        public Task<UsuarioDto> Add(UsuarioInsertDto typeInsertDto);
        public Task<UsuarioResponsableDto?> Update(int id, UsuarioResponsableUpdateDto typeUpdateDto);
        public Task<UsuarioResponsableDto?> Delete(int id);
    }
}
