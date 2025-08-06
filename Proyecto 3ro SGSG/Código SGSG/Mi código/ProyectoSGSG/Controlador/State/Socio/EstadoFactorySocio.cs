using Controlador.State.Turno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador.State.Socio
{
    public static class EstadoFactorySocio
    {
        public static IEstadoSocio ObtenerEstado(string estadoNombre)
        {
            switch (estadoNombre)
            {
                case "Nuevo":
                    return new EstadoNuevo();
                case "Suspendido":
                    return new EstadoSuspendido();
                case "Actualizado":
                    return new EstadoActualizado();
                case "Eliminado":
                    return new EstadoEliminado();
                default:
                    return null;
            }
        }
    }
}
