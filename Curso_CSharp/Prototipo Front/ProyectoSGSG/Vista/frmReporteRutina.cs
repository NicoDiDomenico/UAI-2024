using Controlador;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Modelo;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace Vista
{
    public partial class frmReporteRutina : Form
    {
        #region "Variables"
        private string _nombreSocio;
        private string _direccion;
        private string _telefono;
        private string _dia;
        private string _entrenador;
        private List<RutinaCalentamiento> _calentamientos;
        private List<Entrenamiento> _entrenamientos;
        private List<RutinaEstiramiento> _estiramientos;
        private Dictionary<string, int> _historial;
        #endregion

        #region "Métodos"
        private string TraducirDiaConTilde(string diaIngles)
        {
            switch (diaIngles)
            {
                case "Monday": return "Lunes";
                case "Tuesday": return "Martes";
                case "Wednesday": return "Miércoles";
                case "Thursday": return "Jueves";
                case "Friday": return "Viernes";
                case "Saturday": return "Sábado";
                case "Sunday": return "Domingo";
                default: return diaIngles;
            }
        }

        private byte[] CrearGraficoHistorial(Dictionary<string, int> dataHistorial)
        {
            Chart chart = new Chart();
            chart.Width = 600;
            chart.Height = 300;

            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.Title = "Día";
            chartArea.AxisY.Title = "Cantidad de Modificaciones";
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart.ChartAreas.Add(chartArea);

            Series series = new Series
            {
                ChartType = SeriesChartType.Column,
                Color = Color.SkyBlue,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.Black
            };

            foreach (var kvp in dataHistorial)
            {
                series.Points.AddXY(kvp.Key, kvp.Value);
            }

            chart.Series.Add(series);
            chart.Legends.Clear();

            // Nuevo título más representativo
            Title titulo = new Title
            {
                Text = "Historial de modificaciones semanales de la rutina",
                Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Alignment = ContentAlignment.TopCenter
            };

            chart.Titles.Add(titulo);

            using (MemoryStream stream = new MemoryStream())
            {
                chart.SaveImage(stream, ChartImageFormat.Png);
                return stream.ToArray();
            }
        }
        #endregion

        public frmReporteRutina(string nombre, string direccion, string telefono,
            string dia, string entrenador,
            List<RutinaCalentamiento> calentamientos,
            List<Entrenamiento> entrenamientos,
            List<RutinaEstiramiento> estiramientos,
            Dictionary<string, int> historial)
        {
            InitializeComponent();
            _nombreSocio = nombre;
            _direccion = direccion;
            _telefono = telefono;
            _dia = dia;
            _entrenador = entrenador;
            _calentamientos = calentamientos;
            _entrenamientos = entrenamientos;
            _estiramientos = estiramientos;
            _historial = historial;
        }

        private void lblEntrenador_Click(object sender, EventArgs e)
        {

        }

        private void lblDia_Click(object sender, EventArgs e)
        {

        }

        private void lblSocio_Click(object sender, EventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void dgvEstiramientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvEntrenamientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvCalentamientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmReporteRutina_Load(object sender, EventArgs e)
        {
            // Mostrar datos generales
            lblTitulo.Text = $"Rutina de {_nombreSocio}";
            lblSocio.Text = $"Socio: {_nombreSocio}";
            lblDia.Text = "Día: " + TraducirDiaConTilde(_dia);
            lblEntrenador.Text = $"Entrenador: {_entrenador}";

            // ---- Cargar Calentamientos ----
            dgvCalentamientos.AutoGenerateColumns = false;
            dgvCalentamientos.Rows.Clear();
            foreach (var c in _calentamientos)
            {
                string desc = new ControladorGymCalentamiento().ObtenerPorId(c.IdCalentamiento)?.DescripcionCalentamiento ?? "N/A";
                dgvCalentamientos.Rows.Add(desc, c.Minutos);
            }

            // ---- Cargar Entrenamientos ----
            dgvEntrenamientos.AutoGenerateColumns = false;
            dgvEntrenamientos.Rows.Clear();
            foreach (var eItem in _entrenamientos)
            {
                dgvEntrenamientos.Rows.Add(eItem.ElementoGimnasio.NombreElemento, eItem.Series, eItem.Repeticiones, eItem.Peso);
            }

            // ---- Cargar Estiramientos ----
            dgvEstiramientos.AutoGenerateColumns = false;
            dgvEstiramientos.Rows.Clear();
            foreach (var eItem in _estiramientos)
            {
                string desc = new ControladorGymEstiramiento().ObtenerPorId(eItem.IdEstiramiento)?.DescripcionEstiramiento ?? "N/A";
                dgvEstiramientos.Rows.Add(desc, eItem.Minutos);
            }

            // ---- Configurar gráfico historial ----
            chartHistorial.Series.Clear();

            var serie = new Series
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            // Traducimos los días al español antes de agregarlos al gráfico
            foreach (var kv in _historial)
            {
                string diaEsp = TraducirDiaConTilde(kv.Key);
                serie.Points.AddXY(diaEsp, kv.Value);
            }

            chartHistorial.Series.Add(serie);

            // Configuración del área del gráfico
            var area = chartHistorial.ChartAreas[0];
            area.AxisX.Title = "Día";
            area.AxisY.Title = "Cantidad de modificaciones";

            // Aclarar líneas del grid
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.MajorGrid.LineWidth = 1;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineWidth = 1;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = "Rutina_" + _nombreSocio.Replace(" ", "_") + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".pdf";
            guardar.Filter = "Archivos PDF (*.pdf)|*.pdf";

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                // 1. Cargar la plantilla HTML
                string paginahtml_texto = Properties.Resources.plantillaRutina.ToString();
                paginahtml_texto = paginahtml_texto.Replace("@NOMBRE", _nombreSocio.ToUpper());
                paginahtml_texto = paginahtml_texto.Replace("@DIRECCION", _direccion);   // pasalo en el constructor
                paginahtml_texto = paginahtml_texto.Replace("@TELEFONO", _telefono);     // pasalo en el constructor
                paginahtml_texto = paginahtml_texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));
                paginahtml_texto = paginahtml_texto.Replace("@DIA", TraducirDiaConTilde(_dia));
                paginahtml_texto = paginahtml_texto.Replace("@ENTRENADOR", _entrenador);

                // 2. Generar filas para cada sección usando las listas ya cargadas
                StringBuilder filasCalentamientos = new StringBuilder();
                foreach (var cal in _calentamientos)
                {
                    string descripcion = new ControladorGymCalentamiento().ObtenerPorId(cal.IdCalentamiento)?.DescripcionCalentamiento ?? "N/A";
                    filasCalentamientos.AppendLine($"<tr><td>{descripcion}</td><td>{cal.Minutos}</td></tr>");
                }
                if (filasCalentamientos.Length == 0)
                    filasCalentamientos.AppendLine("<tr><td colspan='2'>No se registraron calentamientos.</td></tr>");
                paginahtml_texto = paginahtml_texto.Replace("@CALENTAMIENTOS", filasCalentamientos.ToString());

                StringBuilder filasEntrenamientos = new StringBuilder();
                foreach (var ent in _entrenamientos)
                {
                    filasEntrenamientos.AppendLine($"<tr><td>{ent.ElementoGimnasio.NombreElemento}</td><td>{ent.Series}</td><td>{ent.Repeticiones}</td><td>{ent.Peso}</td></tr>");
                }
                if (filasEntrenamientos.Length == 0)
                    filasEntrenamientos.AppendLine("<tr><td colspan='4'>No se registraron entrenamientos.</td></tr>");
                paginahtml_texto = paginahtml_texto.Replace("@ENTRENAMIENTOS", filasEntrenamientos.ToString());

                StringBuilder filasEstiramientos = new StringBuilder();
                foreach (var est in _estiramientos)
                {
                    string descripcion = new ControladorGymEstiramiento().ObtenerPorId(est.IdEstiramiento)?.DescripcionEstiramiento ?? "N/A";
                    filasEstiramientos.AppendLine($"<tr><td>{descripcion}</td><td>{est.Minutos}</td></tr>");
                }
                if (filasEstiramientos.Length == 0)
                    filasEstiramientos.AppendLine("<tr><td colspan='2'>No se registraron estiramientos.</td></tr>");
                paginahtml_texto = paginahtml_texto.Replace("@ESTIRAMIENTOS", filasEstiramientos.ToString());

                // 3. Crear el PDF
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

                    // 4. Agregar el gráfico de historial
                    byte[] graficoBytes = CrearGraficoHistorial(_historial);
                    iTextSharp.text.Image grafico = iTextSharp.text.Image.GetInstance(graficoBytes);
                    grafico.Alignment = Element.ALIGN_CENTER;
                    grafico.ScaleToFit(500f, 300f);

                    pdfDoc.Add(new Paragraph("\n\n"));
                    pdfDoc.Add(grafico);

                    pdfDoc.Close();
                    stream.Close();
                }

                MessageBox.Show("Reporte generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
