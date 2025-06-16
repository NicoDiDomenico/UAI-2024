using System;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UsoArray
{
    class Program
    {
        static int[] LeerDatos()
        {
            Console.WriteLine("¿Cuántos elementos quieres que tenga el array?");
            string respuesta = Console.ReadLine();
            int numElementos = int.Parse(respuesta);

            int[] datos = new int[numElementos];

            for (int i = 0; i < numElementos; i++)
            {
                Console.WriteLine($"Introduce el dato para la posición {i}:");
                respuesta = Console.ReadLine();
                int datosElemento = int.Parse(respuesta);

                datos[i] = datosElemento;
            }

            return datos; 
        }

        static void Main(string[] args)
        {
            int[] array = LeerDatos(); // uando un método devuelve un arreglo, lo que devuelve es una referencia al arreglo original en memoria, no una copia del arreglo. Esto significa que si modificas el arreglo que recibes como retorno, estás modificando directamente el arreglo original

            Console.WriteLine("Los datos introducidos son:");
            foreach (int dato in array)
            {
                Console.WriteLine(dato);
            }
        }

    }
}