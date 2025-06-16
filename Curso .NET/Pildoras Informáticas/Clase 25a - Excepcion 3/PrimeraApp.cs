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
                int numero = int.MaxValue;

                int resultado = checked(numero + 20); // Forma simplificada
                // Importante, si configuramos el compilador para que el checked se haga automaticamente lo que puede pasar es que queramos que SI se genera el desbordamiento, es decir que no se capture el error y que el compilador lo corrija, entonces lo que vamos a usar es unchecked( ... ) y NO checked( ... ).
                // SOLO FUNCIONA CON int y long.    

                Console.WriteLine(resultado);
            } catch (OverflowException OE)
            {
                Console.WriteLine($"Ahora con checked puedo capturar el error: {OE.Message}");
            }
            // Si compilamos con depuracion el programa se cierra si sucede una, para configurar que excepcion permite la interrrupcion al depurar ir a Depurar -> Ventanas -> Configuracion de Excepciones. Por lo tanto si usamos checked y se lanza el error podemos frenar el programa al depurar, sino usamos checked y tenemos la interrucion habilitada al depurar, no se capturara el error y por lo tanto no se interrupmira el programa.
        }   
    }
}