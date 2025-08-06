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
        #region "Métodos"
        private void limpiarCampos()
        {
            chbGrupo.Checked = false;
            chbAccion.Checked = false;

            txtDescripcion.Clear();
            cboGrupo.SelectedIndex = -1;
            dgvPermisosSeleccionados.Rows.Clear();
            txtDescripcion.Enabled = true;
            gbGrupo.Enabled = false;
        }
        #endregion

        public frmMenuRolesYPermisos()
        {
            InitializeComponent();
        }
        private void frmMenuRolesYPermisos_Load(object sender, EventArgs e)
        {
            gbPermisos.Visible = false;
            gbGrupo.Enabled = false;
            gbAccion.Enabled = false;

            // Cargar GRUPOS
            List<Grupo> grupos = new ControladorGymGrupo().Listar();
            foreach (Grupo item in grupos)
            {
                cboGrupo.Items.Add(new OpcionCombo()
                {
                    Valor = item.IdGrupo,
                    Texto = item.NombreMenu,
                    DescripcionPermiso = item.Descripcion
                });
            }

            // Cargar ACCIONES
            List<Accion> acciones = new ControladorGymAccion().ListarTodo();
            foreach (Accion item in acciones)
            {
                cboAccion.Items.Add(new OpcionCombo()
                {
                    Valor = item.IdAccion,
                    Texto = item.NombreAccion,
                    DescripcionPermiso = item.Descripcion
                });
            }

            cboGrupo.DisplayMember = "Texto";
            cboGrupo.ValueMember = "Valor";
            cboGrupo.SelectedIndex = -1;

            cboAccion.DisplayMember = "Texto";
            cboAccion.ValueMember = "Valor";
            cboAccion.SelectedIndex = -1;

            txtDescripcion.Select();
        }

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            string descripcionActual = txtDescripcion.Text.Trim();

            if (e.KeyData == Keys.Enter)
            {
                if (descripcionActual != "") {
                    gbPermisos.Visible = true;
                    txtDescripcion.Enabled = false;
                    gbGrupo.Enabled = false;
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
            if (cboGrupo.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un grupo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener los datos seleccionados
            string descripcionRol = txtDescripcion.Text.ToUpper();
            string descripcionPermiso = (string)((OpcionCombo)cboGrupo.SelectedItem).DescripcionPermiso;
            string nombreMenu = ((OpcionCombo)cboGrupo.SelectedItem).Texto;
            int idGrupo = (int)((OpcionCombo)cboGrupo.SelectedItem).Valor;

            // Validar duplicados
            foreach (DataGridViewRow row in dgvPermisosSeleccionados.Rows)
            {
                if (row.Cells["TipoPermiso"].Value.ToString() == "Grupo" &&
                    Convert.ToInt32(row.Cells["IdGrupo"].Value) == idGrupo)
                {
                    MessageBox.Show("Este grupo ya fue agregado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Agregar fila
            dgvPermisosSeleccionados.Rows.Add(
                descripcionRol,     // Descripcion (del rol)
                "Grupo",            // TipoPermiso
                nombreMenu,         // NombreMenu
                "-",                 // nombreAccion vacío
                descripcionPermiso, // DescripcionPermiso
                idGrupo,            // IdGrupo
                DBNull.Value        // IdAccion
            );
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

            // Crear nuevo DataTable con la estructura del tipo ETabla_Permisos
            DataTable tablaPermisos = new DataTable();
            tablaPermisos.Columns.Add("TipoPermiso", typeof(string));
            tablaPermisos.Columns.Add("IdGrupo", typeof(int));
            tablaPermisos.Columns.Add("IdAccion", typeof(int));

            // Agregar los permisos seleccionados a la tabla
            foreach (DataGridViewRow row in dgvPermisosSeleccionados.Rows)
            {
                string tipoPermiso = row.Cells["TipoPermiso"].Value.ToString();

                object idGrupo = row.Cells["IdGrupo"].Value;
                object idAccion = row.Cells["IdAccion"].Value;

                // Asegurar valores nulos correctamente
                int? idGrupoVal = (idGrupo == DBNull.Value) ? (int?)null : Convert.ToInt32(idGrupo);
                int? idAccionVal = (idAccion == DBNull.Value) ? (int?)null : Convert.ToInt32(idAccion);

                tablaPermisos.Rows.Add(
                    tipoPermiso,
                    idGrupoVal.HasValue ? idGrupoVal.Value : (object)DBNull.Value,
                    idAccionVal.HasValue ? idAccionVal.Value : (object)DBNull.Value
                );
            }

            // Obtener la descripción del rol
            string descripcionRol = txtDescripcion.Text.Trim().ToUpper();

            // Variable para el mensaje
            string mensaje = string.Empty;

            // Llamar al controlador
            bool respuesta = new ControladorGymRol().Registrar(descripcionRol, tablaPermisos, out mensaje);

            if (respuesta)
            {
                MessageBox.Show("Rol registrado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarCampos();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvPermisosSeleccionados_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Verifica que la fila no sea el encabezado (-1 indica el encabezado)
            if (e.RowIndex < 0)
                return;

            // Verifica que está en la primera columna (columna de botones)
            if (e.ColumnIndex == 7)
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
            limpiarCampos();
        }

        private void btnGrupo_Click(object sender, EventArgs e)
        {
            chbAccion.Checked = false;
            dgvPermisosSeleccionados.Rows.Clear();
            cboAccion.SelectedIndex = -1;
            gbAccion.Enabled = false;
            gbGrupo.Enabled = true;
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            chbGrupo.Checked = false;
            dgvPermisosSeleccionados.Rows.Clear();
            cboGrupo.SelectedIndex = -1;
            gbGrupo.Enabled = false;
            gbAccion.Enabled = true;

        }

        private void btnAgregarAccion_Click(object sender, EventArgs e)
        {
            if (cboAccion.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar  una acción.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener info de combo
            string descripcionRol = txtDescripcion.Text.ToUpper();
            string nombreAccion = ((OpcionCombo)cboAccion.SelectedItem).Texto;
            string descripcionPermiso = (string)((OpcionCombo)cboAccion.SelectedItem).DescripcionPermiso;
            int idAccion = (int)((OpcionCombo)cboAccion.SelectedItem).Valor;

            
            // Validar duplicado
            foreach (DataGridViewRow row in dgvPermisosSeleccionados.Rows)
            {
                if (row.Cells["TipoPermiso"].Value.ToString() == "Accion" &&
                    Convert.ToInt32(row.Cells["IdAccion"].Value) == idAccion)
                {
                    MessageBox.Show("Esta acción ya fue agregada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Agregar fila
            dgvPermisosSeleccionados.Rows.Add(
                descripcionRol,     // Descripcion (rol)
                "Accion",           // TipoPermiso
                "-",         // NombreMenu
                nombreAccion,       // nombreAccion
                descripcionPermiso, // DescripcionPermiso
                DBNull.Value,            // IdGrupo
                idAccion            // IdAccion
            );
        }

        private void chbGrupo_CheckedChanged(object sender, EventArgs e)
        {
            if (chbGrupo.Checked)
            {
                // Evitar duplicados: limpiar solo si querés un inicio limpio
                dgvPermisosSeleccionados.Rows.Clear();

                string descripcionRol = txtDescripcion.Text.ToUpper();
                List<Grupo> grupos = new ControladorGymGrupo().Listar();

                foreach (Grupo grupo in grupos)
                {
                    // Validar si ya existe
                    bool yaExiste = dgvPermisosSeleccionados.Rows
                        .Cast<DataGridViewRow>()
                        .Any(r => r.Cells["TipoPermiso"].Value.ToString() == "Grupo" &&
                                  Convert.ToInt32(r.Cells["IdGrupo"].Value) == grupo.IdGrupo);

                    if (!yaExiste)
                    {
                        dgvPermisosSeleccionados.Rows.Add(
                            descripcionRol,
                            "Grupo",
                            grupo.NombreMenu,
                            "-",
                            grupo.Descripcion,
                            grupo.IdGrupo,
                            DBNull.Value
                        );
                    }
                }
            }
            else
            {
                // Si desmarcás, podés remover solo los grupos
                foreach (DataGridViewRow row in dgvPermisosSeleccionados.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["TipoPermiso"].Value.ToString() == "Grupo").ToList())
                {
                    dgvPermisosSeleccionados.Rows.Remove(row);
                }
            }
        }

        private void chbAccion_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAccion.Checked)
            {
                string descripcionRol = txtDescripcion.Text.ToUpper();
                List<Accion> acciones = new ControladorGymAccion().ListarTodo();

                foreach (Accion accion in acciones)
                {
                    // Validar si ya existe
                    bool yaExiste = dgvPermisosSeleccionados.Rows
                        .Cast<DataGridViewRow>()
                        .Any(r => r.Cells["TipoPermiso"].Value.ToString() == "Accion" &&
                                  Convert.ToInt32(r.Cells["IdAccion"].Value) == accion.IdAccion);

                    if (!yaExiste)
                    {
                        dgvPermisosSeleccionados.Rows.Add(
                            descripcionRol,
                            "Accion",
                            "-",
                            accion.NombreAccion,
                            accion.Descripcion,
                            DBNull.Value,
                            accion.IdAccion
                        );
                    }
                }
            }
            else
            {
                // Si desmarcás, podés remover solo las acciones
                foreach (DataGridViewRow row in dgvPermisosSeleccionados.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["TipoPermiso"].Value.ToString() == "Accion").ToList())
                {
                    dgvPermisosSeleccionados.Rows.Remove(row);
                }
            }
        }
    }
}
