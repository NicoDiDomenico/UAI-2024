using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MindFit.Api.Data;
using MindFit.Api.Models;
using MindFit.Api.Services.Interfaces;

namespace MindFit.Api.Services.Auth;

/// <summary>
/// Servicio para generación y validación de tokens JWT y Refresh Tokens
/// </summary>
public class TokenService : ITokenService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public TokenService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Genera un Access Token JWT con claims mínimos
    /// Claims: sub (UserId), email, gymId (TenantId)
    /// NO incluye permisos (se consultan en cada request)
    /// </summary>
    public string GenerateAccessToken(Usuario usuario)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim("gymId", usuario.GymId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured")));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15), // Access Token: 15 minutos
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Genera un Refresh Token aleatorio y lo persiste en BD
    /// </summary>
    public RefreshToken GenerateRefreshToken(int usuarioId, string? ipAddress = null)
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        var refreshToken = new RefreshToken
        {
            UsuarioId = usuarioId,
            Token = Convert.ToBase64String(randomBytes),
            FechaExpiracion = DateTime.UtcNow.AddDays(7), // Refresh Token: 7 días
            FechaCreacion = DateTime.UtcNow,
            Revocado = false,
            RevokedByIp = ipAddress
        };

        _context.RefreshTokens.Add(refreshToken);

        return refreshToken;
    }

    /// <summary>
    /// Valida un Refresh Token (no revocado, no expirado)
    /// </summary>
    public async Task<RefreshToken?> ValidateRefreshTokenAsync(string token)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(rt => rt.Usuario)
            .FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken == null)
        {
            return null;
        }

        // Verificar si está revocado
        if (refreshToken.Revocado)
        {
            return null;
        }

        // Verificar si expiró
        if (refreshToken.FechaExpiracion < DateTime.UtcNow)
        {
            return null;
        }

        return refreshToken;
    }

    /// <summary>
    /// Revoca un Refresh Token (con rotación)
    /// </summary>
    public async Task RevokeRefreshTokenAsync(string token, string? ipAddress = null, string? replacedByToken = null)
    {
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken == null)
        {
            return;
        }

        refreshToken.Revocado = true;
        refreshToken.FechaRevocacion = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReplacedByToken = replacedByToken;

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Revoca todos los Refresh Tokens activos de un usuario
    /// Útil para logout global o cambio de contraseña
    /// </summary>
    public async Task RevokeAllUserRefreshTokensAsync(int usuarioId, string? ipAddress = null)
    {
        var activeTokens = await _context.RefreshTokens
            .Where(rt => rt.UsuarioId == usuarioId && !rt.Revocado)
            .ToListAsync();

        foreach (var token in activeTokens)
        {
            token.Revocado = true;
            token.FechaRevocacion = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
        }

        await _context.SaveChangesAsync();
    }
}
