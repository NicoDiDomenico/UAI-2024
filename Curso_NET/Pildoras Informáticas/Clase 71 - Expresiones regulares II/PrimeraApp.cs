using System;
using System.Text.RegularExpressions;

namespace ExpresionesRegulares
{
    class Program
    {
        static void Main(string[] args)
        {
            string txt = "cursos@pildorasinformaticas.es";

            string re1 = ".*?";    // Non-greedy match on filler
            string re2 = "(@)";    // Any Single Character 1
            string re3 = ".*?";    // Non-greedy match on filler
            string re4 = "(\\.)";  // Any Single Character 2

            Regex r = new Regex(re1 + re2 + re3 + re4, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);

            if (m.Success)
            {
                Console.WriteLine("Email correcto");
            }
            else
            {
                Console.WriteLine("Email no correcto");
            }

            // Para ver mas info ir a:
            /*
             -	https://learn.microsoft.com/es-es/dotnet/standard/base-types/regular-expression-language-quick-reference
             -	https://learn.microsoft.com/es-es/dotnet/api/system.text.regularexpressions.regex?view=net-8.0
             -  https://www.txt2regex.com/
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
