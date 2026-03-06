using MindFit_Intelligence_Backend.DTOs.Personas;

namespace MindFit_Intelligence_Backend.DTOs.Responsables
{
    public class ResponsableInsertDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = null!;
        public required PersonaResponsableInsertDto PersonaResponsable { get; set; }
        public List<int> IdGrupos { get; set; } = new();
    }
}
