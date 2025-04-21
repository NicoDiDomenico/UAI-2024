using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Nuevo fichero fuente al hacer: Proyecto -> Agregar Clase
// Ahora nos queda 2 ficheros fuente, a esto se le llama MODULARIZACION.
namespace ConceptosPOO
{
    class Punto
    {
        private int x, y;

        private static int contadorDeObjetos = 0;
        // private const int contadorDeObjetos = 0; --> Las constantes ya de por si son estaticas.
        public Punto()
        {
            this.x = 0;
            this.y = 0;

            contadorDeObjetos++;    
        }

        public Punto(int x, int y)
        {
            this.x = x; 
            this.y = y;

            contadorDeObjetos++;
        }

        public static void CantidadObjetos()
        {
            Console.WriteLine($"Esta clase ha creado {contadorDeObjetos} objetos.");
        }

        public double DistanciaHasta(Punto otroPunto)
        {
            int xDif = this.x - otroPunto.x;   

            int yDif = this.y - otroPunto.y;

            double distanciaPuntos = Math.Sqrt(Math.Pow(xDif, 2) + Math.Pow(yDif, 2));

            return distanciaPuntos;
        }
    }
}
