using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vista.Utilidades
{
    public class OpcionComboRangoHorario
    {
        public int Valor { get; set; }
        public string Texto { get; set; }
        public TimeSpan HoraDesde { get; set; } 
        public TimeSpan HoraHasta { get; set; }
        public int Cupo { get; set; }  // Nuevo atributo
    }
}
