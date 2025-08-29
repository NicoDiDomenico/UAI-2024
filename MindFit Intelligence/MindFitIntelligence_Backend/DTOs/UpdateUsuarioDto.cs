namespace MindFitIntelligence_Backend.DTOs
{
    public class UpdateUsuarioDto
    {
        public int IdUsuario { get; set; }
        public string? NombreYApellido { get; set; }
        public required string Email { get; set; }
    }
}
