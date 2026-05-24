using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Repository.Interfaces
{
    public interface IDiaRangoHorarioRepository
    {
        Task<IEnumerable<DiaRangoHorario>> GetAll();
        Task<DiaRangoHorario?> GetById(int id);
        Task<DiaRangoHorarioResponsable?> GetResponsable(int idDiaRangoHorario, int idUsuarioResponsable);
        Task AddResponsable(DiaRangoHorarioResponsable responsable);
        Task RemoveResponsable(DiaRangoHorarioResponsable responsable);
        Task Save();
    }
}
