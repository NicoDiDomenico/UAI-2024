using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Detalle_Compra
    {
        public int IdDetalleCompra { get; set; }

        // Por la logica que tiene el programa en vez de hacer 'public Detalle_Compra oDetalle_Compra { get; set; }' en esta clase, se asigna 'public List<Detalle_Compra> oDetalle_Compra {  get; set; }' en la clase Compra.
        public Producto oProducto { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal MontoTotal { get; set; }
        public string FechaRegistro { get; set; }

    }
}
