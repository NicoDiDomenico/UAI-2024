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

using System.Windows.Forms.DataVisualization.Charting;
using FontAwesome.Sharp;

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

        private void AbrirFormulario(Form formulario)
        {
            formulario.StartPosition = FormStartPosition.CenterScreen; // Centra la ventana
            formulario.Load += (s, e) =>
            {
                // Ajustar la posición para que la ventana baje 50 píxeles desde la posición centrada
                formulario.Top += 155;
                formulario.Left += 276;
            };
            formulario.ShowDialog(); // Muestra como ventana modal
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
            txtCorreo.Text = datos.Email;
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
                Email = txtCorreo.Text, // <--- agregado
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
            guardar.FileName = "Reporte_" + txtNombre.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".pdf";

            // Cargamos la plantilla HTML
            string paginahtml_texto = Properties.Resources.plantilla.ToString();

            // Reemplazo de campos generales del gimnasio
            paginahtml_texto = paginahtml_texto.Replace("@NOMBRE", txtNombre.Text.ToUpper());
            paginahtml_texto = paginahtml_texto.Replace("@DIRECCION", txtDireccion.Text);
            paginahtml_texto = paginahtml_texto.Replace("@TELEFONO", txtTelefono.Text);
            paginahtml_texto = paginahtml_texto.Replace("@CORREO", txtCorreo.Text);
            paginahtml_texto = paginahtml_texto.Replace("@HORARIOLAV", $"{nudAperturaLaV.Value:00}:00 - {nudCierreLaV.Value:00}:00");
            paginahtml_texto = paginahtml_texto.Replace("@HORARIOSAB", $"{nudAperturaS.Value:00}:00 - {nudCierreS.Value:00}:00");
            paginahtml_texto = paginahtml_texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            // Guardamos el logo temporalmente
            string tempLogoPath = Path.Combine(Path.GetTempPath(), "logo_gym.png");
            picLogo.Image.Save(tempLogoPath, System.Drawing.Imaging.ImageFormat.Png);
            paginahtml_texto = paginahtml_texto.Replace("@LOGOPATH", tempLogoPath.Replace("\\", "/"));

            // --- BLOQUE NUEVO: Generar tabla con socios inactivos hace más de 30 días ---
            List<Socio> sociosInactivos = new ControladorGymSocio().ListarSociosInactivos(30); // <-- Asegurate de tener este método implementado

            string filasSociosInactivos = string.Empty;

            foreach (Socio s in sociosInactivos)
            {
                string fechaUltimoTurno = s.FechaUltimoTurno.HasValue
                    ? s.FechaUltimoTurno.Value.ToString("dd/MM/yyyy")
                    : "Sin turnos";

                filasSociosInactivos += $"<tr>" +
                                        $"<td>{s.NombreCompleto}</td>" +
                                        $"<td>{s.Email}</td>" +
                                        $"<td>{s.Estado}</td>" +
                                        $"<td>{fechaUltimoTurno}</td>" +
                                        $"</tr>";
            }

            paginahtml_texto = paginahtml_texto.Replace("@DETALLE_SOCIOS_INACTIVOS", filasSociosInactivos);

            // --- Generación del PDF ---
            if (guardar.ShowDialog() == DialogResult.OK)
            {
                //// --- Generar gráfico de estados dinámicamente ---
                Dictionary<string, int> conteoEstados = new ControladorGymSocio().ContarSociosPorEstado();

                var chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
                chart.Width = 600;
                chart.Height = 400;
                chart.BackColor = System.Drawing.Color.White;

                chart.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea());

                chart.Legends.Add(new Legend("EstadoSocio")
                {
                    Docking = Docking.Right,
                    Alignment = StringAlignment.Center
                });

                var series = new Series
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    Label = "#VAL (#PERCENT)", // Ej: 5 (25.0%)
                    LegendText = "#VALX" // Texto en la leyenda
                };

                foreach (var kvp in conteoEstados)
                {
                    series.Points.AddXY(kvp.Key, kvp.Value);
                }

                chart.Series.Add(series);

                // Guardar el gráfico en memoria
                MemoryStream chartImageStream = new MemoryStream();
                chart.SaveImage(chartImageStream, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                chartImageStream.Position = 0;

                // Convertirlo a imagen para iTextSharp
                iTextSharp.text.Image graficoEstadosImg = iTextSharp.text.Image.GetInstance(chartImageStream);
                graficoEstadosImg.ScaleToFit(400f, 300f);
                graficoEstadosImg.Alignment = Element.ALIGN_CENTER;
                ////

                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();

                    using (StringReader sr = new StringReader(paginahtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);

                        pdfDoc.Add(new Paragraph("\n")); // Espacio vertical

                        //// Título del gráfico
                        var fuenteTitulo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14f, iTextSharp.text.Font.BOLD);
                        var tituloGrafico = new Paragraph("Distribución de Socios por Estado", fuenteTitulo);
                        tituloGrafico.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(tituloGrafico);

                        // Insertar gráfico
                        pdfDoc.Add(graficoEstadosImg);
                        ////

                    }

                    pdfDoc.Close();
                    stream.Close();
                }

                MessageBox.Show("Reporte generado correctamente", "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAuditoria_Click(object sender, EventArgs e)
        {
            frmReporteAuditoria frmReporteAuditoria = new frmReporteAuditoria();

            // Abre el formulario modal con la ubicación deseada
            AbrirFormulario(frmReporteAuditoria);
        }
    }
}
