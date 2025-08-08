using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Detalle_Venta
    {
        public int IdDetalleVenta { get; set; }

        // Por la logica que tiene el programa en vez de hacer 'public Detalle_Venta oDetalle_Venta { get; set; }' en esta clase, se asigna 'public List<Detalle_Venta> oDetalle_Venta {  get; set; }' en la clase Venta.
        public Producto oProducto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public string FechaRegistro { get; set; }

    }
}
