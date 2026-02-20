using Microsoft.EntityFrameworkCore;
using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly MindFitIntelligenceContext _context;

        public GrupoRepository(MindFitIntelligenceContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Grupo>> Get()
        {
            return await _context.Grupos
                .AsNoTracking()
                .Include(g => g.GrupoPermisos)
                    .ThenInclude(gp => gp.Permiso)
                .ToListAsync();
        }

        public async Task<Grupo?> GetById(int id)
        {
            return await _context.Grupos
                .Include(g => g.UsuarioGrupos)
                .Include(g => g.GrupoPermisos)
                    .ThenInclude(gp => gp.Permiso)
                .FirstOrDefaultAsync(g => g.IdGrupo == id);
        }

        public async Task Add(Grupo entity) 
            => await _context.Grupos.AddAsync(entity);

        public void RemoveGrupoPermisos(IEnumerable<GrupoPermiso> items)
        {
            _context.RemoveRange(items);
        }

        public void Update(Grupo entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Grupo entity)
        {
            _context.Grupos.Remove(entity);
        }

        public async Task Save()             
            => await _context.SaveChangesAsync();
    }
}
