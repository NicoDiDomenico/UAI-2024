using System.Security.Claims;

namespace MindFit.Api.Middleware;

/// <summary>
/// Middleware que extrae el GymId del JWT y lo almacena en HttpContext.Items
/// Esto permite que los Query Filters de EF Core lo utilicen para aislamiento multi-tenant
/// </summary>
public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TenantResolverMiddleware> _logger;

    public TenantResolverMiddleware(RequestDelegate next, ILogger<TenantResolverMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Intentar obtener el GymId del claim del JWT
        var gymIdClaim = context.User?.FindFirst("gymId");

        if (gymIdClaim != null && int.TryParse(gymIdClaim.Value, out var gymId))
        {
            // Almacenar GymId en HttpContext.Items para uso en DbContext
            context.Items["GymId"] = gymId;

            _logger.LogDebug("TenantId (GymId) resuelto: {GymId}", gymId);
        }
        else
        {
            // No hay GymId (request público o sin autenticación)
            _logger.LogDebug("No se pudo resolver TenantId (GymId) - Request público o sin autenticación");
        }

        await _next(context);
    }
}
