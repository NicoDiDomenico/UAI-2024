using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Collections;
using System.Runtime.Remoting;

namespace CapaDatos
{
    public class CD_Compra
    {
        public int ObtenerCorrelativo()
        {
            // Se inicializa la variable que almacenará el correlativo
            int idCorrelativo = 0;

            // Se establece la conexión con la base de datos utilizando una instancia de SqlConnection
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    /* Lo que hizo el profe:
                    // Se construye la consulta SQL utilizando StringBuilder
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT(*) + 1 FROM COMPRA"); // Cuenta la cantidad de registros en la tabla COMPRA y le suma 1
                    */

                    // Mi forma:
                    string query = @"
                        SELECT COUNT(*) + 1 
                        FROM COMPRA
                    ";

                    // Se crea el comando SQL a ejecutar
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text; // Se especifica que el comando es una consulta de texto

                    // Se abre la conexión a la base de datos
                    oConexion.Open();

                    // Se ejecuta la consulta y se obtiene el valor escalar convertido a entero
                    idCorrelativo = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    // En caso de error, se asigna 0 al correlativo
                    idCorrelativo = 0;
                }
            }

            // Se retorna el correlativo obtenido
            return idCorrelativo;
        }

        public bool Registrar(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCompra", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("IdProveedor", obj.oProveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("DetalleCompra", DetalleCompra);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    Respuesta = false;
                    Mensaje = ex.Message;
                }
            }

            return Respuesta;
        }

        // Mi Forma
        public Compra ObtenerCompra(string numero)
        {
            Compra obj = new Compra();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select c.IdCompra,
                               u.NombreCompleto,
                               pr.Documento, pr.RazonSocial,
                               c.TipoDocumento, c.NumeroDocumento, c.MontoTotal,
                               convert(char(10), c.FechaRegistro, 103) [FechaRegistro]
                        from COMPRA c
                        inner join USUARIO u on u.IdUsuario = c.IdUsuario
                        inner join PROVEEDOR pr on pr.IdProveedor = c.IdProveedor
                        where c.NumeroDocumento = @numero;
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.Parameters.AddWithValue("@numero", numero);
                    
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Compra()
                            {
                                IdCompra = Convert.ToInt32(dr["IdCompra"]),
                                oUsuario = new Usuario() { NombreCompleto = dr["NombreCompleto"].ToString() },
                                oProveedor = new Proveedor() { Documento = dr["Documento"].ToString(), RazonSocial = dr["RazonSocial"].ToString() },
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"]),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj = new Compra(); // Le envio un objeto vacio
                }
            }
            return obj;
        }

        // Del profe
        public List<Detalle_Compra> ObtenerDetalleCompra(int idcompra)
        {
            // Se crea una lista para almacenar los detalles de la compra
            List<Detalle_Compra> oLista = new List<Detalle_Compra>();

            try
            {
                // Se establece la conexión con la base de datos
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open(); // Se abre la conexión

                    // Se construye la consulta SQL para obtener los detalles de la compra
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.Nombre, dc.PrecioCompra, dc.Cantidad, dc.MontoTotal");
                    query.AppendLine("from DETALLE_COMPRA dc");
                    query.AppendLine("inner join PRODUCTO p on p.IdProducto = dc.IdProducto");
                    query.AppendLine("where dc.IdCompra = @idcompra");

                    // Se prepara el comando SQL
                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@idcompra", idcompra);
                    cmd.CommandType = CommandType.Text;

                    // Se ejecuta la consulta y se obtiene el resultado
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Se recorren los registros obtenidos
                        while (dr.Read())
                        {
                            // Se agrega cada detalle de compra a la lista
                            oLista.Add(new Detalle_Compra()
                            {
                                oProducto = new Producto() { Nombre = dr["Nombre"].ToString() },
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString())
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // En caso de error, se inicializa la lista vacía
                oLista = new List<Detalle_Compra>();
            }

            // Se retorna la lista con los detalles de la compra
            return oLista;
        }
    }
}
