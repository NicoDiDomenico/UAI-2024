using System;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Math; // Ahora no necesitamos escribir la clase Math (No se recomienda hacer esto)

// Veremos Modularizacion
namespace ConceptosPOO
{
    class Program
    {
        static void Main(string[] args)
        {
            //realizarTarea();

            double raiz = Sqrt(9);
            
            double potencia = Pow(3, 4);


            Console.WriteLine(raiz);

            Console.WriteLine(potencia);

            // Clases Anónimas:
            // var <Objeto> = new <Clase Anónima> (El tipo se asigna en tiempo de ejecucion segun el valor que se le asigne)
            var miVariable = new { Nombre = "Juan", Edad = 19 };

            Console.WriteLine(miVariable.Nombre + " " + miVariable.Edad);

            var miOtraVariable = new { Nombre = "Ana", Edad = 25 };

            miVariable = miOtraVariable; // C# me permite hacer eso porque al compilar se da cuenta que son el mismo tipo de objeto debido a la cantidad y tipo de atributos.


        }

        static void realizarTarea()
        {
            // TODO:
            // Para ver rapidamente ir a: Ver -> Lista de Tareas
            Punto origen = new Punto();

            Punto destino = new Punto(128, 80);

            double distancia = origen.DistanciaHasta(destino);

            Console.WriteLine($"La distancia entre 2 puntos es: {distancia}");
            Punto.CantidadObjetos();
        }
    }
}