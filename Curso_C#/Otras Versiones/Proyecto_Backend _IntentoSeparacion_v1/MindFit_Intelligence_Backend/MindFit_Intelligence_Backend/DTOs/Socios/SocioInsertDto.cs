using MindFit_Intelligence_Backend.DTOs.Personas;

namespace MindFit_Intelligence_Backend.DTOs.Socios
{
    public class SocioInsertDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = null!;
        public required PersonaSocioInsertDto PersonaSocio { get; set; }
        public List<int> IdGrupos { get; set; } = new();
    }
}
