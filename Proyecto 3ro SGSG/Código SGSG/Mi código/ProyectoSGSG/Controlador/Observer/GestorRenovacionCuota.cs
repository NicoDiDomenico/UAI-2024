using System;
using System.Collections.Generic;
using Modelo;

using Controlador.Observer;
using System.Collections;

namespace Controlador.Observer
{
    public class GestorRenovacionCuota // Sujeto
    {
        // Guarda una lista de observadores --> Notar como el patrón Observer se basa en polimorfismo en tiempo de ejecución (sobreescritura) 
        private readonly List<ICuotaObserver> observadores = new List<ICuotaObserver>();
        
        // Este método permite agregar a esa lista un nuevo “oyente”.
        public void AgregarObserver(ICuotaObserver observer)
        {
            observadores.Add(observer);
        }
        
        public void NotificarSolo(Socio socio)
        {
            foreach (var observer in observadores)
            {
                observer.NotificarRenovacion(socio);
            }
        }
    }
}
