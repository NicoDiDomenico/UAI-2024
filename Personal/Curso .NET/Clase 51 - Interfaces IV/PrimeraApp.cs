using System;
using System.Threading.Channels;
using System.Timers;

namespace AvisosVarios
{
    class Program
    {
        static void Main(string[] args)
        {
            AvisosTrafico av1 = new AvisosTrafico();

            Console.WriteLine(av1.getFecha());

            av1.mostrarAviso();

            Console.WriteLine();

            AvisosTrafico av2 = new AvisosTrafico("Jefatura Provincial Madrid","Sanción de velocidad: $ 300","02-05-19");

            Console.WriteLine(av2.getFecha());

            av2.mostrarAviso();

        }

    }
}