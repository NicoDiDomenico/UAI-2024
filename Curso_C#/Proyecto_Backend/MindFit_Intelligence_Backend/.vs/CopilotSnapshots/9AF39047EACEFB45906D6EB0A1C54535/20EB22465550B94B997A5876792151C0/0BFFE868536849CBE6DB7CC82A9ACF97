using MindFit_Intelligence_Backend.DTOs.Turno;

namespace MindFit_Intelligence_Backend.Services.Interfaces
{
    public interface ITurnoService
    {
        List<string> Errors { get; }
        Task<IEnumerable<TurnoDto>> GetTurnosByIdUsuarioSocio(int idUsuarioSocio);
        Task<TurnoDto?> RegistrarTurno(TurnoInsertDto turnoInsertDto);
        Task<bool> ValidateAsync(TurnoInsertDto dto);
    }
}

