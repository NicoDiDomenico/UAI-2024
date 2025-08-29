using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface ICommonService <T, TI, TU>
    {
        // 77. Definición de Métodos de validación (2)
        public List<string> Errors { get; }
        //
        public Task<IEnumerable<T>> Get();
        public Task<T?> GetById(int id); // ActionResult no va en el servicio porque es responsabilidad del controlador devolver los códigos HTTP
        public Task<T> Add(TI beerInsertDto);
        public Task<T?> Update(int id, TU beerUpdateDto);
        public Task<T?> Delete(int id);

        // 77. Definición de Métodos de validación (1)
        public bool Validate(TI dto);
        public bool Validate(TU dto);
        //
    }
}
