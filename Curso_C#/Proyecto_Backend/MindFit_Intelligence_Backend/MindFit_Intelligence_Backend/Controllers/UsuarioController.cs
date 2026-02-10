using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindFit_Intelligence_Backend.DTOs;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.Services;

namespace MindFit_Intelligence_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration; // JWT
        private ICommonService<UsuarioResponsableDto, UsuarioResponsableInsertDto, UsuarioResponsableUpdateDto> _usuarioService;
        //private IAuthService _authService;
        public static Usuario _unUsuario = new();

        public UsuarioController(
            IConfiguration configuration,
            ICommonService<UsuarioResponsableDto, UsuarioResponsableInsertDto, UsuarioResponsableUpdateDto> usuarioService
            )
        {
            _configuration = configuration; // JWT
            _usuarioService = usuarioService;
            //_authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponsableDto>>> Get()
        {
            var usuarios = await _usuarioService.Get();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponsableDto?>> GetById(int id)
        {
            var UsuarioResponsableDto = await _usuarioService.GetById(id);

            return UsuarioResponsableDto == null
                ? NotFound()
                : Ok(UsuarioResponsableDto);
        }
        /*
        #region JWT
        [HttpPost("register")]
        public async Task<ActionResult<UsuarioResponsableDto>> Register(UsuarioResponsableInsertDto usuarioResponsableInsertDto)
        {
            var UsuarioResponsableDto = await _authService.Register(usuarioResponsableInsertDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = UsuarioResponsableDto.IdUsuario },
                UsuarioResponsableDto
            );
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(LoginUsuarioDto loginUsuarioResponsableDto)
        {
            var responde = await _authService.Login(loginUsuarioResponsableDto);

            if (responde == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            return Ok(responde);
            // 1:06:39
        }
        
        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var result = await _authService.RefreshTokensAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");

            return Ok(result);
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
        */

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioResponsableDto?>> Update(int id, UsuarioResponsableUpdateDto usuarioResponsableUpdateDto)
        {
            UsuarioResponsableDto? usuarioResponsableDto = await _usuarioService.Update(id, usuarioResponsableUpdateDto);

            return usuarioResponsableDto == null ? NotFound() : Ok(usuarioResponsableDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioResponsableDto>> Delete(int id)
        {
            var UsuarioResponsableDto = await _usuarioService.Delete(id);

            return UsuarioResponsableDto == null ? NotFound() : Ok(UsuarioResponsableDto);
        }
    }
}
