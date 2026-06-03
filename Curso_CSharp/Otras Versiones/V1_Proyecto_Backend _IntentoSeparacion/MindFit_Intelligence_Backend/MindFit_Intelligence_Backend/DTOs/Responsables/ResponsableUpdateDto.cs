using MindFit_Intelligence_Backend.DTOs.Personas;

namespace MindFit_Intelligence_Backend.DTOs.Responsables
{
    public class ResponsableUpdateDto
    {
        public string Username { get; set; } = string.Empty;
        public required PersonaResponsableUpdateDto PersonaResponsable { get; set; }
        public List<int>? IdGrupos { get; set; }
    }
}
