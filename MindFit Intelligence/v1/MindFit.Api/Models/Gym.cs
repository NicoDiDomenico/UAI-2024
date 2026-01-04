namespace MindFit.Api.Models;

/// <summary>
/// Entidad Gym (Tenant) - Representa un gimnasio cliente del sistema SaaS
/// </summary>
public class Gym
{
    public int Id { get; set; }

    public required string Nombre { get; set; }

    public required string Email { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Provincia { get; set; }

    public string? CodigoPostal { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    // Navigation properties
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
