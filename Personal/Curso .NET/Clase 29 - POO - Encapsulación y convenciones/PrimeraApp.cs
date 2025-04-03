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
            // IMPORTANTE: Ciculo () --> Es el construcotor, si bien en la clase no lo escribimos explicitamente, C# no coloca por default de la siguiente forma: public Circulo() { }

            Console.WriteLine(miCirculo.CalculoArea(5)); // gracias al public del metodo puedo acceder a él desde cualquier otra clase como esta que es program. --> Principio de Encapsulamiento

            // 2do Objeto:
            Circulo miCirculo2;
            miCirculo2 = new Circulo();
            Console.WriteLine(miCirculo2.CalculoArea(10));

            // Conversor Euro a Dolar:
            ConversorEuroDolar conversor = new ConversorEuroDolar(); // Declaro e Inicializo en la misma linea
            Console.WriteLine("200 Euros son " + conversor.Convierte(200) + " Dólares");

            Console.WriteLine("¿Quieres cambiar el valor del euro?");
            string rta = Console.ReadLine();
            if (rta == "si")
            {
                Console.WriteLine("Ingresa el valor del euro:");
                double valorNuevo = double.Parse(Console.ReadLine());
                conversor.cambiarValorEuro(valorNuevo);
            }

            Console.WriteLine("200 Euros son " + conversor.Convierte(200) + " Dólares");
        }
    }
    class Circulo
    {
        private const double pi = 3.1416;  // private hace que se encapsule pi y solo pueda leerse en el ambito que se encuentra, y const que no pueda modificar su valor.

        public double CalculoArea(int radio)  // método de clase. ¿Qué pueden hacer los objetos de tipo círculo?
        {
            return pi * radio * radio;
        }
    }

    class ConversorEuroDolar
    {
        private double euro = 1.231;

        public void cambiarValorEuro(double valor)
        {
            if (valor >= 0)
            {
                euro = valor;
            }
        }
        public double Convierte(double cantidad)
        {
            return cantidad* euro;
        }
    }
}