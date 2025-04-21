using System;
using System.Runtime.Intrinsics.X86;


namespace Practica_Metodos
{
    class Program
    {
        // Contexto/Ambito/Alcance
        int numero1 = 5; // Variable con ambito de clase --> Campo / Campo de clase 
        static void Main(string[] args){ // Reordar las clases static no se pueden instanciar

            int numero2 = 7;
            Console.WriteLine(Suma(18, numero2)); // Cual metodo se ejecutara depemdera del tipo de parametros que le pasemos, ya que justamente es esto lo que diferenica a los metodos con el mismos nomre --> Sobrecarga
        }

        void primerMetodo()
        {
            Console.WriteLine(numero1);
        }

        void segundoMetodo()
        {
            Console.WriteLine(numero1);
        }

        // Sobrecarga --> Tener varios metodos con el mismo nombre
        // Forma lambda '=>'
        static int Suma(int numero1, int numero2, int parametroOpcional = 0) => numero1 + numero2 + parametroOpcional; // Antes se usaba parametros ocionales cuando no existia la sobrecarga
        static double Suma(int numero1, double numero2) => numero1 + numero2;
        static int Suma(int numero1, int numero2, int numero3, int numero4) => numero1 + numero2 + numero3 + numero4;
        

    }
}