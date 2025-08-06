using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador.State.Socio
{
    public class EstadoEliminado : IEstadoSocio
    {
        public string Nombre => "Eliminado";

        public bool ActualizarEstadoSocio(Modelo.Socio unSocio, SocioDAO socioDAO)
        {
            return false;
        }

        public bool VerificarSuspension(Modelo.Socio socio, out string mensaje, SocioDAO socioDAO)
        {
            if (socio.FechaFinActividades.HasValue && socio.FechaFinActividades.Value.AddDays(30) > DateTime.Now.Date)
            {
                // Todavía no pasaron 30 días desde el vencimiento → se puede suspender
                socio.EstadoSocio = "Suspendido";
                bool actualizado = socioDAO.ActualizarEstadoSocio(socio.IdSocio, "Suspendido");
                mensaje = actualizado ? "Estado cambiado a Suspendido." : "No se pudo actualizar el estado.";
                return actualizado;
            }
            else
            {
                // Pasaron más de 30 días → no se puede volver a suspender
                mensaje = "No se puede cambiar a estado Suspendido porque pasaron más de 30 días desde el vencimiento. " +
                    "El socio debe renovar la cuota.";
                return false;
            }
        }
    }
}
