using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    public partial class frmReporteNegocio : Form
    {
        #region "Variables"
        private List<Socio> _socios;
        private Dictionary<string, int> _conteoEstados;
        private dynamic _datos;
        #endregion


        public frmReporteNegocio(dynamic txts, List<Socio> socios, Dictionary<string, int> conteoEstados)
        {
            InitializeComponent();
            _datos = txts;
            _socios = socios;
            _conteoEstados = conteoEstados;
        }

        private void frmReporteNegocio_Load(object sender, EventArgs e)
        {
            // --- Título para el listado de socios ---
            lblTituloSocios.Text = "Socios Inactivos hace más de 30 días";
            lblTituloSocios.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);

            // --- Llenar el DataGridView ---
            dgvSociosInactivos.Rows.Clear();

            foreach (var s in _socios)
            {
                string fechaUltimoTurno = s.FechaUltimoTurno.HasValue
                    ? s.FechaUltimoTurno.Value.ToString("dd/MM/yyyy")
                    : "Sin turnos";

                dgvSociosInactivos.Rows.Add(
                    s.NombreCompleto,
                    s.Email,
                    s.Estado,
                    fechaUltimoTurno
                );
            }

            // --- Configurar gráfico ---
            chartEstados.Series.Clear();
            chartEstados.ChartAreas.Clear();
            chartEstados.Legends.Clear();
            chartEstados.Titles.Clear();

            // Título arriba del gráfico
            lblTituloChart.Text = "Distribución de Socios por Estado";
            lblTituloChart.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
            /*
            chartEstados.Titles.Add("Distribución de Socios por Estado");
            chartEstados.Titles[0].Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            */
            // Crear área del gráfico
            ChartArea area = new ChartArea();
            area.InnerPlotPosition = new ElementPosition(5, 5, 90, 90);
            chartEstados.ChartAreas.Add(area);

            // Leyenda a la derecha
            Legend legend = new Legend();
            legend.Docking = Docking.Right;
            legend.Alignment = StringAlignment.Center;
            legend.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Regular);
            chartEstados.Legends.Add(legend);

            // Crear serie
            Series serie = new Series();
            serie.ChartType = SeriesChartType.Pie;

            // Mostrar SOLO cantidad y porcentaje dentro de los sectores
            // Ejemplo: 8 (57,14 %)
            serie.IsValueShownAsLabel = true;
            serie.Label = "#VAL (#PERCENT{P2})";
            serie.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            // Agregar los datos a la serie
            foreach (var kv in _conteoEstados)
            {
                int valor = kv.Value;
                string estado = kv.Key;

                DataPoint punto = new DataPoint();
                punto.AxisLabel = estado;               // Usado internamente por el chart
                punto.LegendText = estado;              // Esto asegura que la leyenda muestre solo el estado
                punto.SetValueXY(estado, valor);

                serie.Points.Add(punto);
            }

            chartEstados.Series.Add(serie);

            // Hacer que el gráfico ocupe todo el espacio disponible
            chartEstados.Dock = DockStyle.Fill;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExportar_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = "Reporte_" + _datos.NOMBRE + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".pdf";

            // Cargamos la plantilla HTML
            string paginahtml_texto = Properties.Resources.plantilla.ToString();

            // Reemplazo de campos generales del gimnasio
            paginahtml_texto = paginahtml_texto.Replace("@NOMBRE", _datos.NOMBRE);
            paginahtml_texto = paginahtml_texto.Replace("@DIRECCION", _datos.DIRECCION);
            paginahtml_texto = paginahtml_texto.Replace("@TELEFONO", _datos.TELEFONO);
            paginahtml_texto = paginahtml_texto.Replace("@CORREO", _datos.CORREO);
            paginahtml_texto = paginahtml_texto.Replace("@HORARIOLAV", _datos.HORARIOLAV);
            paginahtml_texto = paginahtml_texto.Replace("@HORARIOSAB", _datos.HORARIOSAB);
            paginahtml_texto = paginahtml_texto.Replace("@FECHA", _datos.FECHA);
            paginahtml_texto = paginahtml_texto.Replace("@LOGOPATH", _datos.LOGO.Replace("\\", "/"));

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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
