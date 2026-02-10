using Microsoft.EntityFrameworkCore;
using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository
{
    public class UsuarioRepository : ICommonRepository<Usuario>
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

        public async Task Add(Usuario entity)
            => await _context.Usuarios.AddAsync(entity);

        public void Update(Usuario entity)
        {
            _context.Usuarios.Attach(entity);
            _context.Usuarios.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Usuario entity)
            => _context.Usuarios.Remove(entity);

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}
