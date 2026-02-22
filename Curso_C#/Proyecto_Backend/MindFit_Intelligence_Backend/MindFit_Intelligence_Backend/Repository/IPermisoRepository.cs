using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository
{
    public interface IPermisoRepository
    {
        public Task<IEnumerable<Permiso>> Get();
    }
}
