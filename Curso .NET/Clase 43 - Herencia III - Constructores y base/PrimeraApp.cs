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
            Caballo unCaballo = new Caballo("Tiro al Blanco");
            Humano unHumano = new Humano("Nico");
            Gorila unGorila = new Gorila("King Kong");

            Mamiferos unMamifero = new Mamiferos("Jesus");
            // Notar como el siguiente objeto tiene:
            unCaballo.Galopar(); // Su propio metodo
            unCaballo.Respirar(); // El metodo que hereda de Mamifero
            unCaballo.GetType(); // Y METODOS DE LA CLASE OBJECT --> Todos los objetos heredan de la clase Object

            unCaballo.getNombre(); 
            unHumano.getNombre();
            unGorila.getNombre();

            unMamifero.getNombre();
            unHumano.pensar();
        }

    }

    class Mamiferos
    {
        private string nombreSerVivo;

        public Mamiferos(string nombre)
        {
            nombreSerVivo = nombre;
        }

        public void Respirar()
        {
            Console.WriteLine("Soy capaz de respirar");
        }

        public void CuidarCrias()
        {
            Console.WriteLine("Cuido a mis crias hasta que sean mayores");
        }

        public void getNombre()
        {
            Console.WriteLine("El nombre es: " + nombreSerVivo);
        }
    }

    class Caballo : Mamiferos // Asi indico que Caballo hereda de Mamifero
    {
        public Caballo(string nombreCaballo):base(nombreCaballo)
        {

        }
        public void Galopar()
        {
            Console.WriteLine("Soy capaz de galopar");
        }        
    }

    class Humano : Mamiferos
    {
        public Humano(string nombreHumano) : base(nombreHumano)
        {
            
        }
        public void pensar()
        {
            Console.WriteLine("Soy capaz de pensar y además...");
            base.Respirar(); // Otra forma de usar base
        }
    }

    class Gorila : Mamiferos
    {
        public Gorila(string nombreGorila) : base(nombreGorila)
        {

        }
        public void trepar()
        {
            Console.WriteLine("Soy capaz de trepar");
        }
    }

}