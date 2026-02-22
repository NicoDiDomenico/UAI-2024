using Microsoft.EntityFrameworkCore;
using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MindFitIntelligenceContext _context;

        public UsuarioRepository(MindFitIntelligenceContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetById(int id)
        {
            Usuario? usuario = await _context.Usuarios.FindAsync(id);
            return usuario;
        }

        // Aca traigo tanto los Responsables como los Socios
        public async Task<List<Usuario>> GetUsuariosResponsablesYSocios()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.PersonaResponsable)
                .Include(u => u.PersonaSocio)
                // NO incluir UsuarioGrupos
                .ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioDetalleConGruposPermisosById(int id)
        {
            Usuario? usuario = await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.PersonaResponsable)
                .Include(u => u.PersonaSocio)
                .Include(u => u.UsuarioGrupos)
                    .ThenInclude(ug => ug.Grupo)
                        .ThenInclude(g => g.GrupoPermisos)
                            .ThenInclude(gp => gp.Permiso)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            return usuario;
        }

        public async Task Add(Usuario entity)
            => await _context.Usuarios.AddAsync(entity);

        public async Task<Usuario?> GetByUsername(string username)
        {
            return await _context.Usuarios
                    .Include(u => u.UsuarioGrupos)
                        .ThenInclude(ug => ug.Grupo)
                    .FirstOrDefaultAsync(u => u.Username == username);
        }

        // No se que hacer con este metodo porque no lo estoy usando ya que siempre uso el metodo GetById y ya queda el objeto en el contexto para mapearlo y luego guardarlo.
        public void Update(Usuario entity)
        {
            _context.Usuarios.Attach(entity);
            _context.Usuarios.Entry(entity).State = EntityState.Modified;
        }

        public async Task ReplaceUsuarioGrupos(int idUsuario, List<int> nuevosIdGrupos)
        {
            Usuario? usuario = await _context.Usuarios
                .Include(u => u.UsuarioGrupos)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

            if (usuario == null) return;

            // eliminar actuales
            _context.UsuarioGrupos.RemoveRange(usuario.UsuarioGrupos);

            // agregar nuevos
            usuario.UsuarioGrupos = nuevosIdGrupos
                .Select(idGrupo => new UsuarioGrupo
                {
                    IdUsuario = idUsuario,
                    IdGrupo = idGrupo
                })
                .ToList();
        }

        public void Delete(Usuario entity)
            => _context.Usuarios.Remove(entity);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public async Task<Usuario?> GetByPasswordResetTokenHash(string tokenHasheado)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.PasswordResetTokenHash == tokenHasheado);
        }

        public async Task<bool> UsuarioTienePermiso(int idUsuario, string nombrePermiso)
        {
            // Esta consulta busca si existe alguna relación donde:
            // El usuario pertenezca a un grupo y ese grupo tenga el permiso solicitado.
            return await _context.Usuarios
                .Where(u => u.IdUsuario == idUsuario)
                .AnyAsync(u => u.UsuarioGrupos
                    .Any(ug => ug.Grupo.GrupoPermisos
                        .Any(gp => gp.Permiso.Codigo == nombrePermiso)));
        }

        public async Task<List<string>> GetNombresPermisosByUsuario(int idUsuario)
        {
            return await _context.Usuarios
                .Where(u => u.IdUsuario == idUsuario)
                .SelectMany(u => u.UsuarioGrupos)
                .Select(ug => ug.Grupo)
                .SelectMany(g => g.GrupoPermisos)
                .Select(gp => gp.Permiso.Codigo)
                .Distinct()
                .ToListAsync();
        }
    }
}
