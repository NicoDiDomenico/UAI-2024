using MindFit_Intelligence_Backend.DTOs.Personas;

namespace MindFit_Intelligence_Backend.DTOs.Usuarios
{
    public class UsuarioResponsableInsertDto
    {
        public PersonaResponsableInsertDto? PersonaResponsableInsertDto { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        #region JWT
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        #endregion
    }
}
