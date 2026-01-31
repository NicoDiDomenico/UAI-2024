using Backend.DTOs;
using Backend.Modelos;

using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class BeerRepository : IRepository<Beer>
    {
        private StoreContext _context;
        public BeerRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> Get() 
            => await _context.Beers.ToListAsync();

        public async Task<Beer?> GetById(int id)
            => await _context.Beers.FindAsync(id);

        public async Task Add(Beer beer)
            => await _context.Beers.AddAsync(beer);

        public void Update(Beer beer)
        {
            _context.Beers.Attach(beer); // 1. Le dice a EF Core: “Este objeto beer ya existe en la base” (Attach). No hacia falta usalo porque beer ya estaba siendo trackeado por el contexto cuando hice GetById().
            _context.Beers.Entry(beer).State = EntityState.Modified; // 2. Marca la entidad como Modified, lo que significa: “Todas las propiedades han cambiado, actualízalas en la base”.
            // 3. Ojo: todavía no guarda en la base. Recién se aplica cuando haces SaveChangesAsync().
        }
        public void Delete(Beer entity) // No existe un RemoveAsync(), por eso no lo trabajamos con Task
            => _context.Beers.Remove(entity);

        public async Task Save()
            => await _context.SaveChangesAsync();

    }
}
