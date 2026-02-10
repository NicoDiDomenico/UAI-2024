using MindFit_Intelligence_Backend.Models.Enums;

namespace MindFit_Intelligence_Backend.DTOs.Personas
{
    public class PersonaResponsableInsertDto
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public string? Ciudad { get; set; }

        public string TipoDocumento { get; set; } = null!;

        string NroDocumento { get; set; } = null!;

        public Genero? Genero { get; set; }

        public DateTime? FechaNacimiento { get; set; }
    }
}
