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
        public Punto(int x, int y)
        {
            Console.WriteLine($"Coordenada X: {x}, Coordenada Y: {y}");
            // Proyecto -> Compilar solucion. Asi depuramos todos los módulos.
        }

        public Punto() 
        {
            Console.WriteLine( "Este es el constructor por defecto");
        }
    }
}
