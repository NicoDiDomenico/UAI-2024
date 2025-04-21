using System;
using System.Collections.Generic;

namespace DelegadosPredicadosLambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creación del objeto delegado apuntando a MensajeBienvenida
            ObjetoDelegado ElDelegado = new ObjetoDelegado(MensajeBienvenida.SaludoBienvenida);

            // Utilización del delegado para llamar al método SaludoBienvenida
            ElDelegado("Hola acabo de llegar");

            // Cambiar la referencia del delegado a MensajeDespedida
            ElDelegado = new ObjetoDelegado(MensajeDespedida.SaludoDespedida);

            // Utilización del delegado para llamar al método SaludoDespedida
            ElDelegado("Hola ya me voy");
        }

        // Definición del objeto delegado
        delegate void ObjetoDelegado(string msj);
    }

    class MensajeBienvenida
    {
        public static void SaludoBienvenida(string msj)
        {
            Console.WriteLine("Mensaje de bienvenida: {0}", msj);
        }
    }

    class MensajeDespedida
    {
        public static void SaludoDespedida(string msj)
        {
            Console.WriteLine("Mensaje de despedida: {0}", msj);
        }
    }


}