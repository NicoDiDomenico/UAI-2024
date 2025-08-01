using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using Modelo;

namespace Controlador.State.Socio
{
    public interface IEstadoSocio
    {
        string Nombre { get; }

        bool ActualizarEstadoSocio(Modelo.Socio unSocio, SocioDAO socioDAO);
        bool VerificarSuspension(Modelo.Socio socio, out string mensaje, SocioDAO socioDAO);
    }
}
