using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Entrenamiento
    {
        public int IdEntrenamiento { get; set; }
        public int Series { get; set; }
        public int Repeticiones { get; set; }
        public ElementoGimnasio ElementoGimnasio { get; set; }  // Puede ser una Máquina, Equipamiento o Ejercicio
    }
}
