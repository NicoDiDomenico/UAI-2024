using CapaPresentacion.Utilidades;
using Controlador;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmMenuMaquinas : Form
    {
        #region "Métodos"
        private void limpiarCampos()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtBusqueda.Clear();

            txbNombreElemento.Clear();
            dtpFechaFabricacion.Value = DateTime.Now;
            dtpFechaCompra.Value = DateTime.Now;
            txtPrecio.Text = "0";
            txtPeso.Text = "0";
            cbSi.Checked = false;
            cbNo.Checked = false;

            cboBusqueda.SelectedIndex = -1;
            // Desmarcar todas las filas en la columna "Seleccionado"
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Cells["Seleccionado"].Value = false;
            }

            // Refrescar el DataGridView para reflejar los cambios
            dgvData.Refresh();
        }

        private void cargarGrid()
        {
            // Para el Grid - MOSTRAR TODOS LOS USUARIOS
            List<Maquina> listaMaquina = new ControladorGymMaquina().Listar();

            foreach (Maquina item in listaMaquina)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdElemento,
                    item.NombreElemento,
                    item.FechaFabricacion,
                    item.FechaCompra,
                    item.Precio,
                    item.Peso,
                    item.EsElectrica,
                });
            }
        }
        #endregion
        public frmMenuMaquinas()
        {
            InitializeComponent();
        }

        private void frmMenuMaquinas_Load(object sender, EventArgs e)
        {
            limpiarCampos();

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
                if (string.IsNullOrWhiteSpace(txtPrecio.Text) || !float.TryParse(txtPrecio.Text, out float precio) || precio < 0)
                {
                    MessageBox.Show("Debe ingresar un precio válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPeso.Text) || !int.TryParse(txtPeso.Text, out int peso) || peso < 0)
                {
                    MessageBox.Show("Debe ingresar un peso válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!cbSi.Checked && !cbNo.Checked)
                {
                    MessageBox.Show("Debe indicar si la máquina es eléctrica.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool esElectrica = cbSi.Checked;

                // Crear objeto Maquina
                Maquina maquina = new Maquina()
                {
                    IdElemento = Convert.ToInt32(txtId.Text),
                    NombreElemento = txbNombreElemento.Text,
                    FechaFabricacion = dtpFechaFabricacion.Value,
                    FechaCompra = dtpFechaCompra.Value,
                    Precio = precio,
                    Peso = peso,
                    EsElectrica = esElectrica
                };

                string mensaje = string.Empty;
                bool operacionExitosa;

                if (maquina.IdElemento == 0)
                {
                    operacionExitosa = new ControladorGymMaquina().Registrar(maquina, out mensaje);
                }
                else
                {
                    operacionExitosa = new ControladorGymMaquina().Editar(maquina, out mensaje);
                }

                if (operacionExitosa)
                {
                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvData.Rows.Clear();
                    cargarGrid();
                    limpiarCampos();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                //// Verifica si la celda actual tiene el valor "true" en la columna "Seleccionado"
                bool isSelected = Convert.ToBoolean(dgvData.Rows[e.RowIndex].Cells["Seleccionado"].Value);

                if (isSelected) // Solo dibuja el check2 si la celda está seleccionada
                {
                    var w = Properties.Resources.check2.Width;
                    var h = Properties.Resources.check2.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(Properties.Resources.check2, new Rectangle(x, y, w, h));
                }
                e.Handled = true;
            }// Verifica que la fila no sea el encabezado (-1 indica el encabezado)
            if (e.RowIndex < 0)
                return;

            // Verifica que está en la primera columna (columna de botones)
            if (e.ColumnIndex == 0)
            {
                // Pinta la celda con sus partes predeterminadas
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                //// Verifica si la celda actual tiene el valor "true" en la columna "Seleccionado"
                bool isSelected = Convert.ToBoolean(dgvData.Rows[e.RowIndex].Cells["Seleccionado"].Value);

                if (isSelected) // Solo dibuja el check2 si la celda está seleccionada
                {
                    var w = Properties.Resources.check2.Width;
                    var h = Properties.Resources.check2.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(Properties.Resources.check2, new Rectangle(x, y, w, h));
                }
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    // **Evitar excepciones verificando si la celda no es nula**
                    if (dgvData.Rows[indice] == null) return;

                    // Desmarcar todas las filas primero
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        if (row.Cells["Seleccionado"] != null)
                            row.Cells["Seleccionado"].Value = false;
                    }

                    // Activar el check2 solo en la fila clickeada
                    if (dgvData.Rows[indice].Cells["Seleccionado"] != null)
                        dgvData.Rows[indice].Cells["Seleccionado"].Value = true;

                    dgvData.Refresh(); // Refrescar la vista

                    // Cargar los datos al formulario
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["IdElemento"].Value?.ToString() ?? "0";
                    txbNombreElemento.Text = dgvData.Rows[indice].Cells["NombreElemento"].Value?.ToString() ?? "";

                    // Fechas
                    if (DateTime.TryParse(dgvData.Rows[indice].Cells["FechaFabricacion"].Value?.ToString(), out DateTime fechaFab))
                        dtpFechaFabricacion.Value = fechaFab;
                    else
                        dtpFechaFabricacion.Value = DateTime.Now;

                    if (DateTime.TryParse(dgvData.Rows[indice].Cells["FechaCompra"].Value?.ToString(), out DateTime fechaCompra))
                        dtpFechaCompra.Value = fechaCompra;
                    else
                        dtpFechaCompra.Value = DateTime.Now;

                    // Precio y peso
                    txtPrecio.Text = dgvData.Rows[indice].Cells["Precio"].Value?.ToString() ?? "0";
                    txtPeso.Text = dgvData.Rows[indice].Cells["Peso"].Value?.ToString() ?? "0";

                    // Checkbox eléctrico
                    int valorElectrica = Convert.ToInt32(dgvData.Rows[indice].Cells["EsElectrica"].Value ?? 0);
                    cbSi.Checked = valorElectrica == 1;
                    cbNo.Checked = valorElectrica == 0;
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int IdElemento) && IdElemento > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar la maquina seleccionada?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    bool eliminado = new ControladorGymMaquina().Eliminar(IdElemento, out mensaje);

                    if (eliminado)
                    {
                        dgvData.Rows.Clear();
                        cargarGrid();
                        limpiarCampos();
                        MessageBox.Show("La maquina fue eliminada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar esta maquina.\n" + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un estiramiento para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
