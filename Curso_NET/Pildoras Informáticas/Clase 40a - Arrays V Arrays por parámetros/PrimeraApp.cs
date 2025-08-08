using System;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UsoArray
{
    class Program
    {
        static void Main(string[] args)
        { 
            int[] ints;
            ints = new int[4];
            ints[0] = 1;
            ints[1] = 2;
            ints[2] = 3;
            ints[3] = 4;
                
            ProcesaDatos(ints);

            Console.WriteLine();

            // Notar como cambiaron los valores al salir del metodo, esto sucede porque los arrays son objetos y estos se pasan por REFERENCIA.
            foreach (int i in ints)
            {
                Console.WriteLine(i);
            }
        }

        static void ProcesaDatos(int[] datos)
        {
            foreach (int i in datos)
            {
                // i += 10; // Cuando usas un foreach, este crea una copia temporal de cada elemento de la colección. Esa copia es una variable inmutable (no se puede modificar directamente), lo que significa que solo puedes leer su valor, pero no puedes cambiarlo en la colección original.
                Console.WriteLine(i);
            }

            Console.WriteLine();

            // Como foreach no sirve para modificar su valor usaremos for:
            for (int i = 0; i < datos.Length; i++)
            {
                datos[i] += 10;
            }
        }
    }
}