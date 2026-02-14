using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository
{
    public interface IUsuarioRepository : ICommonRepository<Usuario>
    {
        Task<Usuario?> GetByUsername(string username);
        Task<Usuario?> GetByPasswordResetTokenHash(string tokenHash);
    }
}
