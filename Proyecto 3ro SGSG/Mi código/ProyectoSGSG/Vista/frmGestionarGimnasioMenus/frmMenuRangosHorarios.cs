using CapaPresentacion.Utilidades;
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
using Vista.Utilidades;
using Vista.Modales;

using Controlador;

namespace Vista
{
    public partial class frmMenuRangosHorarios : Form
    {
        #region "Métodos"
        private int indiceActual;
        #endregion
        
        #region "Métodos"

        private void limpiarCampos()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtCupoMaximo.Text = "0";
            txtNombreEntrenador.Text = "";
            txtIdEntrenador.Text = "0";
            dgvData.Rows.Clear();
            cboRangoHorario.SelectedIndex = -1;
            gbEntrenador.Enabled = false;
            txtCupoMaximo.Enabled = false;
            btnEditarCupos.Enabled = false;
            //cargarCBO();
        }

        private void cargarGrid()
        {
            // Validar si el DataGridView o el controlador están inicializados
            if (dgvData == null)
            {
                MessageBox.Show("El control dgvData no está inicializado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<RangoHorario> listaTodoRangoHorario = new ControladorGymRangoHorario().ListarTodo();

            // Contar la cantidad de entrenadores por rango horario
            var cantidadPorRango = listaTodoRangoHorario
                .GroupBy(r => r.IdRangoHorario)
                .ToDictionary(g => g.Key, g => g.Count());

            // Para el Grid - MOSTRAR TODOS LOS Rango 
            List<RangoHorario> listaRangoHorario = new ControladorGymRangoHorario().Listar();

            foreach (RangoHorario item in listaRangoHorario)
            {
                int cantidad = cantidadPorRango.ContainsKey(item.IdRangoHorario)
                    ? cantidadPorRango[item.IdRangoHorario]
                    : 0;

                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdRangoHorario,
                    item.HoraDesde,
                    item.HoraHasta,
                    item.CupoMaximo,
                    item.Activo ? "Si" : "No",
                    cantidad
                    // item.UnUsuario.NombreYApellido,
                    // item.UnUsuario.IdUsuario
                });
            }
            
            foreach (RangoHorario item in listaTodoRangoHorario)
            {
                dgvEntrenadores.Rows.Add(new object[] {
                    item.UnUsuario.NombreYApellido,
                    item.IdRangoHorario,
                    item.HoraDesde,
                    item.HoraHasta,
                    item.UnUsuario.IdUsuario,
                });
            }
        }
        #endregion

        public frmMenuRangosHorarios()
        {
            InitializeComponent();
        }

        private void frmMenuRangosHorarios_Load(object sender, EventArgs e)
        {
            if (dgvData == null)
            {
                MessageBox.Show("El DataGridView no está disponible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cbActivo.Enabled = false;
            btnEditarCupos.Enabled = false;
            cargarGrid();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
    {
            if (cboRangoHorario.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un rango horario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCupoMaximo.Text, out int cupo) || cupo < 0)
            {
                MessageBox.Show("Ingrese un cupo válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensaje;
            
            bool resultado = new ControladorGymRangoHorario().Registrar(Convert.ToInt32(txtId.Text), Convert.ToInt32(txtIdEntrenador.Text), out mensaje);

            if (resultado)
            {
                MessageBox.Show("Entrenador asignado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvEntrenadores.Rows.Clear();
                limpiarCampos();
                cargarGrid();
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarEntrenador_Click(object sender, EventArgs e)
        {
            using (var modal = new mdEntrenador())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdEntrenador.Text = Convert.ToString(modal._Usuario.IdUsuario);
                    txtNombreEntrenador.Text = modal._Usuario.NombreYApellido;
                    txtNombreEntrenador.Enabled = true;
                }
                else
                {
                    btnBuscarEntrenador.Select();
                }
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
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                indiceActual = indice;

                if (indice >= 0)
                {
                    // Desmarcar todas las filas primero
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        row.Cells["Seleccionado"].Value = false;
                    }

                    txtCupoMaximo.Enabled = false;
                    txtIdEntrenador.Text = "0";
                    txtNombreEntrenador.Clear();
                    txtNombreEntrenador.Enabled = false;
                    btnEditarCupos.Enabled = true;
                    cbActivo.Enabled = true;

                    // Marcar la fila actual como seleccionada
                    dgvData.Rows[indice].Cells["Seleccionado"].Value = true;
                    dgvData.Refresh();

                    // Asignar los valores de la fila seleccionada a los controles
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["IdRangoHorario"].Value.ToString();
                    if (dgvData.Rows[indice].Cells["Activo"].Value.ToString() == "Si") cbActivo.Checked = true;
                    else cbActivo.Checked = false;

                    // **Aquí inicializamos el valor de IdRangoHorario**
                    int idRangoHorarioSeleccionado = Convert.ToInt32(dgvData.Rows[indice].Cells["IdRangoHorario"].Value);

                    // **Limpiar y volver a cargar el ComboBox**
                    cboRangoHorario.Items.Clear();

                    List<RangoHorario> listaRangos = new ControladorGymRangoHorario().Listar();
                    foreach (RangoHorario item in listaRangos)
                    {
                        cboRangoHorario.Items.Add(new OpcionCombo()
                        {
                            Valor = item.IdRangoHorario,
                            Texto = $"{item.HoraDesde} - {item.HoraHasta}"
                        });
                    }

                    cboRangoHorario.DisplayMember = "Texto";
                    cboRangoHorario.ValueMember = "Valor";

                    // **Seleccionar el elemento correcto en el ComboBox**
                    foreach (OpcionCombo oc in cboRangoHorario.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == idRangoHorarioSeleccionado)
                        {
                            int indice_combo = cboRangoHorario.Items.IndexOf(oc);
                            cboRangoHorario.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    txtCupoMaximo.Text = dgvData.Rows[indice].Cells["CupoMaximo"].Value.ToString();
                    btnBuscarEntrenador.Select();
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            cargarGrid();
        }

        private void btnEditarCupos_Click(object sender, EventArgs e)
        {
            txtCupoMaximo.Enabled = true;
        }

        private void txtCupoMaximo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Verifica si se presionó Enter
            {
                // Verificar que el valor sea un número válido
                if (!int.TryParse(txtCupoMaximo.Text, out int nuevoCupo) || nuevoCupo < 0)
                {
                    MessageBox.Show("Ingrese un cupo válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener el IdRangoHorario desde el DataGridView o el txtId
                if (!int.TryParse(txtId.Text, out int idRangoHorario))
                {
                    MessageBox.Show("Seleccione un rango horario válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Llamar al método que actualiza la BD
                string mensaje;
                bool resultado = new ControladorGymRangoHorario().ActualizarCupo(idRangoHorario, nuevoCupo, out mensaje);

                if (resultado)
                {
                    MessageBox.Show("Cupo actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCupoMaximo.Enabled = false;
                    dgvData.Rows.Clear();
                    dgvEntrenadores.Rows.Clear();
                    cargarGrid();
                    dgvData.Rows[indiceActual].Cells["Seleccionado"].Value = true;
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvEntrenadores_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Verifica que la fila no sea el encabezado (-1 indica el encabezado)
            if (e.RowIndex < 0)
                return;

            // Verifica que está en la primera columna (columna de botones)
            if (e.ColumnIndex == 5)
            {
                // Pinta la celda con sus partes predeterminadas
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // Obtiene el ancho y alto del recurso (icono o imagen "trash")
                var w = Properties.Resources.trash.Width;
                var h = Properties.Resources.trash.Height;

                // Calcula la posición X para centrar la imagen horizontalmente
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;

                // Calcula la posición Y para centrar la imagen verticalmente
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                // Dibuja la imagen "trash" en la celda, centrada
                e.Graphics.DrawImage(Properties.Resources.trash, new Rectangle(x, y, w, h));

                // Indica que la celda ha sido manejada y no necesita ser pintada nuevamente
                e.Handled = true;
            }
        }

        private void dgvEntrenadores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEntrenadores.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    // Obtener IdRangoHorario e IdUsuario de la fila seleccionada
                    int idRangoHorario = Convert.ToInt32(dgvEntrenadores.Rows[index].Cells["IdRangoHorario2"].Value);
                    int idUsuario = Convert.ToInt32(dgvEntrenadores.Rows[index].Cells["IdUsuario"].Value);

                    // Confirmar antes de eliminar
                    DialogResult result = MessageBox.Show("¿Desea eliminar la asignación del entrenador al rango horario?",
                                                          "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Llamar al método que elimina en la BD
                        string mensaje;
                        bool eliminado = new ControladorGymRangoHorario().EliminarRelacion(idRangoHorario, idUsuario, out mensaje);

                        if (eliminado)
                        {
                            dgvEntrenadores.Rows.RemoveAt(index); // Eliminar visualmente del DataGridView
                            MessageBox.Show("Asignación eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void cbActivo_CheckedChanged(object sender, EventArgs e)
        {
            ControladorGymRangoHorario ControladorGymRangoHorario = new ControladorGymRangoHorario();

            ControladorGymRangoHorario.SetActivo(Convert.ToInt32(txtId.Text), cbActivo.Checked);

            dgvData.Rows.Clear();
            dgvEntrenadores.Rows.Clear();
            cargarGrid();
            dgvData.Rows[indiceActual].Cells["Seleccionado"].Value = true;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
