using System;
using System.Threading.Channels;
using System.Timers;

namespace ProyectoHerencia
{
    class Program
    {
        static void Main(string[] args)
        {
            Lagartija Juancho = new Lagartija("Juancho");

            Juancho.Respirar();
            Juancho.GetNombre();

            Humano Juan = new Humano("Juan");

            Juan.Respirar();
            Juan.GetNombre();
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
        int NumeroPatas(); // Por defecto el modificador de acceso es public
    }

    abstract class Animales
    {
        public void Respirar()
        {
            Console.WriteLine("Soy capaz de respirar");
        }

        public abstract void GetNombre();
    }

    class Lagartija : Animales
    {
        private string nombreReptil;

        public Lagartija(string nombre)
        {
            nombreReptil = nombre;
        }

        public override void GetNombre()
        {
            Console.WriteLine("El nombre es: " + nombreReptil);
        }
    }
    class Mamiferos : Animales
    {
        private string nombreSerVivo;

        public Mamiferos(string nombre)
        {
            nombreSerVivo = nombre;
        }

        public void CuidarCrias()
        {
            Console.WriteLine("Cuido a mis crias hasta que sean mayores");
        }

        public override void GetNombre()
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
        int ISaltoConPatas.NumeroPatas()
        {
            return 2;
        }

        public string TipoDeporte() 
        {
            return "Hípica";
        }

        public Boolean EsOlimpico()
        {
            return true;
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