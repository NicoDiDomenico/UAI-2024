using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindFit_Intelligence_Backend.DTOs.Permisos;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Services;

namespace MindFit_Intelligence_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Front: Endpoint para obtener los permisos y asi cambiar en el front el estado global de los botones y opciones del menú según los permisos del usuario
        [Authorize]
        [HttpGet("permisos-actuales")]
        public async Task<ActionResult<PermisosActualizadosDto>> GetPermisosActuales()
        {
            // 1. Extraemos el ID del Claim del usuario logueado
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out int userId))
                return Unauthorized();

            // 2. LLAMADA AL SERVICE (Ahora devuelve el DTO)
            var response = await _authService.ObtenerPermisosActuales(userId);

            // 3. Devolvemos el objeto DTO
            return Ok(response);
        }

        // Front: Login de usuario, con generación de JWT y Permisos asociados al usuario para validar que puede y no hacer en UI
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(LoginUsuarioDto loginUsuarioDto)
        {
            TokenResponseDto? res = await _authService.LoginAsync(loginUsuarioDto);

            if (res == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            return Ok(res);
        }

        // Front: Endpoint para refrescar el token JWT usando el refresh token, sin necesidad de hacer login nuevamente
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            TokenResponseDto? result = await _authService.RefreshTokensAsync(refreshTokenRequestDto);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");

            return Ok(result);
        }

        // Front: Endpoint para solicitar reseteo de contraseña, se envía un email con un token seguro para resetear la contraseña sin revelar si el email existe o no
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto forgotPasswordRequestDto)
        {
            await _authService.ForgotPasswordAsync(forgotPasswordRequestDto);
            return Ok("Se enviaron instrucciones para recuperar la contraseña.");
            // Siempre respondemos con éxito para evitar revelar si el email existe o no
        }

        // Front: Endpoint para resetear la contraseña usando el token enviado por email
        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto)
        {
            bool rta = await _authService.ResetPasswordAsync(resetPasswordRequestDto);

            return rta == true
                ? Ok("Contraseña reseteada correctamente.")
                : BadRequest("Token inválido o expirado.");
        }

        // Front: Endpoint para cambiar la contraseña autenticado dentro del sistema, requiere la contraseña actual
        [Authorize(Policy = "CambiarContrasena")]
        [HttpPost("change-password")]
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
    }
}
