using System;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace EjemplosPOO
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1er Objeto:
            Circulo miCirculo;  // Creación de objeto de tipo Circulo. Variable/objeto de tipo círculo.

            miCirculo = new Circulo();  // Iniciación de variable/objeto de tipo Circulo. Instanciar una clase.
                                        // Instanciación. Ejemplarización. Creación de ejemplar de clase.
                                        // Esto es una vatiable de tipo objeto y no primitivo como haciamos antes con double, int, etc..

            Console.WriteLine(miCirculo.calculoArea(5)); // gracias al public del metodo puedo acceder a él desde cualquier otra clase como esta que es program. --> Principio de Encapsulamiento
            
            // 2do Objeto:
            Circulo miCirculo2;
            miCirculo2 = new Circulo();
            Console.WriteLine(miCirculo2.calculoArea(10));
        }
    }
    class Circulo
    {
        const double pi = 3.1416;  // propiedad de la clase Circulo. Campo de clase.

        public double calculoArea(int radio)  // método de clase. ¿Qué pueden hacer los objetos de tipo círculo?
        {
            return pi * radio * radio;
        }
    }

}