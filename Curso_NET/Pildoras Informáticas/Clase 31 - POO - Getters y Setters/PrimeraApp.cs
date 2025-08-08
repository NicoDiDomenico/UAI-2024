using System;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace UsoCoches;

class Program
{
    static void Main(string[] args)
    {
        Coche auto1 = new Coche();
        auto1.getInfo();

        Coche auto2 = new Coche(2, 1, 2);
        auto2.getInfo();

        auto2.setExtras(true, "Cuero");
        Console.WriteLine(auto2.getExtras());
    }
    // partial permite hacer divisiones de la misma clase para ordenar y sin perder la funcionlidad
    // Atributos:
    partial class Coche
    {
        private int ruedas;
        private double ancho;
        private double largo;
        private bool climatizador;
        private string tapiceria;
    }

    // Métodos:
    partial class Coche
    {
        public Coche()
        {
            ruedas = 4;
            ancho = 20;
            largo = 100;
            tapiceria = "tela";
        }

        public Coche(int setRuedas, double setAncho, double setLargo) // Sobrecarga de constructor
        {
            ruedas = setRuedas;
            ancho = setAncho;
            largo = setLargo;
            tapiceria = "tela";
        }
    
        public void getInfo()
        {
            Console.WriteLine($"Ruedas: {ruedas}, Ancho: {ancho}, Largo: {largo}");
        }

        public void setExtras(bool paramClimatizador, string tapiceria)
        {
            climatizador = paramClimatizador;
            this.tapiceria = tapiceria; // De esta forma con this resolveremos cuando el parametro se llama igual que la variable
        }

        public string getExtras()
        {
            return "Extras del coche: \n" + "Tapicería: " + tapiceria + ", Climatizador: " + climatizador;
        }

    }
}