using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindFitIntelligence_Backend.Models;
using MindFitIntelligence_Backend.Services;
using MindFitIntelligence_Backend.DTOs;

namespace MindFitIntelligence_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private ICommonService<UsuarioDto, InsertUsuarioDto, UpdateUsuarioDto> _usuarioService;

        public UsuarioController(
            [FromKeyedServices("usuarioService")] ICommonService<UsuarioDto, InsertUsuarioDto, UpdateUsuarioDto> usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAll()
        {
            var usuarios = await _usuarioService.GetAll();
            if (_usuarioService.Errors.Count > 0)
                return BadRequest(_usuarioService.Errors);

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto?>> GetById(int id)
        {
            var usuarioDto = await _usuarioService.GetById(id);

            if(_usuarioService.IsNull(usuarioDto))
                return NotFound(_usuarioService.Errors);

            return Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Add(InsertUsuarioDto insertUsuarioDto)
        {
            var usuarioDto = await _usuarioService.Add(insertUsuarioDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = usuarioDto.IdUsuario },
                usuarioDto
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto?>> Update(int id, UpdateUsuarioDto updateUsuarioDto)
        {
            var usuarioDto =  await _usuarioService.Update(id, updateUsuarioDto);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioDto>> Delete(int id)
        {
            var usuarioDto = await _usuarioService.Delete(id);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }
    }
}
