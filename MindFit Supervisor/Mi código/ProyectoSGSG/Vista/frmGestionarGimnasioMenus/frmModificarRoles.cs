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
using System.Windows.Controls;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmModificarRoles : Form
    {
        #region "Métodos"
        private void limpiarCampos()
        {  
            txtId.Text = "0";
            txtDescripcion.Text = "";
            txtPermisoRol.Text = "";
        }
        private void cargarGrid()
        {
            List<Rol> roles = new ControladorGymRol().Listar();

            foreach (Rol item in roles)
            {
                // Verificar si la descripción del rol ya existe en el DataGridView
                bool existe = false;
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells["Rol"].Value != null &&
                        row.Cells["Rol"].Value.ToString().ToUpper() == item.Descripcion.ToUpper())
                    {
                        existe = true;
                        break; // Si ya existe, no es necesario seguir buscando
                    }
                }

                // Si no existe, lo agregamos al DataGridView
                if (!existe)
                {
                    dgvData.Rows.Add(new object[] {
                        "",
                        item.IdRol,
                        item.Descripcion
                    });
                }
            }
        }
        #endregion
        public frmModificarRoles()
        {
            InitializeComponent();
        }

        private void frmModificarRoles_Load(object sender, EventArgs e)
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
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["IdRol"].Value.ToString();
                    txtDescripcion.Text = dgvData.Rows[indice].Cells["Rol"].Value.ToString();
                }
            }
            dgvDataPermisos.Rows.Clear();

            List<Permiso> permisos = new ControladorGymPermiso().ObtenerPermisosRol(Convert.ToInt32(txtId.Text));

            foreach (Permiso item in permisos)
            {
                dgvDataPermisos.Rows.Add(new object[] {
                    "",
                    item.NombreMenu,
                    item.Descripcion
                });
            }

        }

        private void dgvDataPermisos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                bool isSelected = Convert.ToBoolean(dgvDataPermisos.Rows[e.RowIndex].Cells["Seleccionado2"].Value);

                if (isSelected)
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

        private void dgvDataPermisos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDataPermisos.Columns[e.ColumnIndex].Name == "btnSeleccionar2")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    foreach (DataGridViewRow row in dgvDataPermisos.Rows)
                    {
                        row.Cells["Seleccionado2"].Value = false;
                    }

                    // Activar el check2 solo en la fila clickeada
                    dgvDataPermisos.Rows[indice].Cells["Seleccionado2"].Value = true;

                    // Refrescar la vista
                    dgvDataPermisos.Refresh();
                    ////
                    txtPermisoRol.Text = dgvDataPermisos.Rows[indice].Cells["NombreMenu"].Value.ToString();
                    txbDescripcion.Text = dgvDataPermisos.Rows[indice].Cells["Descripcion"].Value.ToString();
                }
            }

            txtDescripcion.Enabled = true;
            //txtPermisoRol.Enabled = true;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Crear DataTable para los permisos seleccionados
            DataTable tablaPermisos = new DataTable();
            tablaPermisos.Columns.Add("NombreMenu", typeof(string));
            tablaPermisos.Columns.Add("Descripcion", typeof(string));

            // Agregar los permisos seleccionados a la tabla
            foreach (DataGridViewRow row in dgvDataPermisos.Rows)
            {
                tablaPermisos.Rows.Add(
                    row.Cells["NombreMenu"].Value.ToString(),
                    row.Cells["Descripcion"].Value.ToString()
                );
            }

            // Obtener la descripción del rol desde el formulario
            string descripcionRol = (txtDescripcion.Text.Trim()).ToUpper();
            int IdRol = Convert.ToInt32(txtId.Text);

            Rol unRol = new Rol() { IdRol = IdRol, Descripcion = descripcionRol };

            // Variable para almacenar el mensaje de resultado
            string mensaje = string.Empty;

            // Intentar registrar el rol con sus permisos
            bool respuesta = new ControladorGymRol().Actualizar(unRol, tablaPermisos, out mensaje);

            // Si el rol se registró correctamente
            if (respuesta)
            {
                MessageBox.Show("Rol actualizado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos después del registro exitoso
                txtDescripcion.Clear();
                txtPermisoRol.Clear();
                txtIndice.Text = "-1";
                txtId.Text = "0";

                txtDescripcion.Enabled = !true;
                txtPermisoRol.Enabled = !true;

                dgvData.Rows.Clear();
                dgvDataPermisos.Rows.Clear();

                cargarGrid();
            }
            else
            {
                // Si hubo un error, mostrar el mensaje
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDescripcion.Clear();
            txtPermisoRol.Clear();
            txbDescripcion.Clear();
            txtIndice.Text = "-1";
            txtId.Text = "0";

            txtDescripcion.Enabled = !true;
            txtPermisoRol.Enabled = !true;

            cargarGrid();

            dgvDataPermisos.Rows.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el rol?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    
                    bool respuesta = new ControladorGymRol().Eliminar(Convert.ToInt32(txtId.Text), out mensaje);

                    if (respuesta)
                    {
                        dgvData.Rows.Clear();
                        cargarGrid();
                        /*
                        // La del Profe:
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        */
                        limpiarCampos();
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
