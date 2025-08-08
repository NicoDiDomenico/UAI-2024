using System;
using System.Threading.Channels;
using System.Timers;

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

            // Principio de sustitucion:
            Mamiferos soyPepe = new Caballo("Pepe");
            // Caballo soyPepe = new Mamifero("Pepe"); --> No funcionara debido a este principio

            // Util para sustituir un objeto por otro:
            Mamiferos unAnimal = new Mamiferos("Bucéfalo");
            Caballo unBucefalo = new Caballo("Bucéfalo");

            unAnimal = unBucefalo;
            // Bucefalo = animal; --> Esto estará mal

            // Entonces nos dejara hacer esto:
            Object miAnimal = new Caballo("Bucéfalo");
            Object miPersona = new Humano("Juan");
            Object miMamifero = new Mamiferos("Wally");

            // Utilidad en arreglos:
            Mamiferos[] almacenAnimales = new Mamiferos[3];

            almacenAnimales[0] = unCaballo;
            almacenAnimales[1] = unHumano;
            almacenAnimales[2] = unGorila;

            almacenAnimales[1].getNombre();

            // Polimorfism - es la capacidad que tienen los objetos en programacion de comportarse de diferentes formas o a tener diferente forma dependiendo del contexto
            // Poli... : Muchas
            // ...morfimo : formas
            for (int i=0; i<3; i++)
            {
                almacenAnimales[i].pensar();
            }

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

        // Habilitando el Polimorfismo con Virtual en la superclase y Override en las subclases que necesite
        virtual public void pensar()
        {
            Console.WriteLine("Soy capaz de pensar");
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
        public Humano(string nombreHumano):base(nombreHumano)
        {

        }

        // Cuadno tenga un metodo con el mismo nombre, cantidad y tipo de parametros que otro que hereda, se escribira new para indicarle a C# que está hecho aproposito para que se llame al metodo de la clase hija y no al de la clase padre.
        override public void pensar()
        {
            Console.WriteLine("Soy capaz de pensar como Humano");
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

        override public void pensar()
        {
            Console.WriteLine("Soy capaz de pensar como Gorila");
        }
    }

}