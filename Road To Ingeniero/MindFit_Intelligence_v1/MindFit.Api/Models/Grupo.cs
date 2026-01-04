namespace MindFit.Api.Models;

/// <summary>
/// Entidad Grupo - Grupo de permisos con multi-tenancy por GymId
/// </summary>
public class Grupo
{
    public int Id { get; set; }

    // Multi-tenancy
    public int GymId { get; set; }

    public required string Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool EsPredefinido { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    // Navigation properties
    public Gym Gym { get; set; } = null!;

    public ICollection<UsuarioGrupo> UsuarioGrupos { get; set; } = new List<UsuarioGrupo>();

    public ICollection<GrupoPermiso> GrupoPermisos { get; set; } = new List<GrupoPermiso>();
}
