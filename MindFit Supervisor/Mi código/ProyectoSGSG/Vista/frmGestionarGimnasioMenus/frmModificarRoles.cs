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
            txtIdGrupo.Text = "0";
            txbDescripcion.Text = "";
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
            gbGrupo.Enabled = false;
            gbAccion.Enabled = false;

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
            List<Accion> acciones = new ControladorGymAccion().ListarAccionesConGrupo();
            foreach (Accion item in acciones)
            {
                cboAccion.Items.Add(new OpcionCombo()
                {
                    Valor = item.IdAccion,
                    Texto = item.NombreAccion,
                    DescripcionPermiso = item.Descripcion,
                    IdGrupo = item.IdGrupo, // Asignar el IdGrupo al campo I
                    NombreMenu = item.unGrupo.NombreMenu, // Asignar el nombre del grupo al campo NombreMenu
                    DescripcionGrupo = item.unGrupo.Descripcion // Asignar la descripción del grupo al campo DescripcionGrupo
                });
            }

            cboGrupo.DisplayMember = "Texto";
            cboGrupo.ValueMember = "Valor";
            cboGrupo.SelectedIndex = -1;

            cboAccion.DisplayMember = "Texto";
            cboAccion.ValueMember = "Valor";
            cboAccion.SelectedIndex = -1;

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

                if (isSelected) // Solo dibuja el checkGrande si la celda está seleccionada
                {
                    var w = Properties.Resources.checkGrande.Width;
                    var h = Properties.Resources.checkGrande.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(Properties.Resources.checkGrande, new Rectangle(x, y, w, h));
                }
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdGrupo.Text = "-1";
            cboGrupo.SelectedIndex = -1;
            cboAccion.SelectedIndex = -1;

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

                    // Activar el checkGrande solo en la fila clickeada
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
                if (item.TipoPermiso == "Grupo")
                {
                    dgvDataPermisos.Rows.Add(new object[] {
                        "",
                        item.Grupo.NombreMenu,
                        item.Grupo.Descripcion,
                        item.Grupo.IdGrupo,
                        "Grupo",
                        "-", 
                        "-", 
                        null
                    });
                    gbGrupo.Enabled = true;
                    gbAccion.Enabled = false;
                }
                else if (item.TipoPermiso == "Accion")
                {
                    dgvDataPermisos.Rows.Add(new object[] {
                        "",
                        item.Grupo.NombreMenu,
                        item.Grupo.Descripcion,
                        item.Grupo.IdGrupo,
                        "Accion",
                        item.Accion.NombreAccion,
                        item.Accion.Descripcion,
                        item.Accion.IdAccion
                    });
                    gbAccion.Enabled = true;
                    gbGrupo.Enabled = false;
                }
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

            if (e.ColumnIndex == 9)
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

        private void dgvDataPermisos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDataPermisos.Columns[e.ColumnIndex].Name == "btnSeleccionar2")
            {
                txbDescripcion.Enabled = true;

                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIdGrupo.Text = Convert.ToString(dgvDataPermisos.Rows[indice].Cells["IdGrupo"].Value);

                    foreach (DataGridViewRow row in dgvDataPermisos.Rows)
                    {
                        row.Cells["Seleccionado2"].Value = false;
                    }
                    // Activar el checkGrande solo en la fila clickeada
                    dgvDataPermisos.Rows[indice].Cells["Seleccionado2"].Value = true;

                    // Refrescar la vista
                    dgvDataPermisos.Refresh();
                    ////
                    txtPermisoRol.Text = dgvDataPermisos.Rows[indice].Cells["NombreMenu"].Value.ToString();
                    txbDescripcion.Text = dgvDataPermisos.Rows[indice].Cells["Descripcion"].Value.ToString();
                }
            }

            if (dgvDataPermisos.Columns[e.ColumnIndex].Name == "btnEliminarFila")
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    dgvDataPermisos.Rows.RemoveAt(index);
                }
            }

            txtDescripcion.Enabled = true;
            //txtPermisoRol.Enabled = true;
        }

        // AHORA FALTA QUE SE GUARDE LAS ACCIONES O GRUPOS NUEVOS.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado un rol
            if (txtId.Text == "0")
            {
                MessageBox.Show("Debe seleccionar un rol para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear tabla de permisos
            DataTable tablaPermisos = new DataTable();
            tablaPermisos.Columns.Add("IdGrupo", typeof(int));
            tablaPermisos.Columns.Add("IdAccion", typeof(int));
            tablaPermisos.Columns.Add("IdUsuario", typeof(int));

            // Cargar permisos desde el DataGridView
            foreach (DataGridViewRow row in dgvDataPermisos.Rows)
            {
                // Validación segura para IdGrupo
                object valorIdGrupo = row.Cells["IdGrupo"].Value;
                int? idGrupo = (valorIdGrupo != null && valorIdGrupo != DBNull.Value) ? Convert.ToInt32(valorIdGrupo) : (int?)null;

                // Validación segura para IdAccion
                object valorIdAccion = row.Cells["IdAccion"].Value;
                int? idAccion = (valorIdAccion != null && valorIdAccion != DBNull.Value) ? Convert.ToInt32(valorIdAccion) : (int?)null;

                // Por ahora, IdUsuario queda null (puede agregarse si hay usuario logueado)
                int? idUsuario = null;

                // Agregar fila a la tabla
                DataRow fila = tablaPermisos.NewRow();
                fila["IdGrupo"] = idGrupo.HasValue ? (object)idGrupo.Value : DBNull.Value;
                fila["IdAccion"] = idAccion.HasValue ? (object)idAccion.Value : DBNull.Value;
                fila["IdUsuario"] = idUsuario.HasValue ? (object)idUsuario.Value : DBNull.Value;
                tablaPermisos.Rows.Add(fila);
            }

            // Obtener datos del rol
            int IdRol = Convert.ToInt32(txtId.Text);
            string descripcionRol = (txtDescripcion.Text.Trim()).ToUpper();
            Rol unRol = new Rol() { IdRol = IdRol, Descripcion = descripcionRol };

            // Validar y limpiar IdGrupo para el grupo editable
            int idGrupoSinEspacio;
            if (!int.TryParse(txtIdGrupo.Text, out idGrupoSinEspacio))
            {
                idGrupoSinEspacio = -1;
            }

            // Ejecutar actualización
            string mensaje = string.Empty;
            bool respuesta = new ControladorGymRol().Actualizar(unRol, tablaPermisos, idGrupoSinEspacio, txbDescripcion.Text, out mensaje);

            // Mostrar resultado
            if (respuesta)
            {
                MessageBox.Show("Rol actualizado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar formulario
                txtDescripcion.Clear();
                txtPermisoRol.Clear();
                txbDescripcion.Clear();
                txtIndice.Text = "-1";
                txtId.Text = "0";

                txtDescripcion.Enabled = false;
                txtPermisoRol.Enabled = false;
                txbDescripcion.Enabled = false;

                dgvData.Rows.Clear();
                dgvDataPermisos.Rows.Clear();

                foreach (DataGridViewRow row in dgvData.Rows)
                    row.Cells["Seleccionado"].Value = false;

                dgvData.Refresh();

                foreach (DataGridViewRow row in dgvDataPermisos.Rows)
                    row.Cells["Seleccionado2"].Value = false;

                dgvDataPermisos.Refresh();

                cargarGrid();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            gbGrupo.Enabled = false;
            gbAccion.Enabled = false;

            cboGrupo.SelectedIndex = -1;
            cboAccion.SelectedIndex = -1;

            txtDescripcion.Clear();
            txtPermisoRol.Clear();
            txbDescripcion.Clear();
            txtIndice.Text = "-1";
            txtIdGrupo.Text = "-1";
            txtId.Text = "0";

            txtDescripcion.Enabled = false;
            txtPermisoRol.Enabled = false;
            txbDescripcion.Enabled = false;

            cargarGrid();

            dgvDataPermisos.Rows.Clear();

            // Desmarcar todas las filas en la columna "Seleccionado"
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Cells["Seleccionado"].Value = false;
            }

            // Refrescar el DataGridView para reflejar los cambios
            dgvData.Refresh();
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
                        dgvDataPermisos.Rows.Clear();
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

        private void btnAgreagar1_Click(object sender, EventArgs e)
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
            foreach (DataGridViewRow row in dgvDataPermisos.Rows)
            {
                if (row.Cells["TipoPermiso"].Value.ToString() == "Grupo" &&
                    Convert.ToInt32(row.Cells["IdGrupo"].Value) == idGrupo)
                {
                    MessageBox.Show("Este grupo ya fue agregado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Agregar fila
            dgvDataPermisos.Rows.Add(
                "",
                nombreMenu,         // NombreMenu
                descripcionPermiso, // DescripcionPermiso
                idGrupo,            // IdGrupo
                "Grupo",            // TipoPermiso
                "-",                 // nombreAccion vacío
                "-",     // Descripcion (del rol)
                DBNull.Value        // IdAccion
                // Faltaria el usuario pero no va ni en el dgv
            );
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
            string NombreMenu = (string)((OpcionCombo)cboAccion.SelectedItem).NombreMenu;
            string DescripcionGrupo = (string)((OpcionCombo)cboAccion.SelectedItem).DescripcionGrupo;
            int idAccion = (int)((OpcionCombo)cboAccion.SelectedItem).Valor;
            int idGrupo = (int)((OpcionCombo)cboAccion.SelectedItem).IdGrupo;


            // Validar duplicado
            foreach (DataGridViewRow row in dgvDataPermisos.Rows)
            {
                if (row.Cells["TipoPermiso"].Value.ToString() == "Accion" &&
                    Convert.ToInt32(row.Cells["IdAccion"].Value) == idAccion)
                {
                    MessageBox.Show("Esta acción ya fue agregada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Agregar fila
            dgvDataPermisos.Rows.Add(
                "",
                NombreMenu,         // NombreMenu
                DescripcionGrupo,         // DescripcionPermiso
                idGrupo,            // IdGrupo
                "Accion",           // TipoPermiso
                nombreAccion,       // nombreAccion
                descripcionPermiso, // DescripcionPermiso
                idAccion            // IdAccion
            );
        }
    }
}
