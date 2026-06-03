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
    public partial class frmMenuEjercicios : Form
    {
        #region "Métodos"
        private void limpiarCampos()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtBusqueda.Clear();

            txbNombreElemento.Clear();
            txbDescripcion.Clear();

            cboBusqueda.SelectedIndex = -1;

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Cells["Seleccionado"].Value = false;
            }

            dgvData.Refresh();
        }

        private void cargarGrid()
        {
            List<Ejercicio> lista = new ControladorGymEjercicio().Listar();

            foreach (Ejercicio item in lista)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdElemento,
                    item.NombreElemento,
                    item.Descripcion
                });
            }
        }
        #endregion
        public frmMenuEjercicios()
        {
            InitializeComponent();
        }

        private void frmMenuEjercicios_Load(object sender, EventArgs e)
        {
            limpiarCampos();

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
                if (string.IsNullOrWhiteSpace(txbDescripcion.Text))
                {
                    MessageBox.Show("Debe ingresar la descripción del ejercicio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Ejercicio ejercicio = new Ejercicio()
                {
                    IdElemento = Convert.ToInt32(txtId.Text),
                    NombreElemento = txbNombreElemento.Text,
                    Descripcion = txbDescripcion.Text
                };

                string mensaje = string.Empty;
                bool operacionExitosa;

                if (ejercicio.IdElemento == 0)
                    operacionExitosa = new ControladorGymEjercicio().Registrar(ejercicio, out mensaje);
                else
                    operacionExitosa = new ControladorGymEjercicio().Editar(ejercicio, out mensaje);

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
            }
            
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

                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["IdElemento"].Value?.ToString() ?? "0";
                    txbNombreElemento.Text = dgvData.Rows[indice].Cells["NombreElemento"].Value?.ToString() ?? "";
                    txbDescripcion.Text = dgvData.Rows[indice].Cells["Descripcion"].Value?.ToString() ?? "";
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
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar el ejercicio seleccionado?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    bool eliminado = new ControladorGymEjercicio().Eliminar(IdElemento, out mensaje);

                    if (eliminado)
                    {
                        dgvData.Rows.Clear();
                        cargarGrid();
                        limpiarCampos();
                        MessageBox.Show("El ejercicio fue eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar este ejercicio.\n" + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un ejercicio para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
