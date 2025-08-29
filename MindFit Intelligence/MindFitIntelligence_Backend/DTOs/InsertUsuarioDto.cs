namespace MindFitIntelligence_Backend.DTOs
{
    public class InsertUsuarioDto
    {
        public string? NombreYApellido { get; set; }
        public required string Email { get; set; }
    }
}
