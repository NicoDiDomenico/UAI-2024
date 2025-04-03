using System;
using System.Collections.Generic;

namespace AprendiendoColecciones
{
    class Program
    {
        static void Main(string[] args)
        {
            List <int> numeros = new List <int> ();

            // Pagina para ver metodos: https://learn.microsoft.com/es-es/dotnet/api/system.collections.generic.list-1?view=net-8.0

            int[] listaNumeros = new int[] { 3, 6, 8, 10, 50 };

            for (int i = 0; i < listaNumeros.Length; i++)
            {
                numeros.Add(listaNumeros[i]);
            }


            for (int i = 0; i < numeros.Count; i++)
            {
                Console.WriteLine(numeros[i]);
            }

            Console.WriteLine();

            // Introduciendo elementos manualmente
            List<int> numeros2 = new List<int>();
            Console.WriteLine("¿Cuántos elementos quieres introducir?");

            int elem = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < elem; i++)
            {
                numeros2.Add(Int32.Parse(Console.ReadLine()));
            }

            Console.WriteLine("Elementos introducidos: ");

            for (int i = 0; i < elem; i++)
            {
                Console.WriteLine(numeros2[i]);
            }

            // Aplicando Foreach:
            List<int> numeros3 = new List<int>();
            Console.WriteLine("Introduce elementos en la colección (0 para salir)");

            int e = 1; // Variable inicializada con un valor diferente de 0 para entrar al bucle

            while (e != 0) // Mientras elem no sea 0, continúa pidiendo valores
            {
                e = Int32.Parse(Console.ReadLine()); // Lee un número desde la consola

                numeros3.Add(e); // Agrega el número a la colección
            }

            numeros3.RemoveAt(numeros3.Count - 1);

            Console.WriteLine();
            Console.WriteLine("Elementos introducidos: ");

            foreach (int elemento in numeros3) // Itera sobre cada elemento en la colección
            {
                Console.WriteLine(elemento); // Muestra cada elemento en la consola
            }


        }
    }
}