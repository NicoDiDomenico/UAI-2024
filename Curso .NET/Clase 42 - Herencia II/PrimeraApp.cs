using System;
using System.Threading.Channels;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoHerencia
{
    class Program
    {
        static void Main(string[] args)
        {
            Caballo TiroAlBlanco = new Caballo();
            Humano Nico = new Humano();
            Gorila Kong = new Gorila();

            // Notar como el siguiente objeto tiene:
            TiroAlBlanco.Galopar(); // Su propio metodo
            TiroAlBlanco.Respirar(); // El metodo que hereda de Mamifero
            TiroAlBlanco.GetType(); // Y METODOS DE LA CLASE OBJECT --> Todos los objetos heredan de la clase Object


        }

    }

    class Mamiferos
    {
        public void Respirar()
        {
            Console.WriteLine("Soy capaz de respirar");
        }

        public void CuidarCrias()
        {
            Console.WriteLine("Cuido a mis crias hasta que sean mayores");
        }
    }

    class Caballo : Mamiferos // Asi indico que Caballo hereda de Mamifero
    {
        public void Galopar()
        {
            Console.WriteLine("Soy capaz de galopar");
        }        
    }

    class Humano : Mamiferos
    {
        public void pensar()
        {
            Console.WriteLine("Soy capaz de pensar ¿?");
        }
    }

    class Gorila : Mamiferos
    {
        public void trepar()
        {
            Console.WriteLine("Soy capaz de trepar");
        }
    }

}