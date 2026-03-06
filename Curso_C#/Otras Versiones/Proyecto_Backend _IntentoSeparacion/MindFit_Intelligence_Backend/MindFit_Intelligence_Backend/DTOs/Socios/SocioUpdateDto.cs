using MindFit_Intelligence_Backend.DTOs.Personas;

namespace MindFit_Intelligence_Backend.DTOs.Socios
{
    public class SocioUpdateDto
    {
        public string Username { get; set; } = string.Empty;
        public required PersonaSocioUpdateDto PersonaSocio { get; set; }
        public List<int>? IdGrupos { get; set; }
    }
}
