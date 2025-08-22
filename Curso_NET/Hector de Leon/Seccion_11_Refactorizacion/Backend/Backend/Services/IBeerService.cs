using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface IBeerService
    {
        public Task<IEnumerable<BeerDto>> Get();
        public Task<BeerDto?> GetById(int id); // ActionResult no va en el servicio porque es responsabilidad del controlador devolver los códigos HTTP
        Task<BeerDto> Add(BeerInsertDto beerInsertDto);
        Task<BeerDto?> Update(int id, BeerUpdateDto beerUpdateDto);
        Task<BeerDto?> Delete(int id);
    }
}
