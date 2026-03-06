using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindFit_Intelligence_Backend.DTOs.Responsables;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Services.Interfaces;

namespace MindFit_Intelligence_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsableController : ControllerBase
    {
        private readonly IResponsableService _responsableService;
        private readonly IValidator<ResponsableInsertDto> _insertValidator;
        private readonly IValidator<ResponsableUpdateDto> _updateValidator;

        public ResponsableController(
            IResponsableService responsableService,
            IValidator<ResponsableInsertDto> insertValidator,
            IValidator<ResponsableUpdateDto> updateValidator)
        {
            _responsableService = responsableService;
            _insertValidator = insertValidator;
            _updateValidator = updateValidator;
        }

        // Front: Mostrar listado de responsables en grilla
        [Authorize]
        [HttpGet("grilla")]
        public async Task<ActionResult<List<UsuarioGridDto>>> GetResponsablesGrid()
        {
            List<UsuarioGridDto> responsablesGridDto = await _responsableService.GetResponsablesGrid();
            return Ok(responsablesGridDto);
        }

        // Front: Mostrar detalle del responsable en el formulario
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto?>> GetResponsableById(int id)
        {
            UsuarioDto? responsableDto = await _responsableService.GetResponsableById(id);
            return responsableDto == null ? NotFound() : Ok(responsableDto);
        }

        // Front: Crear nuevo responsable desde el formulario
        [Authorize(Policy = "CrearUsuario")]
        [HttpPost("register")]
        public async Task<ActionResult<UsuarioDto>> Register(ResponsableInsertDto responsableInsertDto)
        {
            var validationResult = await _insertValidator.ValidateAsync(responsableInsertDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            UsuarioDto? responsableDto = await _responsableService.Add(responsableInsertDto);

            if (_responsableService.Errors.Any())
                return StatusCode(500, _responsableService.Errors);

            return CreatedAtAction(
                nameof(GetResponsableById),
                new { id = responsableDto!.IdUsuario },
                responsableDto
            );
        }

        // Front: Editar responsable desde el formulario
        [Authorize(Policy = "EditarUsuario")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto?>> Update(int id, ResponsableUpdateDto responsableUpdateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(responsableUpdateDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            UsuarioDto? responsableDto = await _responsableService.Update(id, responsableUpdateDto);

            if (_responsableService.Errors.Any())
                return StatusCode(500, _responsableService.Errors);

            return responsableDto == null ? NotFound() : Ok(responsableDto);
        }

        // Front: Eliminar responsable
        [Authorize(Policy = "EliminarUsuario")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioDto>> Delete(int id)
        {
            if (!await _responsableService.ValidateDelete(id))
                return Conflict(_responsableService.Errors);

            UsuarioDto? responsableDto = await _responsableService.Delete(id);
            return responsableDto == null ? NotFound() : Ok(responsableDto);
        }
    }
}
