using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class AuditoriaTurno
    {
        public int IdAuditoria { get; set; }
        public int IdTurno { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaHora { get; set; }
        public string Accion { get; set; }
        public string DatosOriginales { get; set; }
        public string DatosNuevos { get; set; }
    }
}

