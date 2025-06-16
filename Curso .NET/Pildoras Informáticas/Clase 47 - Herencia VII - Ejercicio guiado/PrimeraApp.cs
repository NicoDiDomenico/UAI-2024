using System;
using System.Threading.Channels;
using System.Timers;

namespace Vehiculo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Avion
            Console.WriteLine("Probando el avión");

            Avion miAvion = new Avion();

            miAvion.ArrancaMotor("tracatratractar");
            miAvion.Despegar();
            miAvion.Conducir();
            miAvion.Aterrizar();
            miAvion.PararMotor("ploooof");

            // Coche
            Console.WriteLine();
            Console.WriteLine("Probando el coche");

            Coche miCoche = new Coche();

            miCoche.ArrancaMotor("grrrum grrrrum");
            miCoche.Acelerar();
            miCoche.Conducir();
            miCoche.Frenar();
            miCoche.PararMotor("bluuuuuuf");

            // Polimorfismo
            Console.WriteLine("Polimorfismo en acción!");

            Vehiculo miVehiculo = miCoche;

            miVehiculo.Conducir();

            miVehiculo = miAvion;

            miVehiculo.Conducir();

        }

    }
}