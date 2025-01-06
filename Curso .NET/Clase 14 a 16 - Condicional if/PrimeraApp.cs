using System;
using System.Runtime.Intrinsics.X86;


namespace Practica_Metodos
{
    class Program
    {
        static void Main(string[] args){
            // Tipo booleano
            bool haceFrio;
            haceFrio = true;

            Console.WriteLine(!haceFrio);

            // Condicional if
            int edad = 25;

            Console.WriteLine($"Vamos a evaluar si eres mayor de edad... {edad}");

            if (edad >= 18)
            {
                Console.WriteLine("Adelante, puedes pasar porque eres mayor de edad");
            } // no hace falta los {} si es una sola linea de codigo:
            Console.WriteLine("Ahora vamos a determinar si estas capacitado para manejar:");
            int edad2 = int.Parse(Console.ReadLine());
            bool carnet = true;

            if (carnet == true && edad2 >=18 && edad2 <= 85) Console.WriteLine("Podes manejar");
            else Console.WriteLine("No podes manejar!");
            Console.WriteLine("ADIOS");

            // Operador or usado en if:
            Console.WriteLine("Introduce el primer parcial");
            int parcial1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduce el segundo parcial");
            int parcial2 = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduce el tercer parcial");
            float parcial3 = float.Parse(Console.ReadLine());

            /*string mensaje;*/ //* Esto no se puede ahcer porque mensaje puede ser NULL si el if no es True
            string mensaje = "Vuelve en septiempre";
            if (parcial1 <= 5 || parcial2 <= 5 || parcial3 <= 5)
            {
                /*string mensaje = "Vuelve en septiempre";*/ // Esto no se puede nhacer porque mensaje solo existe en el bloque if y no en el else donde tambien se tiene que usar.
                /*mensaje = "Vuelve en septiempre";*/ //*
                Console.WriteLine("La nota media es " + (parcial1 + parcial2 + parcial3) / 3);
            }
            else
            {
                Console.WriteLine($"{mensaje}.");
            }


        }
    }
}