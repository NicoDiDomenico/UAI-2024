using MindFit_Intelligence_Backend.DTOs.Permisos;

namespace MindFit_Intelligence_Backend.Services
{
    public interface IPermisoService
    {
        public Task<IEnumerable<PermisoDto>> Get();
    }
}
