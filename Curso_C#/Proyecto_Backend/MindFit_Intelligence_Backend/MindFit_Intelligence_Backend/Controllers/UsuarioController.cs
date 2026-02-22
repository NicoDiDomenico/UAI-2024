using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Services;
using System.Security.Claims;

namespace MindFit_Intelligence_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
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
            UsuarioDto usuarioDto = await _usuarioService.Add(usuarioInsertDto);

            return CreatedAtAction(
                nameof(GetUsuarioById),
                new { id = usuarioDto.IdUsuario },
                usuarioDto
            );
        }

        // Front: Editar usuario desde el formulario
        [Authorize(Policy = "EditarUsuario")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto?>> Update(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            UsuarioDto? usuarioDto = await _usuarioService.Update(id, usuarioUpdateDto);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        // Front: Eliminar usuario desde el formulario o boton
        [Authorize(Policy = "EliminarUsuario")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioDto>> Delete(int id)
        {
            UsuarioDto? usuarioDto = await _usuarioService.Delete(id);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }
    }
}
