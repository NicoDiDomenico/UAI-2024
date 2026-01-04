using MindFit.Api.DTOs.Auth;
using MindFit.Api.Models;

namespace MindFit.Api.Services.Interfaces;

/// <summary>
/// Interfaz para servicio de autenticación
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Realiza el login de un usuario
    /// </summary>
    Task<(bool Success, string? ErrorMessage, LoginResponseDto? Response)> LoginAsync(LoginRequestDto request, string? ipAddress = null);

    /// <summary>
    /// Refresca el Access Token usando un Refresh Token
    /// </summary>
    Task<(bool Success, string? ErrorMessage, string? AccessToken)> RefreshTokenAsync(string refreshToken, string? ipAddress = null);

    /// <summary>
    /// Realiza el logout (revoca el Refresh Token)
    /// </summary>
    Task<bool> LogoutAsync(string refreshToken, string? ipAddress = null);

    /// <summary>
    /// Obtiene los permisos de un usuario
    /// </summary>
    Task<List<string>> GetUsuarioPermisosAsync(int usuarioId);
}
