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
        private IUsuarioService _usuarioService;
        private IAuthService _authService;
        public static Usuario _unUsuario = new();

        public UsuarioController(
            IConfiguration configuration,
            IUsuarioService usuarioService,
            IAuthService authService
            )
        {
            _configuration = configuration; // JWT
            _usuarioService = usuarioService;
            _authService = authService;
        }

        [HttpGet("grilla")]
        public async Task<ActionResult<List<UsuarioGridDto>>> GetUsuariosGrid()
        {
            List<UsuarioGridDto> usuariosGridDto = await _usuarioService.GetUsuariosGrid();

            return Ok(usuariosGridDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto?>> GetUsuarioById(int id)
        {
            UsuarioDto? usuarioDetalleDto = await _usuarioService.GetUsuarioById(id);

            return usuarioDetalleDto == null
                ? NotFound()
                : Ok(usuarioDetalleDto);
        }

        [HttpPut("responsables/{id}")]
        public async Task<ActionResult<UsuarioResponsableDto?>> Update(int id, UsuarioResponsableUpdateDto usuarioResponsableUpdateDto)
        {
            UsuarioResponsableDto? usuarioResponsableDto = await _usuarioService.Update(id, usuarioResponsableUpdateDto);

            return usuarioResponsableDto == null ? NotFound() : Ok(usuarioResponsableDto);
        }

        [HttpDelete("responsables/{id}")]
        public async Task<ActionResult<UsuarioResponsableDto>> Delete(int id)
        {
            var UsuarioResponsableDto = await _usuarioService.Delete(id);

            return UsuarioResponsableDto == null ? NotFound() : Ok(UsuarioResponsableDto);
        }

        #region JWT
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
        
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(LoginUsuarioDto loginUsuarioDto)
        {
            TokenResponseDto? res= await _authService.LoginAsync(loginUsuarioDto);

            if (res == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            return Ok(res);
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
            // Obtenemos el IdUsuario del token JWT
            string? claimIdUsuario = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Validamos que el claim exista y sea un número entero
            if (!int.TryParse(claimIdUsuario, out int idUsuario))
                return Unauthorized();

            // Llamamos al servicio para cambiar la contraseña
            bool rta = await _authService.ChangePasswordAsync(idUsuario, dto);

            return rta == true 
                ? Ok("Contraseña cambiada correctamente.")
                : BadRequest("La contraseña actual es incorrecta.");
        }
        #endregion
    }
}
