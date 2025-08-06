using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador.State.Socio
{
    public class EstadoSuspendido : IEstadoSocio
    {
        public string Nombre => "Suspendido";
        public bool ActualizarEstadoSocio(Modelo.Socio socio, SocioDAO objcd_Socio)
        {
            return false;
        }

        public bool VerificarSuspension(Modelo.Socio socio, out string mensaje, SocioDAO socioDAO)
        {
            mensaje = "El socio no está en estado Eliminado, no es necesario revertir.";
            return false;
        }
    }
}
