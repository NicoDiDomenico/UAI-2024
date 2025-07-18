using System;
using System.Collections.Generic;
using Modelo;

using Controlador.Observer;

namespace Controlador.Observer
{
    public class GestorRenovacionCuota
    {
        private readonly List<ICuotaObserver> observadores = new List<ICuotaObserver>();

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
