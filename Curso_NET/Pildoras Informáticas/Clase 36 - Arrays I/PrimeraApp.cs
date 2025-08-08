using System;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Math; // Ahora no necesitamos escribir la clase Math (No se recomienda hacer esto)

// Veremos Modularizacion
namespace UsoArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] edades; // Declaro
            
            edades = new int[4]; // Inicializo

            Console.WriteLine(edades[2]); // Notar que al inicializar se ponen en 0 ya que es un entero vacio
            
            edades[0] = 15;
            edades[1] = 27;
            edades[2] = 19;
            edades[3] = 80;

            Console.WriteLine(edades[2]); // Ahora si le asignamos un valor en la posicion 2

            // Sintaxis simplificada:
            int[] edades2 = { 1, 2, 3, 4 };
            int[] edades3 = new int[4] { 1, 2, 3, 4 }; // Forma mas clara - Es La Declaracion, Inicializacion,y Asignacion en la misma linea.

            Console.WriteLine(edades2[2]);
            Console.WriteLine(edades3[2]);
        }
    }
}