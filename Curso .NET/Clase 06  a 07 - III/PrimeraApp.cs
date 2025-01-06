using System;
namespace PrimeraAplicacion
{
    class Program
    {
        static void Main(string[] args)
        {
            // DECLARACIONES
            // Declaracion explicita de variables - Operador =
            int edadPersona1; 
            int edadPersona2;
            int edadPersona3;
            int edadPersona4;

            edadPersona1 = edadPersona2 = edadPersona3 = edadPersona4 = 27;

            Console.WriteLine(edadPersona2);
            
            // Declaracion implicita de variables - Se declarar y definen en la misma linea!
            var edadPersona = 37; // Le estamos diciendo al compilador que en tiempo de ejecucion asigne el tipo que corresponda a esa variable. ESTE TIPO SE DEBE RESPETAR A LO LARGO DEL PROGRAMA.
            Console.WriteLine(edadPersona = 27); // El tipo que se asigno se respeta.
            /* Console.WriteLine(edadPersona = 27.3); */ // NO SE RESPETA!!!
                                                         //Mas adelante veremos su utilidad.
            
            // CONVERSIONES
            // Conversiones implícitas e explícitas de variables
            double temperatura = 34.9;
            int temperaturaMadrid;

            // Conversión explícita - casting: .. = (nuevo tipo) ...
            temperaturaMadrid = (int)temperatura;

            Console.WriteLine(temperaturaMadrid);

            // Conversión implícita - hacemos la conversion sin el casting
            int habitantesCiudad = 1000000;
            long habitantesCiudad2018 = habitantesCiudad;

            Console.WriteLine(habitantesCiudad2018);

            // Conversion de texto a número - int.parse()
            Console.WriteLine("Introduce el primer número");
            int num1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduce el segundo número");
            int num2 = int.Parse(Console.ReadLine());

            Console.WriteLine("El resultado es " + (num1 + num2));
        }
    }
}