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
using Vista.Modales;

namespace Vista
{
    public partial class frmMenuCalentamiento : Form
    {
        #region "Métodos"
        private void limpiarCampos()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtIdMaquina.Text = "0";
            txtBusqueda.Clear();
            txbDescripcion.Clear();
            txtNombreMaquina.Text = "Sin Máquina";

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
            List<Calentamiento> listaCalentamientoo = new ControladorGymCalentamiento().Listar();

            foreach (Calentamiento item in listaCalentamientoo)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdCalentamiento,
                    item.MaquinaTipoCardio != null ? item.MaquinaTipoCardio.IdElemento : (object)DBNull.Value,
                    item.DescripcionCalentamiento,
                });
            }
        }
        #endregion

        public frmMenuCalentamiento()
        {
            InitializeComponent();
        }

        private void frmMenuCalentamiento_Load(object sender, EventArgs e)
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

            cargarGrid();
        }
        private void btnBuscarMaquina_Click(object sender, EventArgs e)
        {
            using (var modal = new mdMaquina())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdMaquina.Text = Convert.ToString(modal._Maquina.IdElemento);
                    txtNombreMaquina.Text = modal._Maquina.NombreElemento;
                }
                else
                {
                    btnBuscarMaquina.Select();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(txbDescripcion.Text))
                {
                    MessageBox.Show("Debe ingresar una descripción para el calentamiento.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Calentamiento calentamiento = new Calentamiento()
                {
                    IdCalentamiento = Convert.ToInt32(txtId.Text),
                    DescripcionCalentamiento = txbDescripcion.Text,
                    MaquinaTipoCardio = int.TryParse(txtIdMaquina.Text, out int idMaquina) && idMaquina > 0
                                        ? new Maquina { IdElemento = idMaquina }
                                        : null
                };

                string mensaje = string.Empty;
                bool operacionExitosa;

                if (calentamiento.IdCalentamiento == 0)
                {
                    operacionExitosa = new ControladorGymCalentamiento().Registrar(calentamiento, out mensaje);
                }
                else
                {
                    operacionExitosa = new ControladorGymCalentamiento().Editar(calentamiento, out mensaje);
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
                    txtId.Text = dgvData.Rows[indice].Cells["IdCalentamiento"].Value.ToString();
                    txtIdMaquina.Text = dgvData.Rows[indice].Cells["IdMaquina"].Value?.ToString() ?? "0";
                    txbDescripcion.Text = dgvData.Rows[indice].Cells["DescripcionCalentamiento"].Value?.ToString() ?? "";

                    // Si hay una máquina asociada
                    if (int.TryParse(txtIdMaquina.Text, out int idMaquina) && idMaquina > 0)
                    {
                        Maquina m = new ControladorGymMaquina().Listar().FirstOrDefault(x => x.IdElemento == idMaquina);
                        if (m != null)
                        {
                            txtNombreMaquina.Text = m.NombreElemento;
                        }
                        else
                        {
                            txtNombreMaquina.Text = "Sin Máquina";
                            txtIdMaquina.Text = "0";
                        }
                    }
                    else
                    {
                        txtNombreMaquina.Text = "Sin Máquina";
                        txtIdMaquina.Text = "0";
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void btnLimpiarMaquina_Click(object sender, EventArgs e)
        {
            txtNombreMaquina.Text = "Sin Máquina";
            txtIdMaquina.Text = "0";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int idCalentamiento) && idCalentamiento > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar el calentamiento?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    bool eliminado = new ControladorGymCalentamiento().Eliminar(idCalentamiento, out mensaje);

                    if (eliminado)
                    {
                        dgvData.Rows.Clear();
                        cargarGrid();
                        limpiarCampos();
                        MessageBox.Show("El calentamiento fue eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el calentamiento.\n" + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un calentamiento para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
