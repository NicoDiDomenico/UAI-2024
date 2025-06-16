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
    }

    class Coche
    {
        private int ruedas;
        private double ancho;
        private double largo;
        private bool climatizador;
        private string tapiceria;

        public Coche()
        {
            ruedas = 4;
            ancho = 20;
            largo = 100;
        }

        public Coche(int setRuedas, double setAncho, double setLargo) // Sobrecarga de constructor
        {
            ruedas = setRuedas;
            ancho = setAncho;
            largo = setLargo;
        }
        public void getInfo()
        {
            Console.WriteLine($"Ruedas: {ruedas}, Ancho: {ancho}, Largo: {largo}");
        }
    }
}