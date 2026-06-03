using CapaPresentacion.Utilidades;
using Controlador;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmTurno : Form
    {
        #region "Variables"
        private static int idSocioSeleccionado;
        private static string nombreSocioSeleccionado;
        private static Turno TurnoActual;
        #endregion

        #region "Métodos"
        private void AbrirFormulario(Form formulario)
        {
            formulario.StartPosition = FormStartPosition.CenterScreen; // Centra la ventana
            formulario.Load += (s, e) =>
            {
                // Ajustar la posición para que la ventana baje 50 píxeles desde la posición centrada
                formulario.Top += 95;
            };
            formulario.ShowDialog(); // Muestra como ventana modal
        }

        private void cargarGrid()
        {
            List<Turno> turnos = new ControladorGymTurno().Listar(idSocioSeleccionado);

            foreach (Turno item in turnos)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdTurno,
                    item.FechaTurno.ToString("dd/MM/yyyy"),
                    item.unRangoHorario.IdRangoHorario,
                    item.unRangoHorario.HoraDesde.ToString(@"hh\:mm"),
                    item.unRangoHorario.HoraHasta.ToString(@"hh\:mm"),
                    item.EstadoTurno,
                    item.CodigoIngreso,
                    item.unUsuario.IdUsuario,
                    item.unUsuario.NombreYApellido,
                    item.unSocio.IdSocio,
                    item.unSocio.NombreYApellido
                });
            }
        }
        #endregion

        public frmTurno(int idSocioActual, string nombreSocioActual)
        {
            idSocioSeleccionado = idSocioActual;
            nombreSocioSeleccionado = nombreSocioActual;
            InitializeComponent();
        }

        private void frmTurno_Load(object sender, EventArgs e)
        {
            dgvData.ClearSelection();  // Quita la selección inicial

            btnCancelar.Enabled = false;
            btnCancelar.BackColor = Color.Gray;

            dgvData.Rows.Clear();
            lblSocio.Text = nombreSocioSeleccionado;
            cargarGrid();

            btnNuevoTurno.Select();
        }

        private void btnNuevoTurno_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmNuevoTurno(idSocioSeleccionado));
            dgvData.Rows.Clear();
            cargarGrid();
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Verifica que la fila no sea el encabezado (-1 indica el encabezado)
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                // Pinta la celda con sus partes predeterminadas
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                //// Verifica si la celda actual tiene el valor "true" en la columna "Seleccionado"
                bool isSelected = Convert.ToBoolean(dgvData.Rows[e.RowIndex].Cells["Seleccionado"].Value);

                if (isSelected) // Solo dibuja el check2 si la celda está seleccionada
                {
                    // Calcular tamaño proporcional al alto y ancho de la celda (con margen)
                    int iconSize = Math.Min(e.CellBounds.Width, e.CellBounds.Height) - 6;

                    // Centrado de la imagen
                    int x = e.CellBounds.Left + (e.CellBounds.Width - iconSize) / 2;
                    int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                    // Dibujar la imagen escalada
                    e.Graphics.DrawImage(Properties.Resources.target, new Rectangle(x, y, iconSize, iconSize));
                }
                e.Handled = true;
            }

            if (e.ColumnIndex == 12)
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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

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

                    // 🔹 Asignar el IdUsuario del entrenador seleccionado
                    int idTurnoSeleccionado = Convert.ToInt32(dgvData.Rows[indice].Cells["IdTurno"].Value);

                    TurnoActual = new ControladorGymTurno().getTurno(idTurnoSeleccionado);

                    if (TurnoActual.EstadoTurno == "En Curso")
                    {
                        btnCancelar.Enabled = true;
                        btnCancelar.BackColor = Color.Firebrick;
                    }
                    else
                    {
                        btnCancelar.Enabled = false;
                        btnCancelar.BackColor = Color.Gray;
                    }

                    if (TurnoActual != null)
                    {
                        // Refrescar la vista
                        dgvData.Refresh();
                        ////
                    }
                }
            }

            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar") // Verifica si el clic fue en el botón "Eliminar"
            {
                int index = e.RowIndex;
                if (index >= 0) // Asegura que el índice es válido
                {
                    int turnoId = Convert.ToInt32(dgvData.Rows[index].Cells["IdTurno"].Value);
                    int horarioId = Convert.ToInt32(dgvData.Rows[index].Cells["IdRangoHorario"].Value);
                    DateTime fechaTurno = Convert.ToDateTime(dgvData.Rows[index].Cells["FechaTurno"].Value);

                    // Mostrar advertencia antes de eliminar
                    DialogResult result = MessageBox.Show(
                        "¿Está seguro de que desea eliminar este turno?",
                        "Confirmación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes) // Si confirma la eliminación
                    {
                        string mensaje;
                        bool eliminado = new ControladorGymTurno().Eliminar(turnoId, horarioId, fechaTurno, out mensaje);

                        if (eliminado)
                        {
                            MessageBox.Show("Turno eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Actualizar el DataGridView eliminando la fila
                            dgvData.Rows.RemoveAt(index);
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el turno: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                            "¿Está seguro de que desea cancelar este turno?",
                            "Confirmación",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes) // Si confirma la eliminación
            {
                string EstadoTurno = "Cancelado";
                int horarioId = 0;
                bool eliminado = new ControladorGymTurno().ActualizarEstadoTurno(TurnoActual.IdTurno, horarioId, TurnoActual.FechaTurno, EstadoTurno);

                if (eliminado)
                {
                    MessageBox.Show("Turno cancelado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvData.Rows.Clear();
                    cargarGrid();
                }
                else
                {
                    MessageBox.Show("Error al cancelar el turno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
