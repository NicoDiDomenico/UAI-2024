using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class RangoHorario
    {
        public int IdRangoHorario { get; set; }
        public TimeSpan HoraDesde { get; set; }
        public TimeSpan HoraHasta { get; set; }
        public DateTime Fecha { get; set; }
        public int CupoActual { get; set; }
        public int CupoMaximo { get; set; }
        public Boolean Activo { get; set; }
        public Boolean SoloSabado { get; set; }

        public Usuario UnUsuario { get; set; }
        
    }
}
