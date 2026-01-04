namespace MindFit.Api.Models;

/// <summary>
/// Entidad PasswordResetToken - Token para recuperación de contraseña
/// </summary>
public class PasswordResetToken
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public required string Token { get; set; }

    public DateTime FechaExpiracion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public bool Usado { get; set; }

    public DateTime? FechaUso { get; set; }

    // Navigation properties
    public Usuario Usuario { get; set; } = null!;
}
