using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Models.Enums;

namespace MindFit_Intelligence_Backend.DTOs.Personas
{
    public class PersonaSocioUpdateDto
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string TipoDocumento { get; set; } = null!;
        public string NroDocumento { get; set; } = null!;
        public Genero? Genero { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? ObraSocial { get; set; }
        public string? Plan { get; set; }
        public string? EstadoSocio { get; set; }
        public DateTime? FechaInicioActividades { get; set; }
        public DateTime? FechaFinActividades { get; set; }
        public DateTime? FechaNotificacion { get; set; }
        public bool? RespuestaNotificacion { get; set; }
        public string? Pregunta { get; set; }
        public string? Respuesta { get; set; }
    }
}
