using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vista.Utilidades
{
    public class OpcionComboCalentamiento
    {
        public int IdCalentamiento { get; set; }
        public int? IdMaquina { get; set; }
        public string DescripcionCalentamiento { get; set; }

        public override string ToString()
        {
            return DescripcionCalentamiento;
        }
    }
}
