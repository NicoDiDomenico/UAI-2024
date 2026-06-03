using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class PermisoPersonalizado3
    {
        public int IdPermiso { get; set; }
        public int? IdRol { get; set; }
        public int? IdGrupo { get; set; }
        public int? IdAccion { get; set; }
        public string NombreMenu { get; set; }
        public string NombreAccion { get; set; }
        public string TipoPermiso { get; set; } // "Grupo", "Individual", "Combinado"
    }

}
