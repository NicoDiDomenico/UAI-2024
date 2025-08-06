using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmReportesVentas : Form
    {
        public frmReportesVentas()
        {
            InitializeComponent();
        }

        private void frmReportesVentas_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;
        }

        private void btnBuscarReporte_Click(object sender, EventArgs e)
        {
            List<ReporteVenta> lista = new List<ReporteVenta>();

            lista = new CN_Reporte().Venta(dtpFechaInicio.Value.ToString(), dtpFechaFin.Value.ToString());

            dgvData.Rows.Clear();

            foreach (ReporteVenta rv in lista)
            {
                dgvData.Rows.Add(new object[]
                {
                    rv.FechaRegistro,
                    rv.TipoDocumento,
                    rv.NumeroDocumento,
                    rv.MontoTotal,
                    rv.UsuarioRegistro,
                    rv.DocumentoCliente,
                    rv.NombreCliente,
                    rv.CodigoProducto,
                    rv.NombreProducto,
                    rv.Categoria,
                    rv.PrecioVenta,
                    rv.Cantidad,
                    rv.SubTotal
                });
            }

        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

                if (dgvData.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                            row.Visible = true;
                        else
                            row.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // Se crea un DataTable para almacenar los datos que se exportarán
                DataTable dt = new DataTable();

                // Se recorren todas las columnas visibles del DataGridView para agregarlas al DataTable
                foreach (DataGridViewColumn columna in dgvData.Columns)
                {
                    dt.Columns.Add(columna.HeaderText, typeof(string)); // Agrega la columna al DataTable con tipo string
                }

                // Se recorren todas las filas visibles del DataGridView para agregarlas al DataTable
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Visible) // Solo toma en cuenta las filas visibles
                    {
                        dt.Rows.Add(new object[]
                        {
                            row.Cells[0].Value.ToString(),  // Columna 2
                            row.Cells[1].Value.ToString(),  // Columna 3
                            row.Cells[2].Value.ToString(),  // Columna 4
                            row.Cells[3].Value.ToString(),  // Columna 6
                            row.Cells[4].Value.ToString(),  // Columna 7
                            row.Cells[5].Value.ToString(),  // Columna 8
                            row.Cells[6].Value.ToString(),  // Columna 9
                            row.Cells[7].Value.ToString(),  // Columna 10
                            row.Cells[8].Value.ToString(),  // ...
                            row.Cells[9].Value.ToString(),
                            row.Cells[10].Value.ToString(),
                            row.Cells[11].Value.ToString(),
                            row.Cells[12].Value.ToString()
                        });
                    }
                }

                // Se crea el cuadro de diálogo para guardar el archivo
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteVentas_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss")); // Nombre del archivo con fecha y hora
                savefile.Filter = "Excel Files | *.xlsx"; // Filtro para archivos Excel

                // Si el usuario selecciona una ubicación y presiona "Guardar"
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Se crea un nuevo libro de Excel usando la biblioteca ClosedXML
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe"); // Se agrega una hoja llamada "Informe"
                        hoja.ColumnsUsed().AdjustToContents(); // Ajusta las columnas al contenido automáticamente
                        wb.SaveAs(savefile.FileName); // Guarda el archivo en la ubicación seleccionada
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        // Si ocurre un error al generar el archivo, muestra un mensaje de advertencia
                        MessageBox.Show("Error al generar reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
