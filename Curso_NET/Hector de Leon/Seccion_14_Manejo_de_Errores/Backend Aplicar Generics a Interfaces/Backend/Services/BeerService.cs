using AutoMapper;
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
        private IMapper _mapper;

        public List<string> Errors { get; }

        public BeerService(
            [FromKeyedServices("beerRepository")] IRepository<Beer> beerRepository,
            IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            // Mio: => (IEnumerable<BeerDto>)await _beerRepository.Get();
            // Profe:
            var beers = await _beerRepository.Get();

            return beers.Select(b => _mapper.Map<BeerDto>(b));
        }

        public async Task<BeerDto?> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);
                return beerDto;
            }
            return null;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            // No seria mejor que instanciar beer ocurra en el Repository? Ya que Repository maneja los Modelos y SErivicios maneja los D
            //// 73. AutoMapper con propiedades con el mismo
            var beer = _mapper.Map<Beer>(beerInsertDto);

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = _mapper.Map<BeerDto>(beer);

            return beerDto;
        }
        public async Task<BeerDto?> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                // 75.AutoMapper con objeto existente
                /* Antes:
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;
                beer.BrandID = beerUpdateDto.BrandID;
                Después: */
                beer = _mapper.Map<BeerUpdateDto, Beer>(beerUpdateDto, beer); // No se modifican los atributos qu el objetoDestino/Existente tenga y el objetoOrigen no.

                _beerRepository.Update(beer);
                await _beerRepository.Save(); 

                var beerDto = _mapper.Map<BeerDto>(beer);

                return beerDto;
            }
            return null;
        }

        public async Task<BeerDto?> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                //// 74.AutoMapper con propiedades con distinto nombre.
                var beerDto = _mapper.Map<BeerDto>(beer);

                _beerRepository.Delete(beer);
                await _beerRepository.Save();

                return beerDto;
            }
            return null;
        }

        /// 78. Método búsqueda en capa Repositorio
        // Acá hacemos validaciones relacioadas a las reglas de negocio
        public bool Validate(BeerInsertDto beerInsertDto)
        {
            if (_beerRepository.Search(b => b.Name == beerInsertDto.Name).Count() > 0)
            {
                Errors.Add("No puede existir una cerveza con un nombre ya existente");
                return false;
            }
            return true;
        }
        public bool Validate(BeerUpdateDto beerUpdateDto)
        {
            if (_beerRepository.Search(b => b.Name == beerUpdateDto.Name 
            && b.BeerID != beerUpdateDto.Id).Count() > 0)
            {
                Errors.Add("No puede existir una cerveza con un nombre ya existente");
                return false;
            }
            return true;
        }
    }
}
