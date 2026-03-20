using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<Usuario?> GetByUsername(string username);
        public Task<Usuario?> GetByPasswordResetTokenHash(string tokenHash);

        public Task<Usuario?> GetById(int id);
        public Task<List<Usuario>> GetUsuariosResponsablesYSocios();
        public Task<Usuario?> GetUsuarioDetalleConGruposPermisosById(int id);
        public Task Add(Usuario entity);
        public void Update(Usuario entity);
        public void Delete(Usuario entity);
        public Task Save();
        public Task ReplaceUsuarioGrupos(int idUsuario, List<int> nuevosIdGrupos);
        public Task<bool> UsuarioTienePermiso(int idUsuario, string nombrePermiso);
        public Task<List<string>> GetNombresPermisosByUsuario(int idUsuario);
    }
}
