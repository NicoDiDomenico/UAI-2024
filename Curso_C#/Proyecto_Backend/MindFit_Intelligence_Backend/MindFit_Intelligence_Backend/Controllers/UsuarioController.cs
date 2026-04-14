using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindFit_Intelligence_Backend.DTOs.Dia;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.Services;
using MindFit_Intelligence_Backend.Services.Interfaces;
using System.Security.Claims;

namespace MindFit_Intelligence_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<UsuarioInsertDto> _insertValidator;
        private readonly IValidator<UsuarioUpdateDto> _updateValidator;

        public UsuarioController(
            IUsuarioService usuarioService,
            IValidator<UsuarioInsertDto> insertValidator,
            IValidator<UsuarioUpdateDto> updateValidator)
        {
            _usuarioService = usuarioService;
            _insertValidator = insertValidator;
            _updateValidator = updateValidator;
        }

        // MOVI TODO A diaController.cs
        // Testeado --> Anda bien
        /// CUD02 - Paso 1
        // Front: Sirve para mostrar en el GrupoBox de Dias del formulario de usuario, el listado de dias disponibles para asignar a la rutina del usuario
        //[Authorize]
        /*[HttpGet("dias")]
        public async Task<ActionResult<IEnumerable<DiaDto>>> GetDias()
        {
            IEnumerable<DiaDto> diaDtos = await _usuarioService.GetDias();

            return Ok(diaDtos);
        }
        */

        /// No pertenece a ningun CUD, lo deje impementado por si un modulo necesita motrar todos los usuarios 
        // Front: Mostrar listado esencial de usuarios en grilla, con paginación, ordenamiento y filtros
        [Authorize]
        [HttpGet("grilla-usuarios")]
        public async Task<ActionResult<List<UsuarioGridDto>>> GetUsuariosGrid()
        {
            List<UsuarioGridDto> usuariosGridDto = await _usuarioService.GetUsuariosGrid();

            return Ok(usuariosGridDto);
        }

        /// Esto no pertenece a un CUD pero si al Modulo de Gestion del Gimnasio
        [Authorize]
        [HttpGet("grilla-responsable")]
        public async Task<ActionResult<List<ResponsableGridDto>>> GetUsuariosResponsablesGrid()
        {
            List<ResponsableGridDto> dto = await _usuarioService.GetUsuariosResponsablesGrid();
            return Ok(dto);
        }

        /// CUD02 - Paso 1 y CUD03
        // Front: Mostrar listado esencial de socios en grilla NO ELIMINADOS, con paginación, ordenamiento y filtros. Para mostrar todos se muestra un icono de "Ver socios eliminados" para hacerlo visibles en la grilla
        //[Authorize]
        [HttpGet("grilla-socio")]
        public async Task<ActionResult<List<SocioGridDto>>> GetUsuariosSocioGrid()
        {
            List<SocioGridDto> dto = await _usuarioService.GetUsuariosSociosGrid();
            return Ok(dto);
        }

        /// CUD02 - Paso 2 y CUD03
        // Front: Mostrar detalle de usuario en el formulario al hacer click en la grilla
        //[Authorize]
        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<UsuarioDto?>> GetUsuarioById(int idUsuario)
        {
            UsuarioDto? usuarioDetalleDto = await _usuarioService.GetUsuarioById(idUsuario);

            return usuarioDetalleDto == null
                ? NotFound()
                : Ok(usuarioDetalleDto);
        }

        // Testeado --> Anda bien
        /// CUD02 Paso 3 al 8
        // Front: Crear nuevo usuario desde el formulario
        //[Authorize(Policy = "CrearUsuario")]
        [HttpPost("register")]
        public async Task<ActionResult<UsuarioDto>> Register(UsuarioInsertDto usuarioInsertDto)
        {
            // Validation Pattern
            if (!_usuarioService.Validate(usuarioInsertDto))
                return Conflict(_usuarioService.Errors);

            // FluentValidation
            var validationResult = await _insertValidator.ValidateAsync(usuarioInsertDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            UsuarioDto? usuarioDto = await _usuarioService.Add(usuarioInsertDto);

            if (_usuarioService.Errors.Any())
                return StatusCode(500, _usuarioService.Errors);

            return CreatedAtAction(
                nameof(GetUsuarioById),
                new { idUsuario = usuarioDto!.IdUsuario },
                usuarioDto
            );
        }

        // Testeado --> Anda bien
        /// CUD04
        // Front: Editar usuario desde el formulario
        //[Authorize(Policy = "EditarUsuario")]
        [HttpPut("{idUsuario}")]
        public async Task<ActionResult<UsuarioDto?>> Update(int idUsuario, UsuarioUpdateDto usuarioUpdateDto)
        {
            // Validation Pattern
            if (!_usuarioService.Validate(usuarioUpdateDto))
                return Conflict(_usuarioService.Errors);

            // FluentValidation
            var validationResult = await _updateValidator.ValidateAsync(usuarioUpdateDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            UsuarioDto? usuarioDto = await _usuarioService.Update(idUsuario, usuarioUpdateDto);

            if (_usuarioService.Errors.Any())
                return StatusCode(500, _usuarioService.Errors);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        // ESTADO SOCIO NUEVO/ACTUALIZADO A SUSPENDIDO --> endpoint "actualizar-vencidas" en CuotaController.cs

        /// CUD05 Paso *.b.
        // Front: El sistema elimina automáticamente a un socio suspendido durante 30
        // ESTADO SOCIO SUSPENDIDO A ELIMINADO (AUTOMATICA)
        [Authorize] // LO AUTOMATICO LO DEJO SIN RESTRICCION DE ROL O POLITICA PORQUE SE EJECUTARIA DEPUES DE CIERTAS CONDICION EN EL FRONT
        [HttpPatch("eliminacion-automatica/{idUsuario}")]
        public async Task<ActionResult<UsuarioDto>> AutoSoftDeleteSocio(int idUsuario)
        {
            if (!await _usuarioService.ValidateDelete(idUsuario))
                return Conflict(_usuarioService.Errors);

            UsuarioDto? usuarioDto = await _usuarioService.AutoSoftDeleteSocio(idUsuario);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        /// CUD05 
        // Front: Baja lógica de un socio (cambia EstadoSocio a Eliminado). 
        // ESTADO SOCIO SUSPENDIDO A ELIMINADO
        [Authorize(Policy = "EliminarUsuario")] // Cambiar a una política más específica (opcional)
        [HttpPatch("{idUsuario}/baja")]
        public async Task<ActionResult<UsuarioDto>> SoftDeleteSocio(int idUsuario)
        {
            if (!await _usuarioService.ValidateDelete(idUsuario))
                return Conflict(_usuarioService.Errors);

            UsuarioDto? usuarioDto = await _usuarioService.SoftDeleteSocio(idUsuario);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        // Front: Cuando en la grilla se filtra por "Ver socios eliminados" aparece un boton de recuperacion del socio elimninado
        // ESTADO SOCIO ELIMINADO A SUSPENDIDO
        [Authorize(Policy = "EliminarUsuario")]
        [HttpPatch("{idUsuario}/recuperacion")]
        public async Task<ActionResult<UsuarioDto>> RecoverSoftDeletedSocio(int idUsuario)
        {
            if (!await _usuarioService.ValidateRecover(idUsuario))
                return Conflict(_usuarioService.Errors);
            UsuarioDto? usuarioDto = await _usuarioService.RecoverSoftDeletedSocio(idUsuario);
            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        // Front: Eliminar usuario desde el formulario o boton
        // ELIMINACION DEFINITIVA (ELIMINADO a NO HAY ESTADO)
        [Authorize(Policy = "EliminarUsuario")]
        [HttpDelete("{idUsuario}")]
        public async Task<ActionResult<UsuarioDto>> Delete(int idUsuario)
        {
            if (!await _usuarioService.ValidateDelete(idUsuario))
                return Conflict(_usuarioService.Errors);

            UsuarioDto? usuarioDto = await _usuarioService.Delete(idUsuario);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }
    }
}
