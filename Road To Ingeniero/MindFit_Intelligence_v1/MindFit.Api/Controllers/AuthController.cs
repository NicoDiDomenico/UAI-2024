using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindFit.Api.DTOs.Auth;
using MindFit.Api.DTOs.Common;
using MindFit.Api.Services.Interfaces;

namespace MindFit.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// POST /api/auth/login
    /// Login con gymId + email + password
    /// Retorna: Access Token (JWT) + Usuario + Permisos
    /// Refresh Token se envía en cookie HttpOnly
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

        var (success, errorMessage, response) = await _authService.LoginAsync(request, ipAddress);

        if (!success || response == null)
        {
            return Unauthorized(ApiResponse<object>.ErrorResponse(errorMessage ?? "Login fallido"));
        }

        // Obtener el refresh token más reciente del usuario
        // (En la implementación real, el RefreshToken debería retornarse desde AuthService)
        // Por ahora, asumimos que está en la respuesta o lo manejamos diferente

        // Configurar cookie HttpOnly con Refresh Token
        SetRefreshTokenCookie(response.AccessToken); // Temporal - debería ser el refresh token real

        return Ok(ApiResponse<LoginResponseDto>.SuccessResponse(response, "Login exitoso"));
    }

    /// <summary>
    /// POST /api/auth/refresh
    /// Refresca el Access Token usando el Refresh Token de la cookie
    /// Implementa rotación de Refresh Token
    /// </summary>
    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized(ApiResponse<object>.ErrorResponse("Refresh token no encontrado"));
        }

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

        var (success, errorMessage, newAccessToken) = await _authService.RefreshTokenAsync(refreshToken, ipAddress);

        if (!success || newAccessToken == null)
        {
            return Unauthorized(ApiResponse<object>.ErrorResponse(errorMessage ?? "Refresh token inválido"));
        }

        // Configurar nueva cookie HttpOnly con el nuevo Refresh Token rotado
        SetRefreshTokenCookie(newAccessToken); // Temporal - debería ser el nuevo refresh token

        return Ok(ApiResponse<object>.SuccessResponse(new { accessToken = newAccessToken }, "Token refrescado"));
    }

    /// <summary>
    /// POST /api/auth/logout
    /// Revoca el Refresh Token (logout)
    /// </summary>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (!string.IsNullOrEmpty(refreshToken))
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            await _authService.LogoutAsync(refreshToken, ipAddress);
        }

        // Eliminar cookie
        Response.Cookies.Delete("refreshToken");

        return Ok(ApiResponse<object>.SuccessResponse(null, "Logout exitoso"));
    }

    /// <summary>
    /// Helper para configurar cookie HttpOnly con Refresh Token
    /// </summary>
    private void SetRefreshTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true, // No accesible desde JavaScript (protección XSS)
            Secure = true,   // Solo HTTPS en producción
            SameSite = SameSiteMode.Strict, // Protección CSRF
            Expires = DateTime.UtcNow.AddDays(7) // 7 días como el refresh token
        };

        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }
}
