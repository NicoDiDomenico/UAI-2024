using System;
namespace PrimeraAplicacion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(5/2); // como son enteros, devuelve entero
            Console.WriteLine(5.0 / 2.0); // ahora si devuelve en decimal (el compilador considera estos valores de tipo double)
            Console.WriteLine(5 / 2.0); // Si existe un decimal tendra prioridad
            Console.WriteLine(9 % 3); // nos devuelve el resto de una división 

            int edad = 19;

            edad++;

            string mensaje = "Tu edad es de " + edad;
            Console.WriteLine(mensaje);
            Console.WriteLine("Tu edad es de " + 19 + " años"); // El + con texto concatena/une

            // Se puede utiñizar "interpolacion" como reemplazo a la concatenacion con +:
            Console.WriteLine($"Tu edad es de {edad} años"); // Interpolacion: $"{variable}"

            edad = 19;

            // OJO CON EL FLUJO
            Console.WriteLine($"Tu edad es de {edad++} años"); // Acá uno esperaria que devuelva 20, pero como el flujo es de arriba abajo e izquierda derecha, primero escribe y despues incrementa.
            Console.WriteLine($"Tu edad es de {++edad} años"); // Este último incremento queda y se le aplica otro incremento antes de escribir en el console, ya que ahora si respetamos el flujo y asi ocurre lo que queremos --> QUE SE IMPRIMA EL INCREMENTO!!.
        }
    }
}