using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Socio
    {
        public int IdSocio { get; set; }
        public string NombreYApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public int NroDocumento { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string ObraSocial { get; set; }
        public string Plan { get; set; }  // No es palabra reservada en C#
        public string EstadoSocio { get; set; }
        public DateTime? FechaInicioActividades { get; set; } // Puede ser NULL
        public DateTime? FechaFinActividades { get; set; }    // Puede ser NULL
        public DateTime? FechaNotificacion { get; set; }      // Puede ser NULL
        public bool? RespuestaNotificacion { get; set; }      // Puede ser NULL

        // Relación con Rutina (Lista de Rutinas asociadas al Socio)
        public List<Rutina> Rutinas { get; set; }

        public DateTime? FechaUltimoTurno { get; set; }
        public string NombreCompleto => NombreYApellido;
        public string Estado => EstadoSocio;

    }
}
