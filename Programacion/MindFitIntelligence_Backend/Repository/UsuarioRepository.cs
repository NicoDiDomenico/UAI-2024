using MindFitIntelligence_Backend.Models;
using Microsoft.EntityFrameworkCore;
using MindFitIntelligence_Backend.DTOs;

namespace MindFitIntelligence_Backend.Repository
{
    public class UsuarioRepository : IRepository<Usuario>
    {
        private MindFitBDContext _context;
        public UsuarioRepository(MindFitBDContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetById(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario?> GetByUsername(string username)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task Register(Usuario insertUsuario)
        {
            await _context.Usuarios.AddAsync(insertUsuario);
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Attach(usuario);
            _context.Usuarios.Entry(usuario);
        }

        public void Delete(Usuario entity)
        {
            _context.Usuarios.Remove(entity);
        }

        public async Task Save() 
            => await _context.SaveChangesAsync();
    }
}
