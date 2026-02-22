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
        public Task<UsuarioDto?> Update(int id, UsuarioUpdateDto typeUpdateDto);
        public Task<UsuarioDto?> Delete(int id);
        public Task<bool> UsuarioTienePermiso(int idUsuario, string nombrePermiso);
    }
}
