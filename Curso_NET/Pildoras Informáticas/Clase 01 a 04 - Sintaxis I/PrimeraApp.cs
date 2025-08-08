// C# es case sensitive --> main <> a Main 
// { ... } --> Bloque 
// Una linea de codigo se le llama sentencia o instrucciones, y terminan en ; excepto los métodos, clases, condicional o de un bucle.
// C# es estricto con la sintaxis
// intellisense* --> El entorno de desarrollo proporciona ayudas durante la programacion. *EL SIMBOLO DE LA LLAVE ES UNA RECOMENDACION/AYUDA 
using System; // System e sun paquete que contiene clases predefinidas, entres ellas Console.

namespace PrimeraAplicacion // Permite tener 2 o mas clases iguales en un mismo programa <=> están en namespaces diferentes 
{
    class Program // Program: Clase propia, ademas es un identificador, como tambien lo es PrimeraAplicacion. INPORTANTE: No se pueden usar palabras reservadas para los identificadores, las palabras reservadas son las que se pintan de azul (using, namespace, static, void, etc...).
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenidos a C#"); // Console: Clase predefinida --> Biblioteca de clases o Api 
            
            int edad;
            edad = 28;

            Console.WriteLine(edad);
        }
    }
}
/*
    Flujo típico:
        Escribes tu código.
        Compilas para verificar que no haya errores de sintaxis.
        Si todo compila bien, ejecutas el programa en modo depuración para identificar problemas de lógica o comportamiento.
*/

/*
    Buenas prácticas_
        - Dejar un espacio al inicio de un bloque.
        - tabular cuando un bloque un bloque dentro de otro.
 */