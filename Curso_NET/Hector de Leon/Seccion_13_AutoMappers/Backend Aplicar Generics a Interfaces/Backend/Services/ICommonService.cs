using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface ICommonService <T, TI, TU>
    {
        public Task<IEnumerable<T>> Get();
        public Task<T?> GetById(int id); // ActionResult no va en el servicio porque es responsabilidad del controlador devolver los códigos HTTP
        Task<T> Add(TI beerInsertDto);
        Task<T?> Update(int id, TU beerUpdateDto);
        Task<T?> Delete(int id);
    }
}
