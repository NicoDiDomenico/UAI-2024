using System;
namespace PrimeraAplicacion
{
    class Program
    {
        static void Main(string[] args)
        {
            // Uso de constantes - no pueden cambia su valor durante la ejecucion del programa
            const int VALOR = 0; // Se deben declarar y definir en la misma linea (igual a las declaraciones implicitas). Como buena practica se ponen en mayusculas.

            const int VALOR2 = 1;

            int variable = 3;

            Console.WriteLine("El valor de la constante es: {0}", VALOR); // Notar como en C# tener que indicar enter {} la posicion del parametro.
            Console.WriteLine("Los valores de las constantes son {1} {2} y el de la variable {0}.", variable, VALOR, VALOR2);

            // Calculando el área de un círculo:
            const double PI = 3.1416;

            Console.WriteLine("Introduce la medida del radio");

            double radio = double.Parse(Console.ReadLine());

            // double area = radio * radio * PI;

            double area = Math.Pow(radio, 2) * PI; // Math es una clase estática que pertenece al espacio de nombres System. Proporciona un conjunto de métodos y constantes para realizar operaciones matemáticas comunes, como cálculos trigonométricos, logarítmicos, exponenciales, de redondeo y más.

            Console.WriteLine($"El área del círculo es: {area}");

        }
    }
}