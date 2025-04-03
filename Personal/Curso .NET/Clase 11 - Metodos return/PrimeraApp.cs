using System;
namespace PrimeraAplicacion
{
    class Program
    {
        static void Main(string[] args) // Toda app en C# comienza con el metodo main siempre.
        // La palabra clave static se utiliza para declarar miembros de clase (como variables, métodos o propiedades) o clases que no dependen de una instancia específica de la clase. Esto significa que los miembros estáticos pertenecen a la clase en sí misma y no a los objetos creados a partir de ella.
        {
            double num1, res;
            int num2;

            Console.WriteLine("Ingresa un primer numero:");
            num1 = double.Parse(Console.ReadLine());

            Console.WriteLine("Ingresa un segudo numero:");
            num2 = int.Parse(Console.ReadLine());

            res = sumarNumeros(num1, num2); //**
       
            Console.WriteLine($"La suma de ambos numeros ingresados es {res}");
            Console.WriteLine($"La div de ambos numeros ingresados es {divNumeros(num1, num2)}");
        }

        static double sumarNumeros(double num1, int num2) //** --> Pasando parametros por valor 
        {
            return num1 + num2; // notar que al sumar un double y un int se retorna un double.
        }

        /* static double divNumeros(double num1, int num2) //** --> Pasando parametros por valor 
        {
            return num1 / num2; // notar que al sumar un double y un int se retorna un double.
        }*/ // Al ser un metodo tan sencilo lo declararemos de la siugiente manera en C#:
        static double divNumeros(double num1, int num2) => num1 / num2;
    }
}