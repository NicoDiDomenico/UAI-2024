using System;
using System.Collections.Generic;

namespace DelegadosPredicadosLambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            // Uso del delegado
            OperacionesMatematicas operation = new OperacionesMatematicas(Cuadrado);

            // Aplicando Lamda
            OperacionesMatematicas operation2 = new OperacionesMatematicas(n => n * n);

            Console.WriteLine(operation(4));

            Console.WriteLine();
            // Otro uso:
            List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            List<int> numerosPares = numeros.FindAll(i => i % 2 == 0);

            // numerosPares.ForEach(numero => Console.WriteLine(numero));

            numerosPares.ForEach(numero => { // Siempre que haya + de una linea de codigo se tendra queponer el codigo entre { ... }
                Console.WriteLine("El número par es: ");
                Console.WriteLine(numero);
            });

            Console.WriteLine();
            // Usando Objetos con lambda:
            Personas P1 = new Personas();
            P1.Nombre = "Juana";
            P1.Edad = 18;

            Personas P2 = new Personas();
            P2.Nombre = "María";
            P2.Edad = 28;

            /*ComparaPersonas comparaEdad = new ComparaPersonas((persona1, persona2) => persona1 == persona2);*/
            ComparaPersonas comparaEdad = (persona1, persona2) => persona1 == persona2; // Es igual a lo de arriba pero ahorrando codigo

            Console.WriteLine(comparaEdad(P1.Nombre, P2.Nombre));
    }

        public delegate int OperacionesMatematicas(int numero);

        public delegate bool ComparaPersonas(String n1, String n2);
        public static int Cuadrado(int num)
        {
            return num * num;
        }

    }

    class Personas
    {
        private string nombre;
        private int edad;

        public string Nombre { get => nombre; set => nombre = value; }
        public int Edad { get => edad; set => edad = value; }
    }
}