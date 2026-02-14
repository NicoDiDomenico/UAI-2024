using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository
{
    public interface IPersonaResponsableRepository : ICommonRepository<PersonaResponsable>
    {
        Task<PersonaResponsable?> GetByEmail(string email);
    }
}
