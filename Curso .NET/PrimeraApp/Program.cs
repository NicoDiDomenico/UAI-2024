// C# es case sensitive --> main <> a Main 
// { ... } --> Bloque 
// Una linea de codigo se le llama sentencia o instrucciones, y terminan en ; excepto los métodos, clases, condicional o de un bucle.
// C# es estricto con la sintaxis
// intellisense* --> El entorno de desarrollo proporciona ayudas durante la programacion. *EL SIMBOLO DE LA LLAVE ES UNA RECOMENDACION/AYUDA 
using System;

namespace PrimeraAplicacion // Permite tener 2 o mas clases iguales en un mismo programa <=> están en namespaces diferentes 
{
    class Program // Clase propia 
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenidos a C#"); // Clase predefinida --> Biblioteca / Api 
        }
    }
} 