using System;

namespace DibujarCuadrado
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = Convert.ToInt32(Console.ReadLine());
            int width = Convert.ToInt32(Console.ReadLine());

            for (int row = 0; row < width; row++)
            {
                for (int column = 0; column < width; column++)
                    Console.Write(x);

                Console.WriteLine();
            }
        }
    }
}
