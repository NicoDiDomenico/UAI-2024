using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface ICommonService <T, TI, TU>
    {
        public Task<IEnumerable<T>> Get();
        public Task<T?> GetById(int id); 
        Task<T> Add(TI createDto);
        Task<T?> Update(int id, TU updateDto);
        Task<T?> Delete(int id);
    }
}
