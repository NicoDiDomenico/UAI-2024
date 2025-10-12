namespace MindFitIntelligence_Backend.DTOs
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string? NombreYApellido { get; set; }
        public string? Email { get; set; }
        #region JWT
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        #endregion
    }
}
