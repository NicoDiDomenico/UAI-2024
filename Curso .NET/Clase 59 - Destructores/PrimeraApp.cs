using System;
using System.Collections.Concurrent;
using System.Threading.Channels;
using System.Timers;
using System.Xml;

namespace AprendiendoDestructor
{
    using System;

    class Recurso
    {
        // Constructor de la clase
        public Recurso()
        {
            Console.WriteLine("Recurso creado.");
        }

        // Destructor de la clase
        ~Recurso()
        {
            // Este código se ejecuta cuando el Garbage Collector elimina el objeto
            Console.WriteLine("Recurso destruido por el Garbage Collector.");
        }

        public void UsarRecurso()
        {
            Console.WriteLine("Usando el recurso...");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio del programa.");

            // Crear un objeto de tipo Recurso
            Recurso recurso = new Recurso();
            recurso.UsarRecurso();

            // Forzar que el objeto esté fuera de alcance
            recurso = null;

            // Sugerir al Garbage Collector que haga limpieza
            GC.Collect(); // Nota: No garantiza que el destructor se ejecute de inmediato
            GC.WaitForPendingFinalizers(); // Espera a que se ejecuten los destructores

            Console.WriteLine("Fin del programa.");
        }
    }
    /* No me anda porque no se llega a liberar el recurso antes de que se termine el programa, pero lo que tendria que mostrar la consola es lo siguiente:
    Inicio del programa.
    Recurso creado.
    Usando el recurso...
    Recurso destruido por el Garbage Collector.
    Fin del programa.
    */
}