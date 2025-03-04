using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreYApellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public int NroDocumento { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Clave { get; set; }
        public Rol Rol { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public List<RangoHorario> RangoHorario { get; set; }
    }
}
