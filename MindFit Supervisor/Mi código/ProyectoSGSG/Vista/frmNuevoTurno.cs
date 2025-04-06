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

using Controlador;
using Vista.Utilidades;
using System.Data.SqlClient;

namespace Vista
{
    public partial class frmNuevoTurno : Form
    {
        #region "Variables"
        private static int idSocioSeleccionado;
        private int IdEntrenadoSeleccionado = 0;
        #endregion
        #region "Métodos"
        private void CargarComboRangoHorario()
        {
            List<RangoHorario> rangoHorarios = new ControladorGymRangoHorario().ListarParaTurno();

            cboRangoHorario.Items.Clear(); // Limpiamos los elementos previos

            // Verificamos el día de la fecha seleccionada
            bool esSabado = dtpFechaTurno.Value.DayOfWeek == DayOfWeek.Saturday;

            foreach (var item in rangoHorarios)
            {
                // Mostrar rangos activos si es día de semana, o solo los de sábado si es sábado
                if ((esSabado && item.SoloSabado) || (!esSabado && item.Activo))
                {
                    string textoRango = $"{item.HoraDesde:hh\\:mm} a {item.HoraHasta:hh\\:mm}";

                    // Verificar si ya existe una opción con la misma HoraDesde y HoraHasta
                    bool existe = cboRangoHorario.Items
                        .OfType<OpcionComboRangoHorario>()
                        .Any(op => op.HoraDesde == item.HoraDesde && op.HoraHasta == item.HoraHasta);

                    // Solo agregar si no existe ya ese rango
                    if (!existe)
                    {
                        OpcionComboRangoHorario opcion = new OpcionComboRangoHorario
                        {
                            Valor = item.IdRangoHorario,
                            Texto = textoRango,
                            HoraDesde = item.HoraDesde,
                            HoraHasta = item.HoraHasta,
                            Cupo = item.CupoMaximo
                        };

                        cboRangoHorario.Items.Add(opcion);
                    }
                }
            }

            cboRangoHorario.DisplayMember = "Texto";
            cboRangoHorario.ValueMember = "Valor";

            // Seleccionar el rango actual basado en la hora del sistema
            TimeSpan ahora = DateTime.Now.TimeOfDay;
            for (int i = 0; i < cboRangoHorario.Items.Count; i++)
            {
                OpcionComboRangoHorario opcion = (OpcionComboRangoHorario)cboRangoHorario.Items[i];
                if (opcion.HoraDesde <= ahora && ahora < opcion.HoraHasta)
                {
                    cboRangoHorario.SelectedIndex = i;
                    break;
                }
            }

            // Si no se encontró ninguno válido, dejar sin selección
            if (cboRangoHorario.SelectedIndex == -1 && cboRangoHorario.Items.Count > 0)
            {
                cboRangoHorario.SelectedIndex = -1;
            }
        }

        private void ValidarFecha(DateTime fecha)
        {
            if (fecha.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("No se pueden asignar turnos los días domingo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Mover automáticamente al lunes siguiente
                dtpFechaTurno.Value = fecha.AddDays(1);
            }
        }
        #endregion

        public frmNuevoTurno(int idSocio)
        {
            // Arreglar
            idSocioSeleccionado = idSocio;
            InitializeComponent();
        }

        private void frmNuevoTurno_Load(object sender, EventArgs e)
        {
            CargarComboRangoHorario();

            // Bloquear selección de domingos
            dtpFechaTurno.MinDate = DateTime.Today;
            dtpFechaTurno.ValueChanged += dtpFechaTurno_ValueChanged;
            ValidarFecha(dtpFechaTurno.Value); // Inicial
        }

        private void cboRangoHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRangoHorario.SelectedItem != null)
            {
                OpcionComboRangoHorario seleccion = (OpcionComboRangoHorario)cboRangoHorario.SelectedItem;

                /*
                var FechaTurno = dtpFechaTurno.Value;
                MessageBox.Show("FechaTurno enviada: " + FechaTurno.ToString("yyyy-MM-dd"));
                */

                // Obtener la lista de entrenadores para el rango horario seleccionado
                List<RangoHorario> entrenadores = new ControladorGymRangoHorario().ListarEntrenadoresDisponibles(seleccion.Valor, dtpFechaTurno.Value);
                
                /*
                foreach (var entrenador in entrenadores)
                {
                    MessageBox.Show($"Entrenador: {entrenador.UnUsuario.NombreYApellido}, CupoActual: {entrenador.CupoActual}");
                }
                */

                dgvData.Rows.Clear(); // Limpiar la grilla antes de agregar datos

                if (entrenadores.Count == 0)
                {
                    lblSinEntrenadores.Visible = true;
                }
                else
                {
                    lblSinEntrenadores.Visible = false;

                    foreach (var rangoSeleccionado in entrenadores)
                    {
                        int rowIndex = dgvData.Rows.Add();
                        dgvData.Rows[rowIndex].Cells["IdUsuario"].Value = rangoSeleccionado.UnUsuario.IdUsuario;
                        dgvData.Rows[rowIndex].Cells["Entrenador"].Value = rangoSeleccionado.UnUsuario.NombreYApellido;
                        dgvData.Rows[rowIndex].Cells["CupoActual"].Value = $"{rangoSeleccionado.CupoActual:D2}/{rangoSeleccionado.CupoMaximo:D2}";
                    }
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

                    // 🔹 Asignar el IdUsuario del entrenador seleccionado
                    IdEntrenadoSeleccionado = Convert.ToInt32(dgvData.Rows[indice].Cells["IdUsuario"].Value);

                    // Refrescar la vista
                    dgvData.Refresh();
                    ////
                }
            }
        }

        private void btnRegistrarTurno_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado un rango horario
            if (cboRangoHorario.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un rango horario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Sale del método si no hay selección
            }

            // Validar que se haya seleccionado un entrenador
            if (IdEntrenadoSeleccionado == 0) // Suponiendo que el ID sea 0 si no se ha seleccionado ninguno
            {
                MessageBox.Show("Debe seleccionar un entrenador.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Sale del método si no hay entrenador seleccionado
            }

            // Validar que se haya seleccionado un socio
            if (idSocioSeleccionado == 0) // Suponiendo que el ID sea 0 si no se ha seleccionado ninguno
            {
                MessageBox.Show("Debe seleccionar un socio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Sale del método si no hay socio seleccionado
            }

            // Obtener el rango horario seleccionado
            OpcionComboRangoHorario seleccion = (OpcionComboRangoHorario)cboRangoHorario.SelectedItem;

            // Crear el objeto Turno con las validaciones correctas
            Turno nuevoTurno = new Turno()
            {
                FechaTurno = dtpFechaTurno.Value,
                unUsuario = new Usuario() { IdUsuario = IdEntrenadoSeleccionado }, // Entrenador seleccionado
                unSocio = new Socio() { IdSocio = idSocioSeleccionado }, // Socio seleccionado
                unRangoHorario = new RangoHorario() { IdRangoHorario = seleccion.Valor } // Rango horario seleccionado
            };

            string mensaje;
            int idTurno = new ControladorGymTurno().Registrar(nuevoTurno, out mensaje);

            if (idTurno > 0)
            {
                MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Registrar Rurno " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpFechaTurno_ValueChanged(object sender, EventArgs e)
        {
            ValidarFecha(dtpFechaTurno.Value);
            CargarComboRangoHorario(); // <- esta línea actualiza el combo con rangos correctos
        }
    }
}
