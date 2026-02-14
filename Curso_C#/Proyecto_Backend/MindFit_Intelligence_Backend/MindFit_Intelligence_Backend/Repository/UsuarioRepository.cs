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

        public async Task<IEnumerable<Usuario>> Get()
        {
            IEnumerable<Usuario> usuario = await _context.Usuarios.ToListAsync();
            return usuario;
        }

        public async Task<Usuario?> GetById(int id)
        {
            Usuario? usuario = await _context.Usuarios.FindAsync(id);
            return usuario;
        }
        public async Task<Usuario?> GetByUsername(string username)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task Add(Usuario entity)
            => await _context.Usuarios.AddAsync(entity);

        // No se que hacer con este metodo porque no lo estoy usando ya que siempre uso el metodo GetById y ya queda el objeto en el contexto para mapearlo y luego guardarlo.
        public void Update(Usuario entity)
        {
            _context.Usuarios.Attach(entity);
            _context.Usuarios.Entry(entity).State = EntityState.Modified;
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
    }
}
