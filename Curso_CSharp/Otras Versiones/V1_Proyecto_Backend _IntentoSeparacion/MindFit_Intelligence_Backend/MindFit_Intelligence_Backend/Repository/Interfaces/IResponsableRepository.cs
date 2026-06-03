using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository.Interfaces
{
    public interface IResponsableRepository
    {
        Task<List<Usuario>> GetResponsablesGrid();
        Task<Usuario?> GetResponsableCompletoById(int id);
        Task<Usuario?> GetById(int id);
        Task Add(Usuario entity);
        void Delete(Usuario entity);
        Task Save();
        Task ReplaceUsuarioGrupos(int idUsuario, List<int> nuevosIdGrupos);
    }
}
