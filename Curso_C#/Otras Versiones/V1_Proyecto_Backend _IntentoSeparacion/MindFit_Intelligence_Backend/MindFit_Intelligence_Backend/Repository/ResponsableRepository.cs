using Microsoft.EntityFrameworkCore;
using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.Repository.Interfaces;

namespace MindFit_Intelligence_Backend.Repository
{
    public class ResponsableRepository : IResponsableRepository
    {
        private readonly MindFitIntelligenceContext _context;

        public ResponsableRepository(MindFitIntelligenceContext context)
        {
            _context = context;
        }

        // Solo los campos necesarios para la grilla: sin grupos ni permisos
        public async Task<List<Usuario>> GetResponsablesGrid()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.PersonaResponsable)
                .Where(u => u.PersonaResponsable != null)
                .ToListAsync();
        }

        // Detalle completo con grupos y permisos del responsable
        public async Task<Usuario?> GetResponsableCompletoById(int id)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.PersonaResponsable)
                .Include(u => u.UsuarioGrupos)
                    .ThenInclude(ug => ug.Grupo)
                        .ThenInclude(g => g.GrupoPermisos)
                            .ThenInclude(gp => gp.Permiso)
                .FirstOrDefaultAsync(u => u.IdUsuario == id && u.PersonaResponsable != null);
        }

        // Entidad trackeada para operaciones de escritura
        public async Task<Usuario?> GetById(int id)
            => await _context.Usuarios.FindAsync(id);

        public async Task Add(Usuario entity)
            => await _context.Usuarios.AddAsync(entity);

        public void Delete(Usuario entity)
            => _context.Usuarios.Remove(entity);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public async Task ReplaceUsuarioGrupos(int idUsuario, List<int> nuevosIdGrupos)
        {
            Usuario? usuario = await _context.Usuarios
                .Include(u => u.UsuarioGrupos)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

            if (usuario == null) return;

            _context.UsuarioGrupos.RemoveRange(usuario.UsuarioGrupos);

            usuario.UsuarioGrupos = nuevosIdGrupos
                .Select(idGrupo => new UsuarioGrupo
                {
                    IdUsuario = idUsuario,
                    IdGrupo = idGrupo
                })
                .ToList();
        }
    }
}
