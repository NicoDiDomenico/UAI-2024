using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository
{
    public interface IGrupoRepository : ICommonRepository<Grupo>
    {
        public void RemoveGrupoPermisos(IEnumerable<GrupoPermiso> items);
    }
}
