namespace MindFit.Api.Models;

/// <summary>
/// Entidad RefreshToken - Token de refresco para renovación de JWT
/// </summary>
public class RefreshToken
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public required string Token { get; set; }

    public DateTime FechaExpiracion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public bool Revocado { get; set; }

    public DateTime? FechaRevocacion { get; set; }

    public string? RevokedByIp { get; set; }

    public string? ReplacedByToken { get; set; }

    // Navigation properties
    public Usuario Usuario { get; set; } = null!;
}
