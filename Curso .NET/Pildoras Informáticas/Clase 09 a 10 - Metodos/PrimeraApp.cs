using System;
namespace PrimeraAplicacion
{
    class Program
    {
        static void mensajePantalla2()
        {
            Console.WriteLine("Chau");
        }

        static void Main(string[] args) // Toda app en C# comienza con el metodo main siempre.
        // La palabra clave static se utiliza para declarar miembros de clase (como variables, métodos o propiedades) o clases que no dependen de una instancia específica de la clase. Esto significa que los miembros estáticos pertenecen a la clase en sí misma y no a los objetos creados a partir de ella.
        {
            int num1, num2, res;

            mensajePantalla();
            Console.WriteLine("=)");
            Console.WriteLine("Ingresa un primer numero:");
            num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingresa un segudo numero:");
            num2 = int.Parse(Console.ReadLine());
            res = sumarNumeros(num1, num2); //**
            Console.WriteLine($"La suma de ambos numeros ingresados es {res}");
            mensajePantalla2();
        }

        static void mensajePantalla()
        {
            Console.WriteLine("Hola");
        }

        static int sumarNumeros(int num1, int num2) //** --> Pasando parametros por valor 
        {
            int rta;
            rta = num1 + num2;
            return rta;
        }       
    }
}