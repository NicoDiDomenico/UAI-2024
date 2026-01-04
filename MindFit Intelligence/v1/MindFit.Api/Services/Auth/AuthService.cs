using Microsoft.EntityFrameworkCore;
using MindFit.Api.Data;
using MindFit.Api.DTOs.Auth;
using MindFit.Api.Services.Interfaces;
using BCrypt.Net;

namespace MindFit.Api.Services.Auth;

/// <summary>
/// Servicio de autenticación con login, refresh, logout y gestión de permisos
/// </summary>
public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthService(ApplicationDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Login: Valida credenciales, genera tokens, retorna datos del usuario
    /// </summary>
    public async Task<(bool Success, string? ErrorMessage, LoginResponseDto? Response)> LoginAsync(
        LoginRequestDto request, string? ipAddress = null)
    {
        // Buscar usuario por gymId + email
        var usuario = await _context.Usuarios
            .Include(u => u.Gym)
            .Include(u => u.UsuarioGrupos)
                .ThenInclude(ug => ug.Grupo)
            .FirstOrDefaultAsync(u => u.GymId == request.GymId && u.Email == request.Email);

        if (usuario == null)
        {
            return (false, "Credenciales inválidas", null);
        }

        // Verificar si el usuario está activo
        if (!usuario.Activo)
        {
            return (false, "Usuario inactivo", null);
        }

        // Verificar si el gimnasio está activo
        if (!usuario.Gym.Activo)
        {
            return (false, "Gimnasio inactivo", null);
        }

        // Verificar contraseña con BCrypt
        if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
        {
            return (false, "Credenciales inválidas", null);
        }

        // Generar Access Token
        var accessToken = _tokenService.GenerateAccessToken(usuario);

        // Generar Refresh Token y persistirlo
        var refreshToken = _tokenService.GenerateRefreshToken(usuario.Id, ipAddress);
        await _context.SaveChangesAsync();

        // Actualizar último acceso
        usuario.UltimoAcceso = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Obtener permisos del usuario
        var permisos = await GetUsuarioPermisosAsync(usuario.Id);

        // Construir response
        var response = new LoginResponseDto
        {
            AccessToken = accessToken,
            Usuario = new UsuarioDto
            {
                Id = usuario.Id,
                GymId = usuario.GymId,
                Email = usuario.Email,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Telefono = usuario.Telefono,
                Grupos = usuario.UsuarioGrupos.Select(ug => ug.Grupo.Nombre).ToList()
            },
            Permisos = permisos
        };

        return (true, null, response);
    }

    /// <summary>
    /// Refresh Token: Valida refresh token, genera nuevo access token, rota refresh token
    /// </summary>
    public async Task<(bool Success, string? ErrorMessage, string? AccessToken)> RefreshTokenAsync(
        string refreshToken, string? ipAddress = null)
    {
        // Validar refresh token
        var validToken = await _tokenService.ValidateRefreshTokenAsync(refreshToken);

        if (validToken == null)
        {
            return (false, "Refresh token inválido o expirado", null);
        }

        var usuario = validToken.Usuario;

        // Verificar si el usuario está activo
        if (!usuario.Activo)
        {
            return (false, "Usuario inactivo", null);
        }

        // Generar nuevo Access Token
        var newAccessToken = _tokenService.GenerateAccessToken(usuario);

        // Generar nuevo Refresh Token (rotación)
        var newRefreshToken = _tokenService.GenerateRefreshToken(usuario.Id, ipAddress);
        await _context.SaveChangesAsync();

        // Revocar el refresh token anterior
        await _tokenService.RevokeRefreshTokenAsync(refreshToken, ipAddress, newRefreshToken.Token);

        return (true, null, newAccessToken);
    }

    /// <summary>
    /// Logout: Revoca el refresh token
    /// </summary>
    public async Task<bool> LogoutAsync(string refreshToken, string? ipAddress = null)
    {
        await _tokenService.RevokeRefreshTokenAsync(refreshToken, ipAddress);
        return true;
    }

    /// <summary>
    /// Obtiene todos los permisos de un usuario consultando sus grupos
    /// Los permisos se consultan en cada request (no se cachean en JWT)
    /// </summary>
    public async Task<List<string>> GetUsuarioPermisosAsync(int usuarioId)
    {
        var permisos = await _context.UsuarioGrupos
            .Where(ug => ug.UsuarioId == usuarioId && ug.Grupo.Activo)
            .SelectMany(ug => ug.Grupo.GrupoPermisos)
            .Where(gp => gp.Permiso.Activo)
            .Select(gp => gp.Permiso.Codigo)
            .Distinct()
            .ToListAsync();

        return permisos;
    }
}
