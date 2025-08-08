using System;
using System.Collections.Concurrent;
using System.Threading.Channels;
using System.Timers;
using System.Xml;

namespace AprendiendoStruct
{
    enum Estaciones
    {
        Primavera,
        Verano,
        Otoño,
        Invierno
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Enum sin valor interno
            Estaciones haceFrio = Estaciones.Invierno;

            Console.WriteLine(haceFrio);

            Console.WriteLine();

            // Enum con valor interno
            Bonus Antonio = Bonus.bajo;

            Console.WriteLine(Antonio);

            Console.WriteLine((double)Antonio); // Notar como ahora si muestra el valor

            double bonusAntonio = (double)Antonio; // Puedo hacer operaciones con este

            double salarioAntonio = 1500 + bonusAntonio;

            Console.WriteLine(salarioAntonio);

            // El comportamiento predeterminado de los enums al imprimirlos o utilizarlos como cadenas es mostrar su nombre textual porque son más descriptivos y legibles. Si deseas trabajar con el valor numérico asociado, necesitas convertir explícitamente el enum a su tipo base (generalmente int, o double si haces una conversión explícita).

            // Aplicando Enum en objetos
            Empleado Juan = new Empleado(Bonus.extra, 1900.50);

            Console.WriteLine("El salario del empleado es: " + Juan.getSalario());
        }
    }

    // Declaración de la clase Empleado
    class Empleado
    {
        // Atributos de la clase
        private double bonus; // Almacenará el bonus del empleado
        private double salario; // Almacenará el salario base del empleado

        // Constructor de la clase Empleado
        public Empleado(Bonus bonusEmpleado, double salario)
        {
            // Conversión del enum Bonus a double para obtener su valor subyacente
            bonus = (double)bonusEmpleado;

            // Asignación del salario recibido como parámetro al atributo salario
            this.salario = salario;
        }

        // Método público para obtener el salario total del empleado
        public double getSalario()
        {
            // Devuelve la suma del salario base y el bonus
            return salario + bonus;
        }
    }
    // Los enum son útiles cuando necesitas trabajar con un conjunto fijo de valores relacionados, como estados, categorías o niveles. Mejoran la legibilidad, evitan errores, y hacen que tu código sea más fácil de mantener y entender. ¡Por eso son una herramienta fundamental en la programación!
    enum Bonus { bajo = 500, normal = 1000, bueno = 1500, extra = 3000 }

}