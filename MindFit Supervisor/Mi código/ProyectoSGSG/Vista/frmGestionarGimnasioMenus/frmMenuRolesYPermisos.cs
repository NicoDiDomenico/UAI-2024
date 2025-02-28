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

using Modelo;
using Controlador;
using CapaPresentacion.Utilidades;

namespace Vista
{
    public partial class frmMenuRolesYPermisos : Form
    {
        public frmMenuRolesYPermisos()
        {
            InitializeComponent();
        }
        private void frmMenuRolesYPermisos_Load(object sender, EventArgs e)
        {
            // Para ComboBox de Permiso 
            List<Permiso> permisos = new ControladorGymPermiso().ListarTodos();
            
            foreach (Permiso item in permisos)
            {
                int count = 0;
                cboPermiso.Items.Add(new OpcionCombo() { Valor = count, Texto = item.NombreMenu });
                count++;
            }

            cboPermiso.DisplayMember = "Texto";
            cboPermiso.ValueMember = "Valor";
            cboPermiso.SelectedIndex = -1;
        }

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            string descripcionActual = txtDescripcion.Text.Trim();

            if (e.KeyData == Keys.Enter)
            {
                if (descripcionActual != "") {
                    txtDescripcion.Enabled = false;
                    gbPermiso.Enabled = true;
                } else
                {
                    MessageBox.Show("Debe ingresar una descripcion para el Rol", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Evita que se agregue duplicado
                }
            }
        }

        private void btnAgreagar_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un permiso
            if (cboPermiso.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un permiso antes de agregar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Sale del método para evitar errores
            }

            string nuevaDescripcion = txtDescripcion.Text.ToUpper();
            string nuevoPermiso = ((OpcionCombo)cboPermiso.SelectedItem).Texto;

            // Verifica si el permiso ya existe en alguna fila del DataGridView
            foreach (DataGridViewRow row in dgvPermisosSeleccionados.Rows)
            {
                if (row.Cells["NombreMenu"].Value != null && row.Cells["NombreMenu"].Value.ToString() == nuevoPermiso)
                {
                    MessageBox.Show("Este permiso ya ha sido agregado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Evita que se agregue duplicado
                }
            }

            // Si no existe, agregar la nueva descripción y permiso
            dgvPermisosSeleccionados.Rows.Add(new object[] { nuevaDescripcion, nuevoPermiso });
        }

        private void btnRegistrarRol_Click(object sender, EventArgs e)
        {
            // Verificar si el campo de descripción del rol está vacío
            if (txtDescripcion.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar una descripción para el rol", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Verificar si hay permisos seleccionados
            if (dgvPermisosSeleccionados.Rows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar al menos un permiso para el rol", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Crear DataTable para los permisos seleccionados
            DataTable tablaPermisos = new DataTable();
            tablaPermisos.Columns.Add("NombreMenu", typeof(string));

            // Agregar los permisos seleccionados a la tabla
            foreach (DataGridViewRow row in dgvPermisosSeleccionados.Rows)
            {
                tablaPermisos.Rows.Add(row.Cells["NombreMenu"].Value.ToString());
            }

            // Obtener la descripción del rol desde el formulario
            string descripcionRol = (txtDescripcion.Text.Trim()).ToUpper();

            // Variable para almacenar el mensaje de resultado
            string mensaje = string.Empty;

            // Intentar registrar el rol con sus permisos
            bool respuesta = new ControladorGymRol().Registrar(descripcionRol, tablaPermisos, out mensaje);

            // Si el rol se registró correctamente
            if (respuesta)
            {
                MessageBox.Show("Rol registrado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos después del registro exitoso
                txtDescripcion.Text = "";
                dgvPermisosSeleccionados.Rows.Clear();
            }
            else
            {
                // Si hubo un error, mostrar el mensaje
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvPermisosSeleccionados_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Verifica que la fila no sea el encabezado (-1 indica el encabezado)
            if (e.RowIndex < 0)
                return;

            // Verifica que está en la primera columna (columna de botones)
            if (e.ColumnIndex == 2)
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

        private void dgvPermisosSeleccionados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPermisosSeleccionados.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    dgvPermisosSeleccionados.Rows.RemoveAt(index);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDescripcion.Clear();
            cboPermiso.SelectedIndex = -1;
            dgvPermisosSeleccionados.Rows.Clear();
            txtDescripcion.Enabled = true;
            gbPermiso.Enabled = false;
        }
    }
}
