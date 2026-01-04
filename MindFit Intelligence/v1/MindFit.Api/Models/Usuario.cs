namespace MindFit.Api.Models;

/// <summary>
/// Entidad Usuario - Usuario del sistema con multi-tenancy por GymId
/// </summary>
public class Usuario
{
    public int Id { get; set; }

    // Multi-tenancy
    public int GymId { get; set; }

    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public required string Nombre { get; set; }

    public required string Apellido { get; set; }

    public string? Telefono { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public DateTime? UltimoAcceso { get; set; }

    // Navigation properties
    public Gym Gym { get; set; } = null!;

    public ICollection<UsuarioGrupo> UsuarioGrupos { get; set; } = new List<UsuarioGrupo>();

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();
}
