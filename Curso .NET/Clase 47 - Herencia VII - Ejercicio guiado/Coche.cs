using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehiculo
{
    class Coche : Vehiculo
    {
        public void Acelerar()
        {
            Console.WriteLine("Acelerando");
        }

        public void Frenar()
        {
            Console.WriteLine("Frenando");
        }

        public override void Conducir()
        {
            // base.Conducir();
            Console.WriteLine("Tomando las curvas con decisión y destreza");
        }

    }
}
