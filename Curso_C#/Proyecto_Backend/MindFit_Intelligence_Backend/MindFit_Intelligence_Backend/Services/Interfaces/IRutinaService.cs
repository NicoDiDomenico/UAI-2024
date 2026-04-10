using MindFit_Intelligence_Backend.DTOs.Rutina;

namespace MindFit_Intelligence_Backend.Services.Interfaces
{
    public interface IRutinaService
    {
        Task<RutinaDto?> GetRutinaPorSocioYDia(int idUsuarioSocio, DateTime? fecha);
    }
}
