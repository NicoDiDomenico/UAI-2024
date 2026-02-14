using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private IAuthService _authService;
        public static Usuario _unUsuario = new();

        public UsuarioController(
            IConfiguration configuration,
            ICommonService<UsuarioResponsableDto, UsuarioResponsableInsertDto, UsuarioResponsableUpdateDto> usuarioService,
            IAuthService authService
            )
        {
            _configuration = configuration; // JWT
            _usuarioService = usuarioService;
            _authService = authService;
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

        #region JWT
        [HttpPost("register")]
        public async Task<ActionResult<UsuarioResponsableDto>> Register(UsuarioResponsableInsertDto usuarioResponsableInsertDto)
        {
            UsuarioResponsableDto UsuarioResponsableDto = await _usuarioService.Add(usuarioResponsableInsertDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = UsuarioResponsableDto.IdUsuario },
                UsuarioResponsableDto
            );
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(LoginUsuarioDto loginUsuarioResponsableDto)
        {
            TokenResponseDto? responde = await _authService.LoginAsync(loginUsuarioResponsableDto);

            if (responde == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            return Ok(responde);
        }
        
        [Authorize]
        [HttpGet("autenticado")]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("Estás autenticado!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("soloAdmin")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("Estás autenticado Admin querido!");
        }

        [Authorize(Roles = "Responsable")]
        [HttpGet("soloResponsable")]
        public IActionResult ResponsableOnlyEndpoint()
        {
            return Ok("Estás autenticado Responsable querido!");
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            TokenResponseDto? result = await _authService.RefreshTokensAsync(refreshTokenRequestDto);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");

            return Ok(result);
        }
        #endregion

        #region Password Reset
        [AllowAnonymous]
        [HttpPost("forgot-password")] // Endpoint para solicitar el reseteo de contraseña
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto forgotPasswordRequestDto)
        {
            await _authService.ForgotPasswordAsync(forgotPasswordRequestDto);
            return Ok("Se enviaron instrucciones para recuperar la contraseña."); 
            // Siempre respondemos con éxito para evitar revelar si el email existe o no
        }

        [AllowAnonymous]
        [HttpPost("reset-password")] // Endpoint para resetear la contraseña usando el token enviado por email
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto)
        {
            bool rta = await _authService.ResetPasswordAsync(resetPasswordRequestDto);

            return rta == true 
                ? Ok("Contraseña reseteada correctamente.")
                : BadRequest("Token inválido o expirado.");
        }

        [Authorize]
        [HttpPost("change-password")] // Endpoint para cambiar la contraseña autenticado, requiere la contraseña actual
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto dto)
        {
            string? userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            bool rta = await _authService.ChangePasswordAsync(userId, dto);

            return rta == true 
                ? Ok("Contraseña cambiada correctamente.")
                : BadRequest("La contraseña actual es incorrecta.");
        }
        #endregion
    }
}
