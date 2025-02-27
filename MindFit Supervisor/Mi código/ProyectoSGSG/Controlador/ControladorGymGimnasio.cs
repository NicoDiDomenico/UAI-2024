using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;
using DAO;

namespace Controlador
{
    public class ControladorGymGimnasio
    {
        private GimnasioDAO objcd_Gimnasio = new GimnasioDAO();

        public Gimnasio ObtenerDatos()
        {
            return objcd_Gimnasio.ObtenerDatos();
        }

        public bool GuardarDatos(Gimnasio obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.NombreGimnasio == "")
            {
                Mensaje += "Es necesario el nombre del Negocio\n";
            }

            if (obj.Telefono == "")
            {
                Mensaje += "Es necesario el numero de RUC del Negocio\n";
            }

            if (obj.Direccion == "")
            {
                Mensaje += "Es necesaria la direccion del Negocio\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Gimnasio.GuardarDatos(obj, out Mensaje);
            }
        }

        public byte[] ObtenerLogo(out bool obtenido)
        {
            return objcd_Gimnasio.ObtenerLogo(out obtenido);
        }

        public bool ActualizarLogo(byte[] imagen, out string mensaje)
        {
            return objcd_Gimnasio.ActualizarLogo(imagen, out mensaje);
        }
    }
}
