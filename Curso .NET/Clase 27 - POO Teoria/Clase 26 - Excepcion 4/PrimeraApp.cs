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
            System.IO.StreamReader archivo = null;

            try
            {
                string linea;
                int contador = 0;

                string path = @"C:\Users\Nicol\Desktop\UAI-2024\Curso .NET\Clase 26 - Excepcion 4\tirar.txt";

                archivo = new System.IO.StreamReader(path);

                while ((linea = archivo.ReadLine()) != null)
                {
                    Console.WriteLine(linea);
                    contador++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error con la lectura del archivo");
            }
            finally // Notar como se ejecuta igualmente sin importar si existe o no un error
            {
                if (archivo != null) archivo.Close();
                Console.WriteLine("Conexión con el fichero cerrada");
            }
        }
    }
}