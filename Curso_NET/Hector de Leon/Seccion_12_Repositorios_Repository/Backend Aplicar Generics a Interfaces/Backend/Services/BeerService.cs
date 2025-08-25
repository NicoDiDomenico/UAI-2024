using Backend.DTOs;
using Backend.Modelos;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private IRepository<Beer> _beerRepository;

        public BeerService(
            [FromKeyedServices("beerRepository")] IRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            // Mio: => (IEnumerable<BeerDto>)await _beerRepository.Get();
            // Profe:
            var beers = await _beerRepository.Get();

            return beers.Select(x => new BeerDto() 
            { 
                Id =  x.BeerID,
                Name = x.Name,
                BrandID = x.BrandID,
                Alcohol = x.Alcohol,
            });
        }

        public async Task<BeerDto?> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

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
            // No seria mejor que instanciar beer ocurra en el Repository? Ya que Repository maneja los Modelos y SErivicios maneja los D
            var beer = new Beer
            {
                Name = beerInsertDto.Name,
                Alcohol = beerInsertDto.Alcohol,
                BrandID = beerInsertDto.BrandID
            };

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };

            return beerDto;
        }
        public async Task<BeerDto?> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;
                beer.BrandID = beerUpdateDto.BrandID;

                _beerRepository.Update(beer);
                await _beerRepository.Save(); 

                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };

                return beerDto;
            }
            return null;
        }

        public async Task<BeerDto?> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };

                _beerRepository.Delete(beer);
                await _beerRepository.Save();

                return beerDto;
            }
            return null;
        }
    }
}
