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

        // Front: Sirve para mostrar en el GrupoBox de Dias del formulario de usuario, el listado de dias disponibles para asignar a la rutina del usuario
        [Authorize]
        [HttpGet("dias")]
        public async Task<ActionResult<IEnumerable<DiaDto>>> GetDias()
        {
            IEnumerable<DiaDto> diaDtos = await _usuarioService.GetDias();

            return Ok(diaDtos);
        }

        // Front: Mostrar listado esencial de usuarios en grilla, con paginación, ordenamiento y filtros
        [Authorize]
        [HttpGet("grilla")]
        public async Task<ActionResult<List<UsuarioGridDto>>> GetUsuariosGrid()
        {
            List<UsuarioGridDto> usuariosGridDto = await _usuarioService.GetUsuariosGrid();

            return Ok(usuariosGridDto);
        }

        // Front: Mostrar detalle de usuario en el formulario al hacer click en la grilla
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto?>> GetUsuarioById(int id)
        {
            UsuarioDto? usuarioDetalleDto = await _usuarioService.GetUsuarioById(id);

            return usuarioDetalleDto == null
                ? NotFound()
                : Ok(usuarioDetalleDto);
        }

        // Front: Crear nuevo usuario desde el formulario
        [Authorize(Policy = "CrearUsuario")]
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
                new { id = usuarioDto!.IdUsuario },
                usuarioDto
            );
        }

        // Front: Editar usuario desde el formulario
        [Authorize(Policy = "EditarUsuario")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto?>> Update(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            // Validation Pattern
            if (!_usuarioService.Validate(usuarioUpdateDto))
                return Conflict(_usuarioService.Errors);

            // FluentValidation
            var validationResult = await _updateValidator.ValidateAsync(usuarioUpdateDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            UsuarioDto? usuarioDto = await _usuarioService.Update(id, usuarioUpdateDto);

            if (_usuarioService.Errors.Any())
                return StatusCode(500, _usuarioService.Errors);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        // Front: Eliminar usuario desde el formulario o boton
        [Authorize(Policy = "EliminarUsuario")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioDto>> Delete(int id)
        {
            /* IMPLMENTAR MANEJO DE REGLAS DE NEGOCIO ANTES DE ELIMINAR, EJEMPLO: Que no se pueda eliminar un usuario que tenga cuota vencida
            if (!_usuarioService.ValidateDelete(id))
                return Conflict(_usuarioService.Errors); // 409 Conflict: reglas de negocio que impiden la operación
            */
            UsuarioDto? usuarioDto = await _usuarioService.Delete(id);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }
    }
}
