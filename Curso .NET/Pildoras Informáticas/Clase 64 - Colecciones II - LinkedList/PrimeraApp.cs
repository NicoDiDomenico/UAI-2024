using System;
using System.Collections.Generic;

namespace AprendiendoColecciones
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> numerosEnlazados = new LinkedList<int>(); //QUE BOLUDO ACA SE ASIGNAN LAS REFERENCIAS ENTRE NODOS

            // Usamos un foreach para recorrer un array y agregar elementos al LinkedList
            Console.WriteLine("Agregando numeros...");
            foreach (int numero in new int[] { 10, 8, 6, 4, 2, 0 })
            {
                /*numeros.AddFirst(numero);*/ // Agrega cada número al inicio de la lista enlazada
                numerosEnlazados.AddLast(numero); // Ahora lo agrega ultimo
            }

            //numeros.Remove(6);
            LinkedListNode<int> nodoImportante = new LinkedListNode<int>(15);

            numerosEnlazados.AddFirst(nodoImportante);

            Console.WriteLine();
            // Usamos otro foreach para recorrer y mostrar los elementos de la lista enlazada
            foreach (int numero in numerosEnlazados)
            {
                Console.WriteLine(numero); // Imprime cada número en la consola
            }
            
            Console.WriteLine();
            // El código recorre una lista enlazada (`LinkedList<int>`) nodo por nodo usando un bucle `for`. Comienza en el primer nodo (`numeros.First`) y, mientras el nodo no sea nulo (`nodo != null`), accede al valor del nodo actual mediante `nodo.Value` y lo imprime en la consola. Luego, avanza al siguiente nodo con `nodo = nodo.Next`. Esto permite procesar cada elemento de la lista enlazada en orden, siendo útil cuando se necesita acceder a los nodos directamente y no solo a sus valores, como sucede con `foreach`.
            for (LinkedListNode<int> nodo = numerosEnlazados.First; nodo != null; nodo = nodo.Next)
            // No entiendo como sabe el bucle que tiene que tomar el siguiente valor de numeros si nodo solo tiene el priemr valor de 
            {
                int numero = nodo.Value;
                Console.WriteLine(numero);
            }
        }
    }
}