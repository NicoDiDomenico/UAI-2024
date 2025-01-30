using System;
using System.Threading.Channels;
using System.Timers;

namespace ProyectoHerencia
{
    class Program
    {
        static void Main(string[] args)
        {
            Empleado Juan = new Empleado("Juan");

            Juan.SALARIO = 1200;
            Juan.SALARIO += 500;

            Console.WriteLine("El salario del empleado es: " + Juan.SALARIO);

            Juan.EDAD = 20;

            Console.WriteLine("Su edad es: " + Juan.EDAD);
        }

    }
    class Empleado
    {
        private double salario; // Hay una convencion de usar _salario para las propiedades

        private string nombre;

        public Empleado(string nombre)
        {
            this.nombre = nombre;
        }

        /*public void setSalario(double salario)
        {
            if (salario < 0)
            {
                Console.WriteLine("El salario no puede ser negativo. Se asignará 0 como salario");
                this.salario = 0;
            }
            else
            {
                this.salario = salario;
            }
        }

        public double getSalario()
        {
            return salario;
        }*/
        private double evaluaSalario(double salario)
        {
            if (salario < 0) return 0;
            else return salario;
        }

        // CREACIÓN DE PROPIEDAD - hace que una variable privada se use como publica sin violar sus reglas
        /*public double SALARIO
        {
            get { return this.salario; }
            set { this.salario = evaluaSalario(value); }
        }*/

        // Simplificacion:
        public double SALARIO
        {
            get => this.salario;
            set => this.salario = evaluaSalario(value);
        }

        // ULtra Simplificacion - No es necesario declarar
        public double EDAD { get; set; }
        // OJO: NO se puede aplicar si necesitas validaciones en set (como evaluaSalario(value)).
    }

}