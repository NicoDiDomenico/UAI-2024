namespace MindFit.Api.Models;

/// <summary>
/// Entidad UsuarioGrupo - Relación N:N entre Usuario y Grupo
/// </summary>
public class UsuarioGrupo
{
    public int UsuarioId { get; set; }

    public int GrupoId { get; set; }

    public DateTime FechaAsignacion { get; set; }

    // Navigation properties
    public Usuario Usuario { get; set; } = null!;

    public Grupo Grupo { get; set; } = null!;
}
