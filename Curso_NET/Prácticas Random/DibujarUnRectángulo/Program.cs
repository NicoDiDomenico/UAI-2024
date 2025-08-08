namespace DibujarUnRectángulo
{
    /*  Escriba un programa en C# que solicite un número (x) 
     *  y luego muestre un rectángulo de 3 columnas de ancho 
     *  y 5 filas de alto usando ese dígito.*/
    internal class Program
    {
        static void Main(string[] args)
        {
            int ancho = 3, alto = 5, numero = 0;

            Console.WriteLine("Ingrese un numero para dibujar un rectangulo de 3x5:");
            try
            {
                numero = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se ha ingresado un n° válido, se dibujará con 0.\n");
            }

            for (int i = 0; i < alto; i++)
            {
                for (int j = 0; j < ancho; j++) Console.Write(numero);

                Console.WriteLine();
            }
        }
    }
}
