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
    public partial class frmProducto : Form
    {
        #region "Métodos"
        private void limpiarCampos()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtDescripcion.Text = "";
            cboEstado.SelectedIndex = -1;

            txtDescripcion.Select();
        }

        private void cargarGrid()
        {
            // Para el Grid - MOSTRAR TODOS LAS CATEGORIAS
            List<Producto> lista = new CN_Producto().Listar();

            foreach (Producto item in lista)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.oCategoria.IdCategoria,
                    item.oCategoria.Descripcion,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }
        #endregion

        public frmProducto()
        {
            InitializeComponent();
        }   

        private void frmProducto_Load(object sender, EventArgs e)
        {
            // Para ComboBox de Estado
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            cboEstado.DisplayMember = "Texto"; 
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = -1; 

            // Para ComboBox de Categoria 
            List<Categoria> listaCategoria = new CN_Categoria().Listar();

            foreach (Categoria item in listaCategoria)
            {
                cboCategoria.Items.Add(new OpcionCombo() { Valor = item.IdCategoria, Texto = item.Descripcion });
            }

            cboCategoria.DisplayMember = "Texto";
            cboCategoria.ValueMember = "Valor";
            cboCategoria.SelectedIndex = -1;

            // Para ComboBox de filtrado - con Items.Add
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = -1;

            cargarGrid();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto unProducto = new Producto
                {
                    IdProducto = Convert.ToInt32(txtId.Text),
                    Codigo = txtCodigo.Text,
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OpcionCombo)cboCategoria.SelectedItem).Valor) },
                    Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
                };
                string mensaje = string.Empty;

                if (unProducto.IdProducto == 0)
                {
                    int idProductoGenerado = new CN_Producto().Registrar(unProducto, out mensaje);

                    if (idProductoGenerado != 0)
                    {
                        dgvData.Rows.Clear();
                        cargarGrid();
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(mensaje);
                    }
                }
                else
                {
                    bool rta = new CN_Producto().Editar(unProducto, out mensaje);

                    if (rta)
                    {
                        dgvData.Rows.Clear();
                        cargarGrid();
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Verifica que la fila no sea el encabezado (-1 indica el encabezado)
            if (e.RowIndex < 0)
                return;

            // Verifica que está en la primera columna (columna de botones)
            if (e.ColumnIndex == 0)
            {
                // Pinta la celda con sus partes predeterminadas
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // Obtiene el ancho y alto del recurso (icono o imagen "check20")
                var w = Properties.Resources.check.Width;
                var h = Properties.Resources.check.Height;

                // Calcula la posición X para centrar la imagen horizontalmente
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;

                // Calcula la posición Y para centrar la imagen verticalmente
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                // Dibuja la imagen "check" en la celda, centrada
                e.Graphics.DrawImage(Properties.Resources.check, new Rectangle(x, y, w, h));

                // Indica que la celda ha sido manejada y no necesita ser pintada nuevamente
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();

                    txtCodigo.Text = dgvData.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtDescripcion.Text = dgvData.Rows[indice].Cells["Descripcion"].Value.ToString();

                    // Recorre todos los ítems del combo box 'cboCategoria'
                    foreach (OpcionCombo oc in cboCategoria.Items)
                    {
                        // Compara el valor del ítem del combo con el valor 'IdCategoria' de la fila seleccionada en el DataGridView
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdCategoria"].Value))
                        {
                            // Si encuentra coincidencia, obtiene el índice del ítem correspondiente en el combo box
                            int indice_combo = cboCategoria.Items.IndexOf(oc);

                            // Establece el ítem encontrado como el seleccionado en el combo box
                            cboCategoria.SelectedIndex = indice_combo;

                            // Sale del bucle una vez encontrado el valor para evitar búsquedas innecesarias
                            break;
                        }
                    }

                    // Recorre todos los ítems del combo box 'cboEstado'
                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        // Compara el valor del ítem del combo con el valor 'EstadoValor' de la fila seleccionada en el DataGridView
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            // Si encuentra coincidencia, obtiene el índice del ítem correspondiente en el combo box
                            int indice_combo = cboEstado.Items.IndexOf(oc);

                            // Establece el ítem encontrado como el seleccionado en el combo box
                            cboEstado.SelectedIndex = indice_combo;

                            // Sale del bucle una vez encontrado el valor para evitar búsquedas innecesarias
                            break;
                        }
                    }

                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el producto", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Producto objProducto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(txtId.Text)
                    };

                    bool respuesta = new CN_Producto().Eliminar(objProducto, out mensaje);

                    if (respuesta)
                    {
                        dgvData.Rows.Clear();
                        cargarGrid();
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
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

        // Evento que se ejecuta cuando el usuario hace clic en el botón "Exportar"
        private void btnExportar_Click(object sender, EventArgs e)
        {
            // Verifica si hay datos en el DataGridView. Si no hay datos, muestra un mensaje y sale del método.
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return; // Salir del método para evitar ejecución innecesaria
            }

            // Se crea un DataTable para almacenar los datos que se exportarán
            DataTable dt = new DataTable();

            // Se recorren todas las columnas visibles del DataGridView para agregarlas al DataTable
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.HeaderText != "" && columna.Visible) // Solo agrega columnas visibles con encabezado
                {
                    dt.Columns.Add(columna.HeaderText, typeof(string)); // Agrega la columna al DataTable con tipo string
                }
            }

            // Se recorren todas las filas visibles del DataGridView para agregarlas al DataTable
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.Visible) // Solo toma en cuenta las filas visibles
                {
                    dt.Rows.Add(new object[]
                    {
                row.Cells[2].Value.ToString(),  // Columna 2
                row.Cells[3].Value.ToString(),  // Columna 3
                row.Cells[4].Value.ToString(),  // Columna 4
                row.Cells[6].Value.ToString(),  // Columna 6
                row.Cells[7].Value.ToString(),  // Columna 7
                row.Cells[8].Value.ToString(),  // Columna 8
                row.Cells[9].Value.ToString(),  // Columna 9
                row.Cells[11].Value.ToString()  // Columna 11
                    });
                }
            }

            // Se crea el cuadro de diálogo para guardar el archivo
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("ReporteProducto_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss")); // Nombre del archivo con fecha y hora
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
