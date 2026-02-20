using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository
{
    public interface IPersonaSocioRepository
    {
        public Task<PersonaSocio?> GetById(int id);
        public Task<PersonaSocio?> GetByEmail(string email);
    }
}
