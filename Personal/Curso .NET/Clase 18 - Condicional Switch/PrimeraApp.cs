using System;
using System.Runtime.Intrinsics.X86;


namespace Practica_Metodos
{
    class Program
    {
        static void Main(string[] args){
            // Todo lo que se puede hacer con un switch tambien se puede hacer con un else if, pero no viceversa.
            
            Console.WriteLine("Elige medio de transporte (coche, tren, avión)");

            string medioTransporte = Console.ReadLine();

            switch (medioTransporte)
            {
                case "coche":
                    Console.WriteLine("Velocidad media: 100km/h.");
                    break;
                case "tren":
                    Console.WriteLine("Velocidad media: 250km/h.");
                    break;
                case "avión":
                    Console.WriteLine("Velocidad media: 800km/h.");
                    break;
                default:
                    Console.WriteLine("Ni idea mostruo.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Introduce nº de mes para cálculo de la comisión");
            int nMes = Int32.Parse(Console.ReadLine());

            switch (nMes)
            {
                case 1: // Las constantes deben ser unicas
                    Console.WriteLine("Mes escogido: Enero");
                    break;
                case 2:
                    Console.WriteLine("Mes escogido: Febrero");
                    break;
                case 3:
                    Console.WriteLine("Mes escogido: Marzo");
                    break;
                case 4:
                    Console.WriteLine("Mes escogido: Abril");
                    break;
                case 5:
                    Console.WriteLine("Mes escogido: Mayo");
                    break;
                case 6:
                    Console.WriteLine("Mes escogido: Junio");
                    break;
                case 7:
                    Console.WriteLine("Mes escogido: Julio");
                    break;
                case 8:
                    Console.WriteLine("Mes escogido: Agosto");
                    break;
                case 9:
                    Console.WriteLine("Mes escogido: Septiembre");
                    break;
                case 10:
                    Console.WriteLine("Mes escogido: Octubre");
                    break;
                case 11:
                    Console.WriteLine("Mes escogido: Noviembre");
                    break;
                case 12:
                    Console.WriteLine("Mes escogido: Diciembre");
                    break;
                default:
                    Console.WriteLine("Número de mes no válido. Introduce un número entre 1 y 12.");
                    break;
            }


        }
    }
}