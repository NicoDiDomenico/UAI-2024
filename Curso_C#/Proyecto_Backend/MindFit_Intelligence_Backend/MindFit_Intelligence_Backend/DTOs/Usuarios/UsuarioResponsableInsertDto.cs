using MindFit_Intelligence_Backend.DTOs.Personas;

namespace MindFit_Intelligence_Backend.DTOs.Usuarios
{
    public class UsuarioResponsableInsertDto
    {
        public PersonaResponsableInsertDto? PersonaResponsableInsertDto { get; set; }

        #region JWT
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Rol { get; set; } = null!;
        #endregion
        public List<int> IdGrupos { get; set; } = new();
    }
}
