using System;
using System.Collections.Generic;

namespace AprendiendoColecciones
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> numeros = new Stack<int>();

            // Rellenado o agregando elementos a la cola
            foreach (int numero in new int[5] { 2, 4, 6, 8, 10 })
            {
                numeros.Push(numero);
            }

            // Recorriendo el Stack
            Console.WriteLine("recorriendo el Queue");
            foreach (int numero in numeros)
            {
                Console.WriteLine(numero);
            }

            // Eliminando elementos del Stack
            Console.WriteLine("Eliminando elementos");

            numeros.Pop(); // Debido al comportamiento de LIFO elimina el primero que entró

            foreach (int numero in numeros)
            {
                Console.WriteLine(numero);
            }


        }
    }
}