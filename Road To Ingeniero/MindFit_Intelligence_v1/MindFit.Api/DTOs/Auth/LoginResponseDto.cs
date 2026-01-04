namespace MindFit.Api.DTOs.Auth;

/// <summary>
/// DTO para response de login exitoso
/// </summary>
public class LoginResponseDto
{
    public required string AccessToken { get; set; }

    public required UsuarioDto Usuario { get; set; }

    public required List<string> Permisos { get; set; }
}

public class UsuarioDto
{
    public int Id { get; set; }

    public int GymId { get; set; }

    public required string Email { get; set; }

    public required string Nombre { get; set; }

    public required string Apellido { get; set; }

    public string? Telefono { get; set; }

    public required List<string> Grupos { get; set; }
}
