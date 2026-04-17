using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador.State.Socio
{
    public class EstadoNuevo : IEstadoSocio
    {
        public string Nombre => "Nuevo";

        public bool ActualizarEstadoSocio(Modelo.Socio socio, SocioDAO objcd_Socio)
        {
            if (socio.FechaFinActividades <= DateTime.Now.Date)
            {
                if (socio.FechaFinActividades.Value.AddDays(30) <= DateTime.Now.Date)
                {
                    socio.EstadoSocio = "Eliminado";
                    return objcd_Socio.ActualizarEstadoSocio(socio.IdSocio, socio.EstadoSocio);
                }
                else
                {
                    socio.EstadoSocio = "Suspendido";
                    return objcd_Socio.ActualizarEstadoSocio(socio.IdSocio, socio.EstadoSocio);
                }
            } else return false;
        }

        public bool VerificarSuspension(Modelo.Socio socio, out string mensaje, SocioDAO socioDAO)
        {
            mensaje = "El socio no está en estado Eliminado, no es necesario revertir.";
            return false;
        }
    }
}
