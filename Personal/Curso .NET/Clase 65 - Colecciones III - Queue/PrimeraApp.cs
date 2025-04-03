using System;
using System.Collections.Generic;

namespace AprendiendoColecciones
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> numeros = new Queue<int>();

            // Rellenado o agregando elementos a la cola
            foreach (int numero in new int[5] { 2, 4, 6, 8, 10 })
            {
                numeros.Enqueue(numero);
            }

            // Recorriendo la cola
            Console.WriteLine("recorriendo el Queue");
            foreach (int numero in numeros)
            {
                Console.WriteLine(numero);
            }

            // Eliminando elementos del Queue
            Console.WriteLine("Eliminando elementos");

            numeros.Dequeue(); // Debido al comportamiento de FIFO Dequeue() elimina el priemero que entró

            foreach (int numero in numeros)
            {
                Console.WriteLine(numero);
            }


        }
    }
}