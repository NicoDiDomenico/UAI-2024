using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindFitIntelligence_Backend.Models;
using MindFitIntelligence_Backend.Services;
using MindFitIntelligence_Backend.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Azure;

namespace MindFitIntelligence_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration; // JWT
        private ICommonService<UsuarioDto, InsertUsuarioDto, UpdateUsuarioDto> _usuarioService;
        private IAuthService _authService;
        public static Usuario _unUsuario = new();

        public UsuarioController(
            IConfiguration configuration,
            [FromKeyedServices("usuarioService")] ICommonService<UsuarioDto, InsertUsuarioDto, UpdateUsuarioDto> usuarioService,
            [FromKeyedServices("authService")] IAuthService authService)
        {
            _configuration = configuration; // JWT
            _usuarioService = usuarioService;
            _authService = authService;
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

        #region JWT
        [HttpPost("register")]
        public async Task<ActionResult<UsuarioDto>> Register(InsertUsuarioDto insertUsuarioDto)
        {
            var usuarioDto = await _authService.Register(insertUsuarioDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = usuarioDto.IdUsuario },
                usuarioDto
            );
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(LoginUsuarioDto loginUsuarioDto)
        {
            var responde = await _authService.Login(loginUsuarioDto);

            if (responde == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            return Ok(responde);
            // 1:06:39
        }

        [Authorize]
        [HttpGet("autenticado")]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("Estás autenticado!");
        }

        [Authorize(Roles = "Admin, Dueño")]
        [HttpGet("soloAdmin")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("Estás autenticado Admin querido!");
        }
        #endregion

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
