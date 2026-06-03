using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Calentamiento
    {
        public int IdCalentamiento { get; set; }
        public string DescripcionCalentamiento { get; set; }
        public Maquina MaquinaTipoCardio { get; set; }  // Relación con Máquina (puede ser NULL)
    }
}
