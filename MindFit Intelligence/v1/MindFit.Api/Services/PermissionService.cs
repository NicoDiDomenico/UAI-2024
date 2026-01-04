using MindFit.Api.Services.Interfaces;

namespace MindFit.Api.Services;

/// <summary>
/// Servicio para obtener y cachear permisos del usuario actual en el request
/// </summary>
public class PermissionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthService _authService;

    public PermissionService(IHttpContextAccessor httpContextAccessor, IAuthService authService)
    {
        _httpContextAccessor = httpContextAccessor;
        _authService = authService;
    }

    /// <summary>
    /// Verifica si el usuario actual tiene un permiso específico
    /// Los permisos se cachean en HttpContext.Items por request
    /// </summary>
    public async Task<bool> HasPermissionAsync(string permissionCode)
    {
        var permisos = await GetCurrentUserPermissionsAsync();
        return permisos.Contains(permissionCode);
    }

    /// <summary>
    /// Obtiene los permisos del usuario actual (con caché por request)
    /// </summary>
    public async Task<List<string>> GetCurrentUserPermissionsAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            return new List<string>();
        }

        // Verificar si ya están cacheados en el request
        if (httpContext.Items.TryGetValue("UserPermissions", out var cachedPermissions)
            && cachedPermissions is List<string> permissions)
        {
            return permissions;
        }

        // Obtener UserId del JWT
        var userIdClaim = httpContext.User?.FindFirst("sub");
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return new List<string>();
        }

        // Consultar permisos desde BD
        var userPermissions = await _authService.GetUsuarioPermisosAsync(userId);

        // Cachear en el request actual
        httpContext.Items["UserPermissions"] = userPermissions;

        return userPermissions;
    }
}
