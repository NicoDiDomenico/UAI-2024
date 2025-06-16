using System;
using System.Collections.Generic;

namespace AprendiendoColecciones
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> edades = new Dictionary<string, int>();

            // Rellenar el diccionario
            edades.Add("Juan", 18);
            edades.Add("Diandra", 35);
            edades["María"] = 25;
            edades["Antonio"] = 29;

            // Recorrer el diccionario
            foreach (KeyValuePair<string, int> persona in edades)
            {
                Console.WriteLine("Nombre: {0} Edad: {1}", persona.Key, persona.Value);
            }
        }
    }
}