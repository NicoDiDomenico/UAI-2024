using System;
using System.Text.RegularExpressions;

namespace ExpresionesRegulares
{
    class Program
    {
        static void Main(string[] args)
        {
            // SAber si hay bloques con J
            string frase = "Mi nombre es Juan y mi nº de tfno es (+34)123-45-67 y mi código postal es 29679";

            string patron = "[J]";

            Regex miRegex = new Regex(patron);

            MatchCollection elMatch = miRegex.Matches(frase);

            if (elMatch.Count > 0)
                Console.WriteLine("Se ha encontrado una J");
            else
                Console.WriteLine("No se ha encontrado J");

            foreach (Match m in elMatch)
                elMatch.
            {
                Console.WriteLine($"Coincidencia: {m.Value}, en la posición {m.Index}");
            }

            // Saber si hay bloques numericos
            string patron2 = @"\d"; // La @ permite utilizar caracteres de escape dentro de un string sin que nos de error.

            Regex miRegex2 = new Regex(patron);

            MatchCollection elMatch2 = miRegex.Matches(frase);

            if (elMatch2.Count > 0)
                Console.WriteLine("Se ha encontrado números");
            else
                Console.WriteLine("No se ha encontrado números");

            // Saber si hay bloques con 3 numeros - 2 numeros - 2 numeros
            string patron3 = @"\d{3}-\d{2}-\d{2}";

            Regex miRegex3 = new Regex(patron);

            MatchCollection elMatch3 = miRegex.Matches(frase);

            if (elMatch3.Count > 0)
                Console.WriteLine("Se ha encontrado número de tfno");
            else
                Console.WriteLine("No se han encontrado números");


            // Para ver mas info ir a:
            /*
             -	https://learn.microsoft.com/es-es/dotnet/standard/base-types/regular-expression-language-quick-reference
             -	https://learn.microsoft.com/es-es/dotnet/api/system.text.regularexpressions.regex?view=net-8.0
            */

            // Estructura de una coleccion tipo MatchCollection:
            /*
             MatchCollection
                |
                +-- Match[0] (Primer coincidencia)
                |     - Value: Texto de la coincidencia
                |     - Index: Posición inicial de la coincidencia en el texto original
                |     - Length: Longitud del texto coincidente
                |     - Groups: Colección de grupos capturados (si los hay)
                |
                +-- Match[1] (Segunda coincidencia)
                |     - Value: Texto de la coincidencia
                |     - Index: Posición inicial de la coincidencia en el texto original
                |     - Length: Longitud del texto coincidente
                |     - Groups: Colección de grupos capturados (si los hay)
                |
                ...
                |
                +-- Match[N] (Última coincidencia)
             */
        }
    }
}
