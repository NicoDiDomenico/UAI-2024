using CapaPresentacion.Utilidades;
using Controlador;
using Controlador.State.Socio;
using FontAwesome.Sharp;
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
    public partial class frmSocios : Form
    {
        #region "Variables"
        private static Form formularioActivo = null;
        private static int idSocioSeleccionado;
        private static string EstadoSocioSeleccionado;
        private static DateTime vencimientoCuota;
        private static string nombreSocioSeleccionado;
        private static Usuario usuario;
        #endregion

        #region "Métodos"
        private void BloquearBotones()
        {
            btnMenuConsultar.Enabled = false;
            btnMenuConsultar.BackColor = Color.Gray;

            btnMenuEliminar.Enabled = false;
            btnMenuEliminar.BackColor = Color.Gray;

            btnMenuTurno.Enabled = false;
            btnMenuTurno.BackColor = Color.Gray;
        }

        private void PersonalizarFormulario(Form formulario)
        {
            // No creo que haga falta
        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.StartPosition = FormStartPosition.CenterScreen; // Centra la ventana
            formulario.Load += (s, e) =>
            {
                // Ajustar la posición para que la ventana baje 50 píxeles desde la posición centrada
                formulario.Top += 95;
            };

            formulario.ShowDialog(); // Muestra como ventana modal
            
            BloquearBotones();

            dgvData.Rows.Clear();
            cargarGrid();
        }

        private void cargarGrid()
        {
            dgvData.Rows.Clear(); // Asegura que se borren los datos anteriores

            List<Socio> listaSocio = new ControladorGymSocio().Listar();
            DateTime fechaActual = DateTime.Today;

            // Filtrar según el estado del CheckBox
            if (!ckbMostrarEliminados.Checked)
            {
                listaSocio = listaSocio.Where(s => s.EstadoSocio != "Eliminado").ToList();
            }

            foreach (Socio item in listaSocio)
            {
                int rowIndex = dgvData.Rows.Add(new object[] {
                    "",
                    item.IdSocio,
                    item.NombreYApellido,
                    item.FechaNacimiento.ToShortDateString(),
                    item.Genero == "Masculino" ? "Masculino" : "Femenino",
                    item.NroDocumento,
                    item.Ciudad,
                    item.Direccion,
                    item.Telefono,
                    item.Email,
                    item.ObraSocial,
                    item.Plan,
                    item.EstadoSocio,
                    item.FechaInicioActividades?.ToShortDateString() ?? "",
                    item.FechaFinActividades?.ToShortDateString() ?? "",
                    item.FechaNotificacion?.ToShortDateString() ?? "",
                    item.RespuestaNotificacion,
                });

                if (item.FechaFinActividades.HasValue && item.FechaFinActividades.Value <= fechaActual)
                {
                    dgvData.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }


        private void validarPermisos()
        {
            List<Permiso> listaPermisos = new ControladorGymPermiso().Listar(usuario.IdUsuario);

            // Lista de botones a validar
            List<IconButton> botones = new List<IconButton> { btnMenuAgregar, btnMenuConsultar, btnMenuEliminar, btnMenuTurno };

            foreach (IconButton boton in botones)
            {
                // Verificar si el usuario tiene permiso por Grupo o por Acción
                bool tienePermiso = listaPermisos.Any(p =>
                    (p.Grupo != null && p.Grupo.NombreMenu == boton.Name) ||
                    (p.Accion != null && p.Accion.NombreAccion == boton.Name)
                );

                if (!tienePermiso)
                {
                    boton.Visible = false; // En lo botones cambio .Enabled por .Visible para evitar que al hacer clica un usuario igualmente se active aunque no tenga el permiso.
                    boton.BackColor = Color.Gainsboro;
                }
            }
        }
        #endregion

        public frmSocios(Usuario usuarioActual)
        {
            usuario = usuarioActual;
            InitializeComponent();
        }
        private void frmSocios_Load(object sender, EventArgs e)
        {
            validarPermisos();

            dgvData.Rows.Clear();

            BloquearBotones();

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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmEditarSocio(idSocioSeleccionado));
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmAgregarSocio());
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
                    // Desmarcar todas las filas primero
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        row.Cells["Seleccionado"].Value = false;
                    }

                    // Activar el check2 solo en la fila clickeada
                    dgvData.Rows[indice].Cells["Seleccionado"].Value = true;

                    // Refrescar la vista
                    dgvData.Refresh();
                }
            }
        }
        private void dgvData_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgvData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    // Desmarcar todas las filas primero
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        row.Cells["Seleccionado"].Value = false;
                    }

                    // Activar el check solo en la fila clickeada
                    dgvData.Rows[indice].Cells["Seleccionado"].Value = true;

                    // Guardar el IdSocio de la fila seleccionada
                    idSocioSeleccionado = Convert.ToInt32(dgvData.Rows[indice].Cells["IdSocio"].Value);
                    EstadoSocioSeleccionado = Convert.ToString(dgvData.Rows[indice].Cells["EstadoSocio"].Value);
                    nombreSocioSeleccionado = Convert.ToString(dgvData.Rows[indice].Cells["NombreYApellido"].Value);
                    vencimientoCuota = Convert.ToDateTime(dgvData.Rows[indice].Cells["FechaFinActividades"].Value);

                    btnMenuConsultar.Enabled = true;
                    btnMenuConsultar.BackColor = Color.RoyalBlue;

                    if (EstadoSocioSeleccionado != "Eliminado")
                    {
                        btnMenuEliminar.Enabled = true;
                        btnMenuEliminar.BackColor = Color.Firebrick;
                    }
                    else
                    {
                        btnMenuEliminar.Enabled = false;
                        btnMenuEliminar.BackColor = Color.Gray;
                    }

                    btnMenuTurno.Enabled = true;
                    btnMenuTurno.BackColor = Color.White;

                    // Refrescar la vista
                    dgvData.Refresh();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
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

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            cboBusqueda.SelectedIndex = -1;
            txtBusqueda.Clear();
            dgvData.Rows.Clear();
            BloquearBotones();
            cargarGrid();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSocioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un socio para eliminar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación: no permitir eliminar si la cuota sigue vigente
            if (vencimientoCuota > DateTime.Today)
            {
                MessageBox.Show("No se puede eliminar un socio que aún tiene la cuota vigente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Socio socio = new Socio() { IdSocio = idSocioSeleccionado, FechaFinActividades = vencimientoCuota };

            if (EstadoSocioSeleccionado == "Eliminado")
            {
                MessageBox.Show("Este socio ya fue eliminado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool eliminado = new ControladorGymSocio().ActualizarEstadoSocio(socio, "Eliminado");

            if (eliminado)
            {
                MessageBox.Show("Socio eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvData.Rows.Clear();
                cargarGrid(); // Recargar la lista de socios
            }
            else
            {
                MessageBox.Show("No se puede eliminar un socio que aún tiene la cuota vigente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTurno_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmTurno(idSocioSeleccionado, nombreSocioSeleccionado));
        }

        private void ckbMostrarEliminados_CheckedChanged(object sender, EventArgs e)
        {
            cargarGrid();
        }
    }
}
