using MindFitIntelligence_Backend.Models;

namespace MindFitIntelligence_Backend.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Crea un JWT válido para el usuario dado.
        /// </summary>
        /// <param name="user">Entidad Usuario autenticada.</param>
        /// <returns>Token JWT en formato string.</returns>
        string CreateToken(Usuario user);
    }
}