namespace MindFit.Api.Models;

/// <summary>
/// Entidad GrupoPermiso - Relación N:N entre Grupo y Permiso
/// </summary>
public class GrupoPermiso
{
    public int GrupoId { get; set; }

    public int PermisoId { get; set; }

    public DateTime FechaAsignacion { get; set; }

    // Navigation properties
    public Grupo Grupo { get; set; } = null!;

    public Permiso Permiso { get; set; } = null!;
}
