using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Turno
    {
        public int IdTurno { get; set; }
        public DateTime FechaTurno { get; set; }
        public string EstadoTurno { get; set; }
        public string CodigoIngreso { get; set; }
        public Usuario unUsuario { get; set; }
        public Socio unSocio { get; set; }
        public RangoHorario unRangoHorario { get; set; }

    }
}
