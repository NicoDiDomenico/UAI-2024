namespace MindFit_Intelligence_Backend.DTOs.Usuarios
{
    public class TokenResponseDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
