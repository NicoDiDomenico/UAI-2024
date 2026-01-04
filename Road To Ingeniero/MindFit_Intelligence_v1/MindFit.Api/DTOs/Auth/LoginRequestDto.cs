namespace MindFit.Api.DTOs.Auth;

/// <summary>
/// DTO para request de login
/// </summary>
public class LoginRequestDto
{
    public required int GymId { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}
