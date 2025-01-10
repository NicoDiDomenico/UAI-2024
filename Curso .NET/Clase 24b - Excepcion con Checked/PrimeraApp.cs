using System;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace UsoChecked
{
    class Program
    {
        static void Main(string[] args) {
            try
            {
                checked
                {
                    int numero = int.MaxValue;

                    int resultado = numero + 20;

                    Console.WriteLine(resultado);
                }
            } catch (OverflowException OE)
            {
                Console.WriteLine("Ahora con checked puedo capturar el error, cosa que antes no...");
            }
            int numero2 = int.MaxValue;

            int resultado2 = numero2 + 20;

            Console.WriteLine($"Al no capturar el error se muestra lo siguiente: {resultado2}");
            // En el min 17:38 de https://www.youtube.com/watch?v=vVRznsklCtI&list=PLU8oAlHdN5BmpIQGDSHo5e1r4ZYWQ8m4B&index=24 te muestra como configurar manualmente el compilador para no tener que implementar el checked y que se ejecute automaticamente.
        }
    }
}