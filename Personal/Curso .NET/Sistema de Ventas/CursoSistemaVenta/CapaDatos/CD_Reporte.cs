using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public List<ReporteCompra> Compra(string fechainicio, string fechafin, int idproveedor)
        {
            List<ReporteCompra> lista = new List<ReporteCompra>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    SqlCommand cmd = new SqlCommand("sp_ReporteCompras", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.Parameters.AddWithValue("idproveedor", idproveedor);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteCompra()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoProveedor = dr["DocumentoProveedor"].ToString(),
                                RazonSocial = dr["RazonSocial"].ToString(),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                PrecioCompra = dr["PrecioCompra"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["SubTotal"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ReporteCompra>();
                }
            }
            return lista;
        }
        
        public List<ReporteVenta> Venta(string fechainicio, string fechafin)
        {
            // Se inicializa la lista que almacenará los reportes de ventas
            List<ReporteVenta> lista = new List<ReporteVenta>();

            // Se establece la conexión con la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se construye la consulta SQL
                    StringBuilder query = new StringBuilder();
                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Se abre la conexión
                    oconexion.Open();

                    // Se ejecuta la consulta y se lee el resultado
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Se agregan los datos obtenidos a la lista de reportes de ventas
                            lista.Add(new ReporteVenta()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["SubTotal"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ReporteVenta>();
                }
            }
            return lista;
        }

        // De esta forma no me anda:
        /*public List<ReporteVenta> Venta(string fechainicio, string fechafin)
        {
            // Se inicializa la lista que almacenará los reportes de ventas
            List<ReporteVenta> lista = new List<ReporteVenta>();

            // Se establece la conexión con la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se construye la consulta SQL
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select v.IdVenta,u.NombreCompleto,");
                    query.AppendLine("v.DocumentoCliente,v.NombreCliente,");
                    query.AppendLine("v.TipoDocumento,v.NumeroDocumento,");
                    query.AppendLine("v.MontoPago,v.MontoCambio,v.MontoTotal,");
                    query.AppendLine("convert(char(10),v.FechaRegistro,103)[FechaRegistro]");
                    query.AppendLine("from VENTA v");
                    query.AppendLine("inner join USUARIO u on u.IdUsuario = v.IdUsuario");
                    query.AppendLine("where CONVERT(date,v.FechaRegistro) between @fechainicio and @fechafin");

                    // Se crea el comando SQL con la consulta y la conexión
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("@fechafin", fechafin);
                    cmd.CommandType = CommandType.Text;

                    // Se abre la conexión
                    oconexion.Open();

                    // Se ejecuta la consulta y se lee el resultado
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Se agregan los datos obtenidos a la lista de reportes de ventas
                            lista.Add(new ReporteVenta()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["SubTotal"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // En caso de error, se devuelve una lista vacía
                    lista = new List<ReporteVenta>();
                }
            }

            // Se retorna la lista con los datos de las ventas obtenidas
            return lista;
        }*/
    }
}
