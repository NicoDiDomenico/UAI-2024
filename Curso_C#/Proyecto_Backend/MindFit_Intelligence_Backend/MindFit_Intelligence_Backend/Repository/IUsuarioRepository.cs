using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository
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
    }
}
