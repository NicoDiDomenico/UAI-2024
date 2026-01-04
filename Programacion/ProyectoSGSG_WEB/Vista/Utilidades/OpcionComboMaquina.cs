using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vista.Utilidades
{
    public class OpcionComboMaquina
    {
        public int IdElemento { get; set; }
        public string DescripcionMaquina { get; set; }

        public override string ToString()
        {
            return DescripcionMaquina;
        }
    }
}
