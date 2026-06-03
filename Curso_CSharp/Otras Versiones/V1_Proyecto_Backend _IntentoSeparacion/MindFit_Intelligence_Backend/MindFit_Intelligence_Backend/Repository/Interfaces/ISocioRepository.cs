using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository.Interfaces
{
    public interface ISocioRepository
    {
        Task<List<Usuario>> GetSociosGrid();
        Task<Usuario?> GetSocioCompletoById(int id);
        Task<Usuario?> GetById(int id);
        Task Add(Usuario entity);
        void Delete(Usuario entity);
        Task Save();
        Task ReplaceUsuarioGrupos(int idUsuario, List<int> nuevosIdGrupos);
    }
}
