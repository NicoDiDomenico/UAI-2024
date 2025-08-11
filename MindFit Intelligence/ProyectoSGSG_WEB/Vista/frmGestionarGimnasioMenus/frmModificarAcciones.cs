using CapaPresentacion.Utilidades;
using Controlador;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmModificarAcciones : Form
    {
        #region "Métodos"
        private void limpiarCampos()
        {
            txtId.Text = "0";
            txtAccion.Text = "";
            txbDescripcion.Text = "";
            txtIdGrupo.Text = "0";

            txbDescripcion.Enabled = false;

            // Desmarcar todas las filas en la columna "Seleccionado"
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Cells["Seleccionado"].Value = false;
            }

            // Refrescar el DataGridView para reflejar los cambios
            dgvData.Refresh();

            dgvData.Rows.Clear();
        }
        private void cargarGrid()
        {
            List<Accion> acciones = new ControladorGymAccion().ListarTodo();

            foreach (Accion item in acciones)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdAccion,
                    item.NombreAccion,
                    item.Descripcion,
                    item.IdGrupo
                });
            }
        }
        #endregion
        public frmModificarAcciones()
        {
            InitializeComponent();
        }

        private void frmModificarAcciones_Load(object sender, EventArgs e)
        {
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

            txbDescripcion.Enabled = false;

            cargarGrid();
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
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    ////
                    // Desmarcar todas las filas primero
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        row.Cells["Seleccionado"].Value = false;
                    }

                    // Activar el check2 solo en la fila clickeada
                    dgvData.Rows[indice].Cells["Seleccionado"].Value = true;

                    // Refrescar la vista
                    dgvData.Refresh();
                    ////
                    txtId.Text = dgvData.Rows[indice].Cells["IdAccion"].Value.ToString();
                    txtAccion.Text = dgvData.Rows[indice].Cells["NombreAccion"].Value.ToString();
                    txbDescripcion.Text = dgvData.Rows[indice].Cells["Descripcion"].Value.ToString();
                    txtIdGrupo.Text = dgvData.Rows[indice].Cells["IdGrupo"].Value.ToString();

                    txbDescripcion.Enabled = true;
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones antes de crear el objeto
            if (string.IsNullOrWhiteSpace(txtId.Text) || !int.TryParse(txtId.Text, out int idAccion))
            {
                MessageBox.Show("El campo ID de la acción es obligatorio y debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAccion.Text))
            {
                MessageBox.Show("El campo Nombre de la Acción es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txbDescripcion.Text))
            {
                MessageBox.Show("El campo Descripción es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtIdGrupo.Text) || !int.TryParse(txtIdGrupo.Text, out int idGrupo))
            {
                MessageBox.Show("El campo ID del Grupo es obligatorio y debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensaje = string.Empty;

            Accion unaAccion = new Accion()
            {
                IdAccion = idAccion,
                NombreAccion = txtAccion.Text,
                Descripcion = txbDescripcion.Text,
                IdGrupo = idGrupo
            };

            bool respuesta = new ControladorGymAccion().Modificar(unaAccion, out mensaje);

            if (respuesta)
            {
                MessageBox.Show("Acción actualizada correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                limpiarCampos();
                cargarGrid();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }
    }
}
