namespace DibujarTriangulo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string simbolo;
            int alto, ancho;

            Console.WriteLine("¡¡¡Formando un triangulo!!!");

            Console.WriteLine("Ingrese un simbolo");

            simbolo = Console.ReadLine();

            Console.WriteLine("Ingrese un numero");

            alto = int.Parse(Console.ReadLine());
            ancho = alto;

            for (int i = 0; i < alto; i++)
            {
                for (int j = 0; j < ancho; j++)
                {
                    Console.Write(simbolo);
                }
                ancho--;
                Console.WriteLine();
            }

        }

    }
}
