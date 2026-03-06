namespace MindFit_Intelligence_Backend.DTOs.Usuarios
{
    public class ResponsableGridDto
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
    }
}
