using System;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace LanzamientoExcepciones
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Introduce nº de mes");

            // Leer el número de mes ingresado por el usuario
            int NumeroMes = int.Parse(Console.ReadLine());

            try
            {
                // Llamar al método para obtener el nombre del mes
                Console.WriteLine(NombreDelMes(NumeroMes));
            }
            catch (ArgumentOutOfRangeException aoore) // no hace falta poner errorAOORE para delcarar la variable ya que no la usamos
            {
                // Manejar el caso en que el mes no sea válido
                Console.WriteLine("El número de mes introducido no es válido. Debe estar entre 1 y 12.");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(aoore); // Opcionalmente tambien puedo lanzar el error por consola
                // throw aoore; // Tambien puedo lanzar la excepcion pero no continuará con el codigo siguiente
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            }

            Console.WriteLine("Aquí continuaría la ejecución del resto del programa");
        }

        // Método para obtener el nombre del mes según su número
        public static string NombreDelMes(int mes)
        {
            switch (mes)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre"; // No hace falta el break si existe algo como el return o el throw.
                default:
                    // Lanza una excepción si el número está fuera del rango 1-12
                    ArgumentOutOfRangeException aoore = new ArgumentOutOfRangeException();
                    throw aoore;
                    /*
                    Buscarlas acá a las excepciones que mas se ajustan a lo que necesitamos:
                    https://learn.microsoft.com/en-us/dotnet/api/system?view=net-9.0
                    */
            }
        }
    }
}