using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Gimnasio
    {
        public int IdGimnasio { get; set; }
        public string NombreGimnasio { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public TimeSpan HoraAperturaLaV { get; set; }
        public TimeSpan HoraCierreLaV { get; set; }
        public TimeSpan HoraAperturaSabado { get; set; }
        public TimeSpan HoraCierreSabado { get; set; }

    }
}
