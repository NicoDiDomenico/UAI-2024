using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository.Interfaces
{
    public interface IPersonaResponsableRepository : ICommonRepository<PersonaResponsable>
    {
        Task<PersonaResponsable?> GetByEmail(string email);
    }
}
