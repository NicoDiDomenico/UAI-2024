using Microsoft.EntityFrameworkCore;
using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.Repository.Interfaces;

namespace MindFit_Intelligence_Backend.Repository
{
    public class SocioRepository : ISocioRepository
    {
        private readonly MindFitIntelligenceContext _context;

        public SocioRepository(MindFitIntelligenceContext context)
        {
            _context = context;
        }

        // Solo los campos necesarios para la grilla: sin Rutinas, Cuotas ni PerfilIA
        public async Task<List<Usuario>> GetSociosGrid()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.PersonaSocio)
                .Where(u => u.PersonaSocio != null)
                .ToListAsync();
        }

        // Detalle completo con todas las relaciones del socio
        public async Task<Usuario?> GetSocioCompletoById(int id)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.PersonaSocio)
                    .ThenInclude(ps => ps!.Rutinas)
                .Include(u => u.PersonaSocio)
                    .ThenInclude(ps => ps!.PerfilIA)
                .Include(u => u.PersonaSocio)
                    .ThenInclude(ps => ps!.Cuotas)
                .Include(u => u.UsuarioGrupos)
                    .ThenInclude(ug => ug.Grupo)
                        .ThenInclude(g => g.GrupoPermisos)
                            .ThenInclude(gp => gp.Permiso)
                .FirstOrDefaultAsync(u => u.IdUsuario == id && u.PersonaSocio != null);
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
