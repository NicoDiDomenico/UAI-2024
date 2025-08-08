using System;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;


namespace Practica_Metodos
{
    class Program
    {
        static void Main(string[] args) {
            string rta = "si";

            while (rta == "si")
            {
                Random random = new Random();
                int aleatorio = random.Next(0, 100);
                /*Console.WriteLine(aleatorio)*/

                Console.WriteLine("¡¡¡Adivina el numero random!!!");
                int numero;
                int intentos = 0;

                do
                {
                    intentos++;
                    Console.WriteLine();
                    Console.WriteLine("Ingrese un numero entre 0 y 100:");
                    numero = int.Parse(Console.ReadLine());

                    if (numero == aleatorio)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Asertaste!");
                        break;
                    }
                    else if (numero > aleatorio)
                    {
                        Console.WriteLine($"El numero es menor a {numero}");
                    }
                    else
                    {
                        Console.WriteLine($"El numero es mayor a {numero}");
                    }
                } while (numero != aleatorio);

                switch (intentos)
                {
                    case 1:
                        Console.WriteLine("¡¡¡Le pegaste a la primera sos alto crack papá!!!");
                        break;
                    case 2:
                        Console.WriteLine("Juju le pegaste al segundo intento nashe");
                        break;
                    case 3:
                        Console.WriteLine("Bien le pegaste al 3er intento maestro");
                        break;
                    default:
                        Console.WriteLine($"Fin del juego, intentos: {intentos}");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("¿Deseas volver a jugar?");
                rta = Console.ReadLine();
            }
        }
    }
}