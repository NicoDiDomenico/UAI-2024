namespace DibujarTriánguloNordeste
{
    /*Escriba un programa en C# que solicite un ancho y muestre un triángulo nordeste.
    Usa el carácter * para pintar el triángulo.*/
    internal class Program
    {
        static void Main(string[] args)
        {
            int alto = Convert.ToInt32(Console.ReadLine());
            int ancho = 0;
            int max = alto;

            for (int fila = 0; fila < alto; fila++)
            {
                for (int columna = 0; columna < ancho; columna++)
                {
                    Console.Write(" ");
                }

                for (int simbolos = 0; simbolos < max; simbolos++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();

                ancho++;
                max--;
            }
        }
    }
}
