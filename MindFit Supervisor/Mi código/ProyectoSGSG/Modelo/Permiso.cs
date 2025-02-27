using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Permiso
    {
        public int IdPermiso { get; set; }
        public Rol Rol { get; set; }
        public string NombreMenu { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
