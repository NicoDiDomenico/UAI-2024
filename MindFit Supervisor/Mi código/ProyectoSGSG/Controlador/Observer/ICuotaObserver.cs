using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Modelo;

namespace Controlador.Observer
{
    public interface ICuotaObserver
    {
        void NotificarRenovacion(Socio socio);
    }
}
