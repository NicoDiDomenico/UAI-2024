using System;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

// Veremos Modularizacion
namespace ConceptosPOO
{
    class Program
    {
        static void Main(string[] args)
        {
            realizarTarea();
        }

        static void realizarTarea()
        {
            // TODO:
            // Para ver rapidamente ir a: Ver -> Lista de Tareas
            Punto origen = new Punto();

            Punto destino = new Punto(128, 80);

            double distancia = origen.DistanciaHasta(destino);

            Console.WriteLine($"La distancia entre 2 puntos es: {distancia}");
        }
    }
}