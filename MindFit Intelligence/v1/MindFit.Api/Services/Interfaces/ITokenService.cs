using MindFit.Api.Models;

namespace MindFit.Api.Services.Interfaces;

/// <summary>
/// Interfaz para servicio de generación y validación de tokens JWT
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Genera un Access Token JWT
    /// </summary>
    string GenerateAccessToken(Usuario usuario);

    /// <summary>
    /// Genera un Refresh Token
    /// </summary>
    RefreshToken GenerateRefreshToken(int usuarioId, string? ipAddress = null);

    /// <summary>
    /// Valida un Refresh Token
    /// </summary>
    Task<RefreshToken?> ValidateRefreshTokenAsync(string token);

    /// <summary>
    /// Revoca un Refresh Token
    /// </summary>
    Task RevokeRefreshTokenAsync(string token, string? ipAddress = null, string? replacedByToken = null);

    /// <summary>
    /// Revoca todos los Refresh Tokens de un usuario
    /// </summary>
    Task RevokeAllUserRefreshTokensAsync(int usuarioId, string? ipAddress = null);
}
