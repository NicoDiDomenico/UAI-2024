using System;
using System.Collections.Concurrent;
using System.Threading.Channels;
using System.Timers;

namespace AprendiendoStruct
{
    class Program
    {
        static void Main(string[] args)
        {
            // En C#, los struct (estructuras) son tipos por valor. Eso significa que cuando pasas un struct a otro lugar (por ejemplo, a una función o lo asignas a otra variable), lo que realmente se copia es el valor completo del struct, no una referencia al original.
            //Por otro lado, variables de tipos básicos como int, float, o bool también son tipos por valor. Si modificas una variable de tipo por valor, no afecta a la original porque se trabaja con una copia.
            
            Empleado empleado1 = new Empleado(1200, 250);

            empleado1.cambiaSalario(empleado1, 100); // Paso la copia por parametros NO la referencia

            // Si cambio directamente desde acá si modifico  el empleado original y no la copia. Al fin y al cabo le da el mismo comportamiento que una variable
            /*empleado1.salarioBase += 100;
            empleado1.comision += 100;*/

            Console.WriteLine(empleado1);
        }
    }
    // Declaración de una estructura (struct) llamada Empleado
    public struct Empleado
    {
        // Campos públicos de la estructura
        public double salarioBase, comision;

        // Constructor de la estructura
        // Inicializa los valores de salarioBase y comision
        public Empleado(int salarioBase, int comision)
        {
            // Asigna los valores pasados al constructor a los campos de la estructura
            this.salarioBase = salarioBase;
            this.comision = comision;
        }

        // Sobrescritura del método ToString
        // Permite obtener una representación en forma de texto de la estructura
        public override string ToString()
        {
            // Usa string.Format para devolver una cadena formateada con los valores actuales de salarioBase y comision
            return string.Format("Salario y comisión del empleado ({0},{1})", this.salarioBase, this.comision);
        }

        // Método para intentar cambiar el salario del empleado
        // Recibe una copia de un Empleado y un incremento
        public void cambiaSalario(Empleado emp, double incremento)
        {
            // Incrementa el salarioBase y la comision de la copia del empleado (NO afecta al original)
            emp.salarioBase += incremento;
            emp.comision += incremento;
            Console.WriteLine("Notar como si cambia los valores de la copia: " + emp.salarioBase + " " + emp.comision);
        }
    }


}