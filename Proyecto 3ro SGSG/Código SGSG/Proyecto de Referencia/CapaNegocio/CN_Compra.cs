using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;


using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Compra
    {
        private CD_Compra objcd_compra = new CD_Compra();

        public int ObtenerCorrelativo()
        {
            return objcd_compra.ObtenerCorrelativo();
        }

        public bool Registrar(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {
            return objcd_compra.Registrar(obj, DetalleCompra, out Mensaje);
        }

        public Compra ObtenerCompra(string numero)
        {
            // Se obtiene la compra a partir del número proporcionado
            Compra oCompra = objcd_compra.ObtenerCompra(numero);

            // Si la compra tiene un IdCompra válido, se obtienen sus detalles
            if (oCompra.IdCompra != 0)
            {
                List<Detalle_Compra> oDetalleCompra = objcd_compra.ObtenerDetalleCompra(oCompra.IdCompra);
                oCompra.oDetalleCompra = oDetalleCompra; // Se asignan los detalles de la compra al objeto de compra
            }

            // Se devuelve el objeto compra con los detalles si existen
            return oCompra;
        }

    }
}
