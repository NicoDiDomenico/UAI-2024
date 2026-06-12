using MindFit_Intelligence_Backend.Models.Master;

namespace MindFit_Intelligence_Backend.Repository.Interfaces
{
    public interface IUsuarioMasterRepository
    {
        Task Add(UsuarioMaster usuario);
        Task Save();
    }
}
