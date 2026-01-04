namespace MindFit.Api.Models;

/// <summary>
/// Entidad Permiso - Catálogo global de permisos del sistema (sin GymId)
/// </summary>
public class Permiso
{
    public int Id { get; set; }

    public required string Codigo { get; set; }

    public required string Nombre { get; set; }

    public string? Descripcion { get; set; }

    public required string Modulo { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    // Navigation properties
    public ICollection<GrupoPermiso> GrupoPermisos { get; set; } = new List<GrupoPermiso>();
}
