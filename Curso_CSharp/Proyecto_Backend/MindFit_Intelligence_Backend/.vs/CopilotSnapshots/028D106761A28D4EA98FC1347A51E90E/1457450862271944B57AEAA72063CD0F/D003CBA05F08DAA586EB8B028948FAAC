using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindFit_Intelligence_Backend.DTOs.Turno;
using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.Services.Interfaces;

namespace MindFit_Intelligence_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoService _turnoService;

        public TurnoController(ITurnoService turnoService)
        {
            _turnoService = turnoService;
        }

        // CUD06 - Gestionar Turno
        // Front: Se trae una grilla de historial de los turnos del Socio seleccionado. Alli esta la opcion del boton de reservar turno 
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurnoDto>>> GetTurnos(int idUsuarioSocio)
        {
            var turnos = await _turnoService.GetTurnosByIdUsuarioSocio(idUsuarioSocio);

            return (turnos is null) ? NotFound() : Ok(turnos);
        }

        /* 
         CUD07 - Agregar Turno
         Front: Se apreta el boton de reservar turno, para armar el dto se necesita:
         IdDiaRangoHorario -> se consigue con un select de rangos horarios desde GET api/DiaRangoHorario/grilla-por-dia.
         IdUsuarioResponsable -> se consigue con un select para elegir el responsable, desde List<GrillaDiaRangoHorarioResponsableDto> Responsables del dto del endpoint anterior
         IdUsuarioSocio -> se consigue de 2 formas:
            Modulo Gestionar Turno Socio - Con el id del usuario que esta logueado, que se puede sacar del token. POST api/Auth/login
            Modulo Gestionar Turno Asistente - Con un select para elegir el socio. Se arma con los datos de los usuarios que son socios desde GET api/Usuario/grilla-socio
         */
        //[Authorize(Policy = "AgregarTurno")]
        [HttpPost("registrar")]
        public async Task<ActionResult<TurnoDto>> RegistrarTurno(TurnoInsertDto turnoInsertDto)
        {
            if (!await _turnoService.ValidateAsync(turnoInsertDto))
                return Conflict(_turnoService.Errors);

            var turno = await _turnoService.RegistrarTurno(turnoInsertDto);
            if (turno is null)
                return Conflict(_turnoService.Errors);

            return Ok(turno);
        }

        /// CUD08 - Eliminar Turno
        /* Front: Se apreta el boton de cancelar turno, se muestra un mensaje de confirmacion "¿Confirma que desea cancelar este turno?" y se cancela el turno seleccionado.
           Esto se puede hacer tanto desde el módulo del Socio como del Asistente.
         */
        [Authorize(Policy = "CancelarTurno")] // Política para proteger el endpoint
        [HttpPatch("cancelar/{idTurno}")]
        public async Task<ActionResult> CancelarTurno(int idTurno)
        {
            // El servicio se encarga de validar la antelación (RN13), cancelar, liberar cupo y guardar.
            var resultado = await _turnoService.CancelarTurno(idTurno);

            if (!resultado)
            {
                // Si la lista de errores tiene algo, es porque falló una regla de negocio (ej: menos de 3 hs)
                if (_turnoService.Errors.Any())
                    return Conflict(new { message = _turnoService.Errors });

                // Si no hay errores pero devolvió false, es porque el ID no existe en la BD
                return NotFound($"No se encontró el turno con ID: {idTurno}");
            }

            return NoContent(); // 204 No Content - Éxito sin cuerpo de respuesta
        }

        // CUD09 – Validar Ingreso del Socio
        [Authorize(Policy = "ValidarIngreso")]
        [HttpPost("validar-ingreso")]
        public async Task<IActionResult> ValidarIngreso([FromBody] ValidarIngresoDto dto)
        {
            var resultado = await _turnoService.ValidarIngresoAsync(dto);

            if (!resultado)
            {
                // Si la lista de errores tiene algo, es porque falló una regla de negocio
                if (_turnoService.Errors.Any())
                    return Conflict(new { message = _turnoService.Errors });

                // Si no hay errores pero devolvió false, es porque el DNI no existe en la BD
                return NotFound(new { message = $"No se encontró el turno del DNI: {dto.DniSocio}" });
            }

            return Ok(new { message = "Se ha validado exitosamente el ingreso del socio." });
        }
    }
}
