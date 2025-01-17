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
            // Ver 7:30 No entiendo
            Humano unHumano = new Humano("Nico");
            Gorila unGorila = new Gorila("King Kong");

            Mamiferos unMamifero = new Mamiferos("Jesus");
            // Notar como el siguiente objeto tiene:
            unCaballo.Galopar(); // Su propio metodo
            //unCaballo.Respirar(); // El metodo que hereda de Mamifero --> Ya no s epuede accededer desde main debido al protected
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

            // unCaballo.Respirar(); --> No funciona porque Main no hereda de Mamifero

            Console.WriteLine();
            // Aplicando lo que hicimos de interfaces
            Ballena miWally = new Ballena("Wally");

            miWally.Nadar();

            Console.WriteLine("Número de patas de Babieca: " + unCaballo.NumeroPatas());


        }

    }
    interface IMamiferosTerrestres
    {
        int NumeroPatas();
    }

    interface IAnimalesYDeportes
    {
        string TipoDeporte();
        Boolean EsOlimpico();
    }

    interface ISaltoConPatas
    {
        int NumeroPatas();
    }

    class Mamiferos
    {
        private string nombreSerVivo;

        public Mamiferos(string nombre)
        {
            nombreSerVivo = nombre;
        }

        protected void Respirar()
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

    class Ballena : Mamiferos
    {
        public Ballena(string nombre) : base(nombre)
        {

        }

        public void Nadar()
        {
            Console.WriteLine("Soy capaz de nadar ");
        }
    }

    class Caballo : Mamiferos, IMamiferosTerrestres, IAnimalesYDeportes, ISaltoConPatas // Primero se eescriben la clase de la que hereda y luego separado por ',' las interfaces.
    {
        public Caballo(string nombreCaballo):base(nombreCaballo)
        {

        }
        public void Galopar()
        {
            Console.WriteLine("Soy capaz de galopar");
            Respirar(); // Funciona porque su hereda de Mamifero esta clase
        }

        int IMamiferosTerrestres.NumeroPatas() // Notar como hay que quitar el public ya que por logica al tener 2 con el mismo nombre, no quiero que sean visibles en otras clases ya que el usuario no va a saber cual usar
        {
            return 4;
        }

        public string TipoDeporte() 
        {
            return "Hípica";
        }

        public Boolean EsOlimpico()
        {
            return true;
        }
        int ISaltoConPatas.NumeroPatas()
        {
            return 2;
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

    class Gorila : Mamiferos, IMamiferosTerrestres
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

        public int NumeroPatas()
        {
            return 2;
        }
    }

}