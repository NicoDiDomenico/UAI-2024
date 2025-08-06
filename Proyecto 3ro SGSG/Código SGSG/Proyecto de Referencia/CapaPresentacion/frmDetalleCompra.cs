using CapaEntidad;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmDetalleCompra : Form
    {
        public frmDetalleCompra()
        {
            InitializeComponent();
        }

        private void frmDetalleCompra_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
        }

        // Probar con: 000001
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Se obtiene la compra basada en el número ingresado en txtbusqueda
            Compra oCompra = new CN_Compra().ObtenerCompra(txtBusqueda.Text);

            // Se verifica si la compra obtenida tiene un IdCompra válido
            if (oCompra.IdCompra != 0)
            {
                // Se asignan los valores de la compra a los controles del formulario
                txtNumeroDocumento.Text = oCompra.NumeroDocumento;
                txtFecha.Text = oCompra.FechaRegistro;
                txtTipoDocumento.Text = oCompra.TipoDocumento;
                txtUsuario.Text = oCompra.oUsuario.NombreCompleto;
                txtDocProveedor.Text = oCompra.oProveedor.Documento;
                txtNombreProveedor.Text = oCompra.oProveedor.RazonSocial;

                // Se limpia el DataGridView antes de cargar los nuevos datos
                dgvData.Rows.Clear();

                // Se recorren los detalles de la compra y se agregan al DataGridView
                foreach (Detalle_Compra dc in oCompra.oDetalleCompra)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        dc.oProducto.Nombre,  // Nombre del producto
                        dc.PrecioCompra,      // Precio de compra
                        dc.Cantidad,          // Cantidad comprada
                        dc.MontoTotal         // Monto total del producto en la compra
                    });
                }

                // Se muestra el monto total de la compra en el campo de texto
                txtMontoTotal.Text = oCompra.MontoTotal.ToString("0.00");
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Se limpian los campos del formulario asignándoles valores vacíos
            txtFecha.Text = "";            // Limpia la fecha de la compra
            txtTipoDocumento.Text = "";    // Limpia el tipo de documento
            txtUsuario.Text = "";          // Limpia el nombre del usuario
            txtDocProveedor.Text = "";     // Limpia el documento del proveedor
            txtNombreProveedor.Text = "";  // Limpia el nombre del proveedor

            // Se limpia el DataGridView eliminando todas sus filas
            dgvData.Rows.Clear();

            // Se asigna el valor "0.00" al campo de monto total para evitar valores nulos
            txtMontoTotal.Text = "0.00";
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            if (txtTipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_Html = Properties.Resources.PlantillaCompra.ToString();
            Negocio datos = new CN_Negocio().ObtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombronegocio", datos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", datos.RUC);
            Texto_Html = Texto_Html.Replace("@dirnegocio", datos.Direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txtTipoDocumento.Text.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtNumeroDocumento.Text);

            Texto_Html = Texto_Html.Replace("@docproveedor", txtDocProveedor.Text);
            Texto_Html = Texto_Html.Replace("@nomproveedor", txtNombreProveedor.Text);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtFecha.Text);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtUsuario.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["PrecioCompra"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }

            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", txtMontoTotal.Text);

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Compra_{0}.pdf", txtNumeroDocumento.Text);
            savefile.Filter = "Pdf Files|*.pdf";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    // Se crea un nuevo documento PDF con tamaño A4 y márgenes personalizados
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    // Se obtiene una instancia del escritor de PDF
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Variable para almacenar el logo del negocio
                    bool obtenido = true;
                    byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);

                    if (obtenido)
                    {
                        // Se crea una imagen a partir de los bytes obtenidos
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(60, 60); // Se ajusta el tamaño de la imagen
                        img.Alignment = iTextSharp.text.Image.UNDERLYING; // Se establece la alineación
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51)); // Se posiciona la imagen en el documento
                        pdfDoc.Add(img); // Se agrega la imagen al documento
                    }

                    // Se procesa el contenido HTML y lo convierte en PDF
                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    // Se cierra el documento y el flujo de datos
                    pdfDoc.Close();
                    stream.Close();

                    // Se muestra un mensaje indicando que el documento fue generado con éxito
                    MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
