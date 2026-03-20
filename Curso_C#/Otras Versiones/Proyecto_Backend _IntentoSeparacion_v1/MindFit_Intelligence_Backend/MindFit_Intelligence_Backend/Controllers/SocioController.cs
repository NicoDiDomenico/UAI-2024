using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindFit_Intelligence_Backend.DTOs.Dia;
using MindFit_Intelligence_Backend.DTOs.Socios;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Services.Interfaces;

namespace MindFit_Intelligence_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocioController : ControllerBase
    {
        private readonly ISocioService _socioService;
        private readonly IValidator<SocioInsertDto> _insertValidator;
        private readonly IValidator<SocioUpdateDto> _updateValidator;

        public SocioController(
            ISocioService socioService,
            IValidator<SocioInsertDto> insertValidator,
            IValidator<SocioUpdateDto> updateValidator)
        {
            _socioService = socioService;
            _insertValidator = insertValidator;
            _updateValidator = updateValidator;
        }

        // Front: Listado de días disponibles para asignar a la rutina del socio
        [Authorize]
        [HttpGet("dias")]
        public async Task<ActionResult<IEnumerable<DiaDto>>> GetDias()
        {
            IEnumerable<DiaDto> diaDtos = await _socioService.GetDias();
            return Ok(diaDtos);
        }

        // Front: Mostrar listado de socios en grilla
        [Authorize]
        [HttpGet("grilla")]
        public async Task<ActionResult<List<UsuarioGridDto>>> GetSociosGrid()
        {
            List<UsuarioGridDto> sociosGridDto = await _socioService.GetSociosGrid();
            return Ok(sociosGridDto);
        }

        // Front: Mostrar detalle del socio en el formulario
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto?>> GetSocioById(int id)
        {
            UsuarioDto? socioDto = await _socioService.GetSocioById(id);
            return socioDto == null ? NotFound() : Ok(socioDto);
        }

        // Front: Crear nuevo socio desde el formulario
        [Authorize(Policy = "CrearUsuario")]
        [HttpPost("register")]
        public async Task<ActionResult<UsuarioDto>> Register(SocioInsertDto socioInsertDto)
        {
            var validationResult = await _insertValidator.ValidateAsync(socioInsertDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            UsuarioDto? socioDto = await _socioService.Add(socioInsertDto);

            if (_socioService.Errors.Any())
                return StatusCode(500, _socioService.Errors);

            return CreatedAtAction(
                nameof(GetSocioById),
                new { id = socioDto!.IdUsuario },
                socioDto
            );
        }

        // Front: Editar socio desde el formulario
        [Authorize(Policy = "EditarUsuario")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto?>> Update(int id, SocioUpdateDto socioUpdateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(socioUpdateDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            UsuarioDto? socioDto = await _socioService.Update(id, socioUpdateDto);

            if (_socioService.Errors.Any())
                return StatusCode(500, _socioService.Errors);

            return socioDto == null ? NotFound() : Ok(socioDto);
        }

        /// CUD05 Paso *.b.
        // ESTADO SOCIO SUSPENDIDO A ELIMINADO (AUTOMÁTICA)
        [Authorize(Policy = "EliminarUsuario")]
        [HttpPatch("{id}/eliminacion-automatica")]
        public async Task<ActionResult<UsuarioDto>> AutoSoftDeleteSocio(int id)
        {
            if (!await _socioService.ValidateDelete(id))
                return Conflict(_socioService.Errors);

            UsuarioDto? socioDto = await _socioService.AutoSoftDeleteSocio(id);
            return socioDto == null ? NotFound() : Ok(socioDto);
        }

        /// CUD05
        // ESTADO SOCIO SUSPENDIDO A ELIMINADO
        [Authorize(Policy = "EliminarUsuario")]
        [HttpPatch("{id}/baja")]
        public async Task<ActionResult<UsuarioDto>> SoftDeleteSocio(int id)
        {
            if (!await _socioService.ValidateDelete(id))
                return Conflict(_socioService.Errors);

            UsuarioDto? socioDto = await _socioService.SoftDeleteSocio(id);
            return socioDto == null ? NotFound() : Ok(socioDto);
        }

        // ESTADO SOCIO ELIMINADO A SUSPENDIDO
        [Authorize(Policy = "EliminarUsuario")]
        [HttpPatch("{id}/recuperacion")]
        public async Task<ActionResult<UsuarioDto>> RecoverSoftDeletedSocio(int id)
        {
            if (!await _socioService.ValidateRecover(id))
                return Conflict(_socioService.Errors);

            UsuarioDto? socioDto = await _socioService.RecoverSoftDeletedSocio(id);
            return socioDto == null ? NotFound() : Ok(socioDto);
        }

        // ELIMINACIÓN DEFINITIVA
        [Authorize(Policy = "EliminarUsuario")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioDto>> Delete(int id)
        {
            if (!await _socioService.ValidateDelete(id))
                return Conflict(_socioService.Errors);

            UsuarioDto? socioDto = await _socioService.Delete(id);
            return socioDto == null ? NotFound() : Ok(socioDto);
        }
    }
}
