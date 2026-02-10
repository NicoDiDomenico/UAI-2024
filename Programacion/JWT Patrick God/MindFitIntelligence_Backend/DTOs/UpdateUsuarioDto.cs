namespace MindFitIntelligence_Backend.DTOs
{
    public class UpdateUsuarioDto
    {
        // public int IdUsuario { get; set; } Mejor no mapearlo porque lo puede sobrescribir 
        public string? NombreYApellido { get; set; }
        public string? Email { get; set; }
        public string Rol { get; set; } = string.Empty;
    }
}
