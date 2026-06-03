using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class CupoFecha
    {
        public int IdCupoFecha { get; set; }  // Clave primaria
        public DateTime Fecha { get; set; }   // Fecha del cupo
        public int IdRangoHorario { get; set; } // Relación con RangoHorario
        public int CupoActual { get; set; }   // Cupo disponible para la fecha

        // Relación con la entidad RangoHorario (opcional)
        public RangoHorario RangoHorario { get; set; }
    }
}

