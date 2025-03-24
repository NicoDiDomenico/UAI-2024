using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Maquina : ElementoGimnasio
    {
        public DateTime FechaFabricacion { get; set; }
        public DateTime FechaCompra { get; set; }
        public float Precio { get; set; }
        public int Peso { get; set; }
        public bool EsElectrica { get; set; }
    }
}
