using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaLINQ
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Email { get; set; }
        public bool Activo { get; set; }
        public string Ciudad { get; set; } = "";
        public DateTime FechaAlta { get; set; }
    }

    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
    }

    public class LineaPedido
    {
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public static class DemoLinq
    {
        public static void Main()
        {
            // =========================
            // DATASET (para practicar)
            // =========================
            var clientes = new List<Cliente>
        {
            new Cliente { Id=1, Nombre="Nico",  Apellido="Di Domenico", Email="nico@mail.com", Activo=true,  Ciudad="Rosario", FechaAlta=new DateTime(2025, 5, 10) },
            new Cliente { Id=2, Nombre="Lau",   Apellido="Mandrile",    Email=null,           Activo=true,  Ciudad="Córdoba", FechaAlta=new DateTime(2024,11, 2) },
            new Cliente { Id=3, Nombre="Est",   Apellido="Bugari",      Email="est@mail.com", Activo=false, Ciudad="Rosario", FechaAlta=new DateTime(2023, 7, 15) },
            new Cliente { Id=4, Nombre="Nico2", Apellido="Mandrile",    Email="n2@mail.com",  Activo=true,  Ciudad="Buenos Aires", FechaAlta=new DateTime(2026, 1, 1) },
        };

            var pedidos = new List<Pedido>
        {
            new Pedido { Id=100, ClienteId=1, Fecha=new DateTime(2026,1, 5) },
            new Pedido { Id=101, ClienteId=1, Fecha=new DateTime(2025,12,20) },
            new Pedido { Id=102, ClienteId=3, Fecha=new DateTime(2024, 3, 1) },
        };

            var productos = new List<Producto>
        {
            new Producto { Id=10, Nombre="Creatina" },
            new Producto { Id=11, Nombre="Proteína" },
            new Producto { Id=12, Nombre="Barra" },
        };

            var lineas = new List<LineaPedido>
        {
            new LineaPedido { PedidoId=100, ProductoId=10, Cantidad=1, PrecioUnitario=20000m },
            new LineaPedido { PedidoId=100, ProductoId=12, Cantidad=3, PrecioUnitario=2500m  },
            new LineaPedido { PedidoId=101, ProductoId=11, Cantidad=1, PrecioUnitario=30000m },
            new LineaPedido { PedidoId=102, ProductoId=12, Cantidad=2, PrecioUnitario=2400m  },
        };

            // ==========================================================
            // 1) Clientes activos (Where)
            // ==========================================================
            var q1_query =
                from c in clientes
                where c.Activo
                select c;

            var q1_method = clientes.Where(c => c.Activo);

            // ==========================================================
            // 2) Nombres de clientes activos (Where + Select)
            // ==========================================================
            var q2_query =
                from c in clientes
                where c.Activo
                select c.Nombre;

            var q2_method = clientes
                .Where(c => c.Activo)
                .Select(c => c.Nombre);

            // ==========================================================
            // 3) Clientes ordenados por Apellido y Nombre (OrderBy + ThenBy)
            // ==========================================================
            var q3_query =
                from c in clientes
                orderby c.Apellido, c.Nombre
                select c;

            var q3_method = clientes
                .OrderBy(c => c.Apellido)
                .ThenBy(c => c.Nombre); // Si los apellidos son iguales el ThenBy entra en acción y ordena a esas personas específicas por su Nombre.

            // ==========================================================
            // 4) Top 3 clientes más nuevos (OrderByDescending + Take)
            // ==========================================================
            var q4_query =
                (from c in clientes
                 orderby c.FechaAlta descending
                 select c).Take(3);

            var q4_method = clientes
                .OrderByDescending(c => c.FechaAlta)
                .Take(3);

            // ==========================================================
            // 5) ¿Hay algún cliente de Rosario? (Any)
            // ==========================================================
            var q5_query = (from c in clientes
                            where c.Ciudad == "Rosario"
                            select c).Any();

            var q5_method = clientes.Any(c => c.Ciudad == "Rosario");

            // ==========================================================
            // 6) ¿Todos los clientes tienen email? (All)
            // ==========================================================
            var q6_query = (from c in clientes
                            select c).All(c => !string.IsNullOrWhiteSpace(c.Email));

            var q6_method = clientes.All(c => !string.IsNullOrWhiteSpace(c.Email));

            // ==========================================================
            // 7) Cantidad de pedidos por cliente (GroupBy + Count)
            //     Resultado: { ClienteId, CantidadPedidos }
            // ==========================================================
            var q7_query =
                from p in pedidos
                group p by p.ClienteId into g
                select new { ClienteId = g.Key, CantidadPedidos = g.Count() };

            var q7_method = pedidos
                .GroupBy(p => p.ClienteId)
                .Select(g => new { ClienteId = g.Key, CantidadPedidos = g.Count() });

            // ==========================================================
            // 8) Total gastado por cliente (GroupBy + Sum)
            //     Usamos: Pedido -> Lineas -> calcular subtotal -> agrupar por ClienteId
            // ==========================================================
            var q8_query =
                from p in pedidos
                join l in lineas on p.Id equals l.PedidoId
                group l by p.ClienteId into g
                select new
                {
                    ClienteId = g.Key,
                    TotalGastado = g.Sum(l => l.Cantidad * l.PrecioUnitario)
                };

            var q8_method = pedidos
                .Join(lineas,
                    p => p.Id,
                    l => l.PedidoId,
                    (p, l) => new { p.ClienteId, Linea = l })
                .GroupBy(x => x.ClienteId)
                .Select(g => new
                {
                    ClienteId = g.Key,
                    TotalGastado = g.Sum(x => x.Linea.Cantidad * x.Linea.PrecioUnitario)
                });

            // ==========================================================
            // 9) Pedidos con nombre de cliente (Join)
            //     Resultado: { PedidoId, ClienteNombreCompleto, Fecha }
            // ==========================================================
            var q9_query =
                from p in pedidos
                join c in clientes on p.ClienteId equals c.Id
                select new
                {
                    PedidoId = p.Id,
                    ClienteNombreCompleto = c.Apellido + ", " + c.Nombre,
                    p.Fecha
                };

            var q9_method = pedidos
                .Join(clientes,
                    p => p.ClienteId,
                    c => c.Id,
                    (p, c) => new
                    {
                        PedidoId = p.Id,
                        ClienteNombreCompleto = c.Apellido + ", " + c.Nombre,
                        p.Fecha
                    });

            // ==========================================================
            // 10) Clientes y su lista de pedidos (GroupJoin)
            //     Resultado: { Cliente, PedidosDelCliente }
            // ==========================================================
            var q10_query =
                from c in clientes
                join p in pedidos on c.Id equals p.ClienteId into pedidosDelCliente
                select new { Cliente = c, PedidosDelCliente = pedidosDelCliente };

            var q10_method = clientes
                .GroupJoin(pedidos,
                    c => c.Id,
                    p => p.ClienteId,
                    (c, pedidosDelCliente) => new { Cliente = c, PedidosDelCliente = pedidosDelCliente });

            // ==========================================================
            // 11) Clientes aunque no tengan pedidos (Left join)
            //     Resultado: { Cliente, Pedido (puede ser null) }
            // ==========================================================
            var q11_query =
                from c in clientes
                join p in pedidos on c.Id equals p.ClienteId into grp
                from p in grp.DefaultIfEmpty()
                select new { Cliente = c, Pedido = p }; // p puede ser null

            var q11_method = clientes
                .GroupJoin(pedidos,
                    c => c.Id,
                    p => p.ClienteId,
                    (c, grp) => new { Cliente = c, grp })
                .SelectMany(
                    x => x.grp.DefaultIfEmpty(),
                    (x, p) => new { Cliente = x.Cliente, Pedido = p });

            // ==========================================================
            // 12) Todas las líneas de pedido con nombre de producto
            //     (SelectMany + Join)
            //     Resultado: { PedidoId, Producto, Cantidad, PrecioUnitario, Subtotal }
            // ==========================================================
            // Query: no hay un "SelectMany" explícito, pero el doble 'from' hace lo mismo.
            var q12_query =
                from p in pedidos
                from l in lineas.Where(l => l.PedidoId == p.Id)
                join pr in productos on l.ProductoId equals pr.Id
                select new
                {
                    PedidoId = p.Id,
                    Producto = pr.Nombre,
                    l.Cantidad,
                    l.PrecioUnitario,
                    Subtotal = l.Cantidad * l.PrecioUnitario
                };

            // Method: SelectMany (pedido -> líneas) y después Join con productos
            var q12_method = pedidos
                .SelectMany(
                    p => lineas.Where(l => l.PedidoId == p.Id),
                    (p, l) => new { p.Id, Linea = l })
                .Join(productos,
                    x => x.Linea.ProductoId,
                    pr => pr.Id,
                    (x, pr) => new
                    {
                        PedidoId = x.Id,
                        Producto = pr.Nombre,
                        x.Linea.Cantidad,
                        x.Linea.PrecioUnitario,
                        Subtotal = x.Linea.Cantidad * x.Linea.PrecioUnitario
                    });

            // Si querés, acá podrías imprimir resultados para verlos:
            // Console.WriteLine(string.Join(", ", q2_method));
        }
    }
}
