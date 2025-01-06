using System;
using System.Runtime.Intrinsics.X86;


namespace Practica_Metodos
{
    class Program
    {
        static void Main(string[] args){
            Console.WriteLine("¿Deseas entrar en el bucle While?");
            string respuesta = Console.ReadLine();

            while (respuesta != "no")
            {
                Console.WriteLine("Estás ejecutando el interior del bucle while");

                Console.WriteLine("Introduce tu nombre, por favor");
                string nombre = Console.ReadLine();

                Console.WriteLine($"Saldrás del bucle {nombre} cuando respondas 'no' a la pregunta");

                Console.WriteLine("¿Deseas repetir otra vez?");
                respuesta = Console.ReadLine();
            }

            Console.WriteLine("Has salido del bucle");
        }
    }
}