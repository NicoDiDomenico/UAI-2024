using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class AuditoriaAcceso
    {
        public int IdAuditoria { get; set; }
        //public int IdUsuario { get; set; }
        public Usuario usuario { get; set; }
        public DateTime FechaHora { get; set; }
        public string TipoEvento { get; set; } // 'Login' o 'Logout'
    }
}
