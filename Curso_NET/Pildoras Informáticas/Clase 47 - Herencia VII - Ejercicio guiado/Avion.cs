using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehiculo
{
    class Avion : Vehiculo
    {
        public void Aterrizar()
        {
            Console.WriteLine("Aterrizando");
        }

        public void Despegar()
        {
            Console.WriteLine("Despegando");
        }

        public override void Conducir()
        {
            // base.Conducir();
            Console.WriteLine("Surcando los cielos del globo");
        }
    }
}
