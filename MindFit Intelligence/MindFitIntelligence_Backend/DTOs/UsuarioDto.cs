namespace MindFitIntelligence_Backend.DTOs
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string? NombreYApellido { get; set; }
        public string? Email { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
