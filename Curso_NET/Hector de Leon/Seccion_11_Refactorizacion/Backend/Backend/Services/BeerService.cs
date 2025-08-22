using Backend.DTOs;
using Backend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : IBeerService
    {
        private StoreContext _context;

        public BeerService(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BeerDto>> Get() =>
            await _context.Beers.Select(b => new BeerDto
            {
                Id = b.BeerID,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandID = b.BrandID
            }).ToListAsync();

        public async Task<BeerDto?> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,     // Asignás la PK de la entidad Beer a la propiedad Id del DTO
                    Name = beer.Name,     // Asignás el nombre
                    Alcohol = beer.Alcohol, // Asignás el grado de alcohol
                    BrandID = beer.BrandID // Asignás la clave foránea de la marca
                };
                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            throw new NotImplementedException();
        }

        public async Task<BeerDto> Delete(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<BeerDto> Update(BeerUpdateDto beerUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
