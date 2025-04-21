using System;
using System.Timers;

namespace UsoArray
{
    class Program
    {
        class Empleados
        {
            private string nombre;
            private int edad;
            public Empleados(string nombre, int edad)
            {
                this.nombre = nombre;
                this.edad = edad;
            }
            
            public void getNombreEdad()
            {
                Console.WriteLine($"Nombre: {this.nombre}, Edad: {this.edad}.");
            }
        }

        static void Main(string[] args)
        {
            // Array Forma completa:
            int[] edades = new int[4] { 1, 2, 3, 4 };

            // Array Implicito - Es muy flexible
            var arregloDatos = new[] { "Juan", "Diaz", "España"};

            // Muy parecido a las clases anonimas:
            var objetoDatos = new { nombre="Juan", apellido="Diaz", pais="España" };

            // var datos = new[] { "Juan", "Díaz", 15 }; Esto no anda porque no hay un tipo que contenga estos valores.

            var valores = new[] { 15, 28, 35, 75.5, 30.30 };  // Esto si anda porque se pasa todo a double ya que contiene a los int.

            // arrays de objetos
            Empleados[] arrayEmpleados = new Empleados[2];

            // Forma 1 --> Mejor forma
            arrayEmpleados[0] = new Empleados("Sara", 37);

            // Forma 2
            Empleados Ana = new Empleados("Ana", 27); 
            arrayEmpleados[1] = Ana;

            // Array de tipo clases anónimas - Todos deben tener el mismo tipo y nombre
            var alumnos = new[]
            {
                new {Nombre = "Juan", Edad= 19}, // Pos 0
                new {Nombre = "Pepe", Edad= 18}, // Pos 1
                new {Nombre = "Diana", Edad= 23}, // Pos 2
            };

            Console.WriteLine(alumnos[1]);

            // Forma Ascendente
            for (int i=0; i<=4; i++)
            {
                Console.WriteLine(valores[i]);
            }

            Console.WriteLine();

            // Forma Descendente
            for (int i = 4; i >= 0; i--)
            {
                Console.WriteLine(valores[i]);
            }

            Console.WriteLine();

            // Haciendo uso de las propiedades de los array:
            for (int i = 0; i < arrayEmpleados.Length; i++)
            {
                arrayEmpleados[i].getNombreEdad();
            }

            Console.WriteLine();

            // Bucle foreach - se usa para acceder a TODOS los elementos de un array sin necesidad de conocer su tamaño
            foreach (double v in valores)
            {
                Console.WriteLine(v);
            }

            Console.WriteLine();

            foreach (Empleados e in arrayEmpleados) // e es el iterador
            {
                e.getNombreEdad();
            };

            Console.WriteLine();
            Console.WriteLine("Arreglo de objetos Alumnos:");
            // Muy util para arrays de clases anonimas
            foreach (var a in alumnos) 
            {
                Console.WriteLine(a);
            };

        }
    }
}