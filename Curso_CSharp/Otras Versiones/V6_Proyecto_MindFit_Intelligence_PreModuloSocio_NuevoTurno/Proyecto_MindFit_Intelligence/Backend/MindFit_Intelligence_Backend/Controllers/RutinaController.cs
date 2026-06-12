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

        // CUD11 – Consultar Rutina
        // Chequeado --> Anda bien
        // Front: Trae la rutina de un socio para un día específico por IdDia (o el día actual si no se especifica IdDia).
        //[Authorize]
        [HttpGet("socios/{idUsuarioSocio}/rutinas")]
        public async Task<ActionResult<RutinaDto>> ObtenerRutinaPorSocioYDia(int idUsuarioSocio, [FromQuery] int? idDia)
        {
            var rutina = await _RutinaService.GetRutinaPorSocioYDia(idUsuarioSocio, idDia);

            return rutina == null
                ? NotFound("El Socio no asiste este dia")
                : Ok(rutina);
        }

        // CUD12 – Agregar Rutina Y CUD13 – Modificar Rutina.
        // Chequeado --> Anda bien
        // Front: Guarda todos los bloques (3 BLOQUES = 3 Paneles de Calentamiento, Entrenamiento y Estiramiento) de una rutina en una sola operación (replace-all).
        //[Authorize(Policy = "EditarRutina")]
        [HttpPut("{idRutina}/bloques")]
        public async Task<ActionResult<RutinaDto>> GuardarBloquesRutina(int idRutina, [FromBody] RutinaBloquesUpdateDto bloquesDto)
        {
            var rutinaActualizada = await _RutinaService.ReemplazarBloquesRutina(idRutina, bloquesDto);

            return rutinaActualizada == null
                ? NotFound("No existe una rutina activa con ese IdRutina")
                : Ok(rutinaActualizada);
        }

        // CUD14 – Ver Historial de Rutinas --> Consultar versiones del Historial de la Rutina
        // Front: Lista de versiones históricas guardadas automáticamente al modificar bloques.
        //[Authorize(Policy = "VerHistorialRutina")]
        [HttpGet("{idRutina}/historial")]
        public async Task<ActionResult<IEnumerable<RutinaHistorialResumenDto>>> ObtenerHistorialPorRutina(int idRutina)
        {
            var historial = await _RutinaService.GetHistorialByRutina(idRutina);
            return Ok(historial);
        }

        // CUD14 – Ver Historial de Rutinas –-> Consultar una version del Historial de la Rutina
        // Front: Detalle de una versión puntual del historial.
        //[Authorize(Policy = "VerHistorialRutina")]
        [HttpGet("{idRutina}/historial/{idRutinaHistorial}")]
        public async Task<ActionResult<RutinaHistorialDetalleDto>> ObtenerDetalleHistorial(int idRutina, int idRutinaHistorial)
        {
            var historial = await _RutinaService.GetHistorialDetalle(idRutina, idRutinaHistorial);

            return historial == null
                ? NotFound("No existe esa versión de historial para la rutina indicada")
                : Ok(historial);
        }

        // CUD14 – Ver Historial de Rutinas –-> Restaurar Rutina desde Historial
        // Front: Reemplaza la rutina actual por una versión histórica seleccionada.
        //[Authorize(Policy = "RecuperarRutina")]
        [HttpPost("{idRutina}/historial/{idRutinaHistorial}/restaurar")]
        public async Task<ActionResult<RutinaDto>> RestaurarDesdeHistorial(int idRutina, int idRutinaHistorial)
        {
            var rutinaRestaurada = await _RutinaService.RestaurarRutinaDesdeHistorial(idRutina, idRutinaHistorial);

            return rutinaRestaurada == null
                ? NotFound("No existe la rutina o la versión histórica solicitada")
                : Ok(rutinaRestaurada);
        }

        // CUD15 – Eliminar Rutina
        // Front: El mismo endpoint activa o desactiva la rutina según el valor enviado en Activo.
        //[Authorize(Policy = "EliminarRutina")]
        [HttpPatch("{idRutina}/estado")]
        public async Task<ActionResult<object>> CambiarEstadoRutina(int idRutina, [FromBody] RutinaEstadoUpdateDto estadoDto)
        {
            var rutinaActualizada = await _RutinaService.CambiarEstadoRutina(idRutina, estadoDto.Activo);
            if (rutinaActualizada == null)
            {
                return NotFound("No existe una rutina con ese IdRutina");
            }

            string mensaje = estadoDto.Activo
                ? "La rutina ha sido activada correctamente."
                : "La rutina ha sido eliminada correctamente.";

            return Ok(new
            {
                Mensaje = mensaje,
                Rutina = rutinaActualizada
            });
        }

    }
}
