using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;

using Modelo;
using Controlador;

namespace Vista
{
    public partial class frmMenuNegocio : Form
    {
        #region "Métodos"
        // Método que convierte un array de bytes en una imagen
        public System.Drawing.Image ByteToImage(byte[] imageBytes)
        {
            // Crea un flujo de memoria
            MemoryStream ms = new MemoryStream();

            // Escribe los bytes de la imagen en el flujo de memoria
            ms.Write(imageBytes, 0, imageBytes.Length);

            // Crea un objeto Bitmap a partir del flujo de memoria
            System.Drawing.Image image = new Bitmap(ms);

            // Retorna la imagen convertida
            return image;
        }

        public void ActualizarRangosHorariosSegunHorario(TimeSpan aperturaLaV, TimeSpan cierreLaV, TimeSpan aperturaSabado, TimeSpan cierreSabado)
        {
            List<RangoHorario> rangos = new ControladorGymRangoHorario().Listar(); // trae TODOS los rangos

            foreach (RangoHorario rh in rangos)
            {
                bool esSoloSabado = false;
                bool estaActivo = false;

                /*
                // Rango dentro de LaV
                if (rh.HoraDesde >= aperturaLaV && rh.HoraHasta <= cierreLaV)
                {
                    estaActivo = true;
                }

                // Rango dentro de Sábado
                if (rh.HoraDesde >= aperturaSabado && rh.HoraHasta <= cierreSabado)
                {
                    esSoloSabado = true;
                }
                */

                // Validamos que el rango sea lógico (no cruzado)
                bool rangoValido = rh.HoraDesde < rh.HoraHasta;

                if (!rangoValido)
                {
                    estaActivo = false;
                    esSoloSabado = false;
                }
                else
                {
                    // Rango válido dentro de lunes a viernes
                    if (rh.HoraDesde >= aperturaLaV && rh.HoraHasta <= cierreLaV)
                    {
                        estaActivo = true;
                    }

                    // Rango válido dentro de sábado
                    if (rh.HoraDesde >= aperturaSabado && rh.HoraHasta <= cierreSabado)
                    {
                        esSoloSabado = true;
                    }
                }

                // Actualizar el RangoHorario
                new ControladorGymRangoHorario().ActualizarEstadoYRango(rh.IdRangoHorario, estaActivo, esSoloSabado);
            }
        }

        #endregion
        public frmMenuNegocio()
        {
            InitializeComponent();
        }

        private void frmMenuNegocio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;

            byte[] byteimage = new ControladorGymGimnasio().ObtenerLogo(out obtenido);

            if (obtenido)
            {
                picLogo.Image = ByteToImage(byteimage);
            }

            Gimnasio datos = new ControladorGymGimnasio().ObtenerDatos();

            txtNombre.Text = datos.NombreGimnasio;
            txtTelefono.Text = datos.Telefono;
            txtDireccion.Text = datos.Direccion;
            nudAperturaLaV.Value = datos.HoraAperturaLaV.Hours;
            nudCierreLaV.Value = datos.HoraCierreLaV.Hours;
            nudAperturaS.Value = datos.HoraAperturaSabado.Hours;
            nudCierreS.Value = datos.HoraCierreSabado.Hours;
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            OpenFileDialog oOpenFileDialog = new OpenFileDialog();

            oOpenFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png";

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] byteimage = File.ReadAllBytes(oOpenFileDialog.FileName);

                bool respuesta = new ControladorGymGimnasio().ActualizarLogo(byteimage, out mensaje);

                if (respuesta)
                    picLogo.Image = ByteToImage(byteimage);
                else
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            // Primero obtenemos los horarios seleccionados desde los NumericUpDown
            TimeSpan aperturaLaV = new TimeSpan((int)nudAperturaLaV.Value, 0, 0);
            TimeSpan cierreLaV = new TimeSpan((int)nudCierreLaV.Value, 0, 0);
            TimeSpan aperturaS = new TimeSpan((int)nudAperturaS.Value, 0, 0);
            TimeSpan cierreS = new TimeSpan((int)nudCierreS.Value, 0, 0);

            // Creamos el objeto Gimnasio con todos los datos
            Gimnasio obj = new Gimnasio()
            {
                NombreGimnasio = txtNombre.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text,
                HoraAperturaLaV = aperturaLaV,
                HoraCierreLaV = cierreLaV,
                HoraAperturaSabado = aperturaS,
                HoraCierreSabado = cierreS
            };

            // Guardamos en la base de datos
            bool respuesta = new ControladorGymGimnasio().GuardarDatos(obj, out mensaje);

            if (respuesta)
            {
                MessageBox.Show("Los cambios fueron guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Actualizamos los rangos horarios activos según los nuevos horarios
                ActualizarRangosHorariosSegunHorario(aperturaLaV, cierreLaV, aperturaS, cierreS);
            }
            else
            {
                MessageBox.Show("No se pudo guardar los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = "Reporte" + txtNombre.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".pdf";
            
            string paginahtml_texto = Properties.Resources.plantilla.ToString();
            paginahtml_texto = paginahtml_texto.Replace("@NOMBRE",(txtNombre.Text).ToUpper());
            paginahtml_texto = paginahtml_texto.Replace("@DIRECCION",txtDireccion.Text);
            paginahtml_texto = paginahtml_texto.Replace("@TELEFONO",txtTelefono.Text);
            
            paginahtml_texto = paginahtml_texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));

                    using (StringReader sr = new StringReader(paginahtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }
            }
        }
    }
}
