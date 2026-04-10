using Microsoft.AspNetCore.Mvc;
using MindFit_Intelligence_Backend.DTOs.Personas;
using MindFit_Intelligence_Backend.DTOs.Rutina;
using MindFit_Intelligence_Backend.Services.Interfaces;

namespace MindFit_Intelligence_Backend.Controllers
{
    // Modulo Gestionar Rutinas
    [Route("api/[controller]")]
    [ApiController]
    public class RutinaController : ControllerBase
    {
        private readonly IPersonaResponsableService _PersonaResponsableService;
        private readonly IPersonaSocioService _PersonaSocioService;
        private readonly IRutinaService _RutinaService;

        public RutinaController(
            IPersonaResponsableService PersonaResponsableService,
            IPersonaSocioService PersonaSocioService,
            IRutinaService RutinaService)
        {
            _PersonaResponsableService = PersonaResponsableService;
            _PersonaSocioService = PersonaSocioService;
            _RutinaService = RutinaService;
        }

        // CUD10 – Gestionar Rutina
        // Chequeado --> Anda bien
        // Front: Trae entrenadores asignados al rango horario del día actual.
        //[Authorize]
        [HttpGet("entrenadores/{idRangoHorario}")]
        public async Task<ActionResult<IEnumerable<EntrenadorDto>>> ObtenerEntrenadoresPorHorario(int idRangoHorario)
        {
            var entrenadores = await _PersonaResponsableService.GetEntrenadoresPorHorario(idRangoHorario);
            return Ok(entrenadores);
        }

        // CUD10 – Gestionar Rutina
        // Chequeado --> Anda bien
        // Front: Trae socios con turno para hoy del entrenador y rango horario seleccionados.
        //[Authorize]
        [HttpGet("entrenadores/{idUsuarioResponsable}/socios/{idRangoHorario}")]
        public async Task<ActionResult<IEnumerable<SocioTurnoDto>>> ObtenerSociosConTurnoHoy(int idUsuarioResponsable, int idRangoHorario)
        {
            var socios = await _PersonaSocioService
                .GetSociosConTurnoHoyPorEntrenadorYHorario(idUsuarioResponsable, idRangoHorario);

            return Ok(socios);
        }

        // CUD10 – Gestionar Rutina y CUD11 – Consultar Rutina
        // Chequeado --> Anda bien
        // Front: Trae la rutina de un socio para un día específico (o el día actual si no se especifica fecha).
        //[Authorize]
        [HttpGet("socios/{idUsuarioSocio}/rutinas")]
        public async Task<ActionResult<RutinaDto>> ObtenerRutinaPorSocioYDia(int idUsuarioSocio, [FromQuery] DateTime? fecha)
        {
            var rutina = await _RutinaService.GetRutinaPorSocioYDia(idUsuarioSocio, fecha);

            return rutina == null
                ? NotFound()
                : Ok(rutina);
        }
    }
}
