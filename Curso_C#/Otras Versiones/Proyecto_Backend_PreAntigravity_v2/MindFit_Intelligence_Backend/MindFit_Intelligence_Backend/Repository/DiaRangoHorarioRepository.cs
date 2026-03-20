using Microsoft.EntityFrameworkCore;
using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.Repository.Interfaces;

namespace MindFit_Intelligence_Backend.Repository
{
    public class DiaRangoHorarioRepository : IDiaRangoHorarioRepository
    {
        private readonly MindFitIntelligenceContext _context;

        public DiaRangoHorarioRepository(MindFitIntelligenceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DiaRangoHorario>> GetAll()
        {
            return await _context.DiaRangosHorarios
                .Include(drh => drh.Dia)
                .Include(drh => drh.RangoHorario)
                .Include(drh => drh.DiaRangoHorarioResponsables)
                    .ThenInclude(r => r.PersonaResponsable)
                .ToListAsync();
        }

        public async Task<DiaRangoHorario?> GetById(int id)
        {
            return await _context.DiaRangosHorarios
                .Include(drh => drh.Dia)
                .FirstOrDefaultAsync(drh => drh.IdDiaRangoHorario == id);
        }

        public async Task<DiaRangoHorarioResponsable?> GetResponsable(int idDiaRangoHorario, int idUsuarioResponsable)
        {
            return await _context.DiaRangosHorariosResponsables
                .FirstOrDefaultAsync(r => r.IdDiaRangoHorario == idDiaRangoHorario && r.IdUsuarioResponsable == idUsuarioResponsable);
        }

        public async Task AddResponsable(DiaRangoHorarioResponsable responsable)
            => await _context.DiaRangosHorariosResponsables.AddAsync(responsable);

        public Task RemoveResponsable(DiaRangoHorarioResponsable responsable)
        {
            _context.DiaRangosHorariosResponsables.Remove(responsable);
            return Task.CompletedTask;
        }

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}
