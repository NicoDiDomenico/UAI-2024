using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Accion
    {
        public int IdAccion { get; set; }
        public string NombreAccion { get; set; }
        public string Descripcion { get; set; }
        public int IdGrupo { get; set; }
        public Grupo unGrupo { get; set; }
    }
}
