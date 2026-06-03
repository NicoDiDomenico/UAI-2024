using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Modelo;

namespace Controlador.Observer
{
    public interface ICuotaObserver // Interfaz del Observador, todos los observers deben implementar este/estos método que tenga la interfaz
    {
        void NotificarRenovacion(Socio socio);
    }
}
