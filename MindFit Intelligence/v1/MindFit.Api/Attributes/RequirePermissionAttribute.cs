using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MindFit.Api.Services;

namespace MindFit.Api.Attributes;

/// <summary>
/// Atributo para requerir un permiso específico en un endpoint
/// Uso: [RequirePermission("SOCIOS_LISTAR")]
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequirePermissionAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string _permissionCode;

    public RequirePermissionAttribute(string permissionCode)
    {
        _permissionCode = permissionCode;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // Obtener PermissionService del DI
        var permissionService = context.HttpContext.RequestServices.GetService<PermissionService>();

        if (permissionService == null)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            return;
        }

        // Verificar si el usuario tiene el permiso
        var hasPermission = await permissionService.HasPermissionAsync(_permissionCode);

        if (!hasPermission)
        {
            context.Result = new ForbidResult();
        }
    }
}
