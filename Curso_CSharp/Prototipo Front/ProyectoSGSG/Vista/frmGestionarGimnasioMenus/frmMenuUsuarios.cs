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
using System.Windows.Controls;
using CapaPresentacion.Utilidades;
using FontAwesome.Sharp;

namespace Vista
{
    public partial class frmMenuUsuarios : Form
    {
        #region "Variables"
        private static Usuario usuarioIniciado;
        private List<Accion> accionesSeleccionadas;
        private int indiceEstado;
        #endregion

        #region "Métodos"
        private void limpiarCampos()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtNombreYApellido.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtCiudad.Text = "";
            txtNroDocumento.Text = "";
            dtpFechaNacimiento.Value = DateTime.Today;
            txtNombreUsuario.Text = "";
            txtClave.Text = "";
            txtConfirmarClave.Text = "";

            cboGenero.SelectedIndex = -1;
            cboRol.SelectedIndex = -1;
            cboEstado.SelectedIndex = -1;

            txtNombreYApellido.Select();

            // Asegurar que la lista está inicializada antes de limpiar
            if (accionesSeleccionadas == null)
            {
                accionesSeleccionadas = new List<Accion>();
            }
            else
            {
                accionesSeleccionadas.Clear();
            }

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
            List<Usuario> listaUsuario = new ControladorGymUsuario().Listar();

            foreach (Usuario item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdUsuario,
                    item.NombreYApellido,
                    item.Email,
                    item.Telefono,
                    item.Direccion,
                    item.Ciudad,
                    item.NroDocumento,
                    item.FechaNacimiento,
                    item.NombreUsuario,
                    item.Clave,
                    item.Genero == "Masculino" ? "Masculino" : "Femenino",
                    item.Rol != null ? item.Rol.IdRol : (object)DBNull.Value,  // Manejo de NULL en IdRol
                    item.Rol != null ? item.Rol.Descripcion : " - ",  // Si no tiene rol, muestra "Sin Rol"
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo" : "No Activo",
                    item.FechaRegistro
                });
            }

        }
        private void AbrirFormulario(Form formulario)
        {
            formulario.StartPosition = FormStartPosition.CenterScreen; // Centra la ventana
            formulario.Load += (s, e) =>
            {
                // Ajustar la posición para que la ventana baje 50 píxeles desde la posición centrada
                formulario.Top += 155;
                formulario.Left += 232;
            };

            // Muestra como ventana modal
            formulario.ShowDialog(); 
        }

        private void RegistrarUsuario(Usuario unUsuario, string mensaje)
        {
            int idUsuarioGenerado = new ControladorGymUsuario().Registrar(unUsuario, out mensaje);

            if (idUsuarioGenerado != 0)
            {

                dgvData.Rows.Clear();
                cargarGrid();
                limpiarCampos();
                // Me falto configurar el mensaje en la BD, por eso se muestar vacio
                MessageBox.Show(mensaje);
            }
            else
            {
                MessageBox.Show(mensaje);
            }
        }
        #endregion

        public frmMenuUsuarios(Usuario usuario)
        {
            usuarioIniciado = usuario;
            InitializeComponent();
        }

        private void frmMenuUsuarios_Load(object sender, EventArgs e)
        {
            //accionesSeleccionadas.Clear();
            // Para ComboBox de Genero
            cboGenero.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Femenino" });
            cboGenero.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Masculino" });

            cboGenero.DisplayMember = "Texto";
            cboGenero.ValueMember = "Valor";
            cboGenero.SelectedIndex = -1;

            // Para ComboBox de Rol 
            //List<Rol> listaRol = new ControladorGymRol().Listar();
            List<Rol> listaRol = new ControladorGymRol().ListarConAcciones();

            foreach (Rol item in listaRol)
            {
                cboRol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }

            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = -1;

            // Para ComboBox de Estado
            // Agregar elementos al ComboBox:
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            // Configurar qué se muestra y qué se usa como valor:
            cboEstado.DisplayMember = "Texto"; // Le dice al ComboBox que lo que debe mostrar es el valor de la propiedad Texto de OpcionCombo. Así, en la interfaz se verá "Activo" o "No Activo".
            cboEstado.ValueMember = "Valor"; // El ValueMember en un ComboBox indica qué propiedad del objeto agregado al ComboBox será considerada como el valor interno.
            cboEstado.SelectedIndex = -1; // Esto selecciona el primer elemento de la lista por defecto, es decir, "Activo".

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClave.Text == txtConfirmarClave.Text)
                {
                    Usuario unUsuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(txtId.Text), // Este id va a servir para saber cuando hay que editar o registrar un usuario
                        NombreYApellido = txtNombreYApellido.Text,
                        Email = txtEmail.Text,
                        Telefono = txtTelefono.Text,
                        Direccion = txtDireccion.Text,
                        Ciudad = txtCiudad.Text,
                        NroDocumento = Convert.ToInt32(txtNroDocumento.Text),
                        FechaNacimiento = dtpFechaNacimiento.Value,
                        NombreUsuario = txtNombreUsuario.Text,
                        Clave = txtClave.Text,
                        Genero = Convert.ToInt32(((OpcionCombo)cboGenero.SelectedItem).Valor) == 1 ? "Femenino" : "Masculino",
                        Rol = cboRol.SelectedItem != null ?
                            new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cboRol.SelectedItem).Valor) }
                            : null,
                        Acciones = accionesSeleccionadas,
                        Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
                    };
                    string mensaje = string.Empty;

                    if (unUsuario.IdUsuario == 0)
                    {
                        //RegistrarUsuario(unUsuario, mensaje);
                        
                        if (unUsuario.Rol != null && (accionesSeleccionadas == null || !accionesSeleccionadas.Any()))
                        {
                            RegistrarUsuario(unUsuario, mensaje);
                        }
                        else if (unUsuario.Rol == null && accionesSeleccionadas != null && accionesSeleccionadas.Any())
                        {   RegistrarUsuario(unUsuario, mensaje);
                        } else
                        {
                            MessageBox.Show("Debe asignar un permiso", "Advertencia");
                        }
                    }
                    else
                    {
                        //int cantidad = unUsuario.Acciones.Count();
                        //MessageBox.Show(Convert.ToString(cantidad));

                        bool rta = new ControladorGymUsuario().Editar(unUsuario, out mensaje);

                        if (rta)
                        {
                            dgvData.Rows.Clear();
                            cargarGrid();
                            limpiarCampos();
                            MessageBox.Show(mensaje);
                        }
                        else
                        {
                            MessageBox.Show(mensaje);
                        }
                    }
                } else
                {
                    MessageBox.Show("Las claves no coinciden", "Advertencia");
                    txtConfirmarClave.Select();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            panelAcciones.Visible = false;
            if (e.RowIndex >= 0 && dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                // Asegurar que la lista no sea null antes de limpiar
                if (accionesSeleccionadas == null)
                {
                    accionesSeleccionadas = new List<Accion>();
                }
                else
                {
                    accionesSeleccionadas.Clear();
                }

                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    if (dgvData.Rows[indice].Cells["IdRol"]?.Value != DBNull.Value && dgvData.Rows[indice].Cells["IdRol"]?.Value != null) btnGrupo.Select();
                    else btnAccion.Select();

                    cboRol.Enabled = true;

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

                    // **Verificaciones antes de asignar valores**
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["Id"]?.Value?.ToString() ?? "0";
                    txtNombreYApellido.Text = dgvData.Rows[indice].Cells["NombreYApellido"]?.Value?.ToString() ?? "";
                    txtEmail.Text = dgvData.Rows[indice].Cells["Email"]?.Value?.ToString() ?? "";
                    txtTelefono.Text = dgvData.Rows[indice].Cells["Telefono"]?.Value?.ToString() ?? "";
                    txtDireccion.Text = dgvData.Rows[indice].Cells["Direccion"]?.Value?.ToString() ?? "";
                    txtCiudad.Text = dgvData.Rows[indice].Cells["Ciudad"]?.Value?.ToString() ?? "";
                    txtNroDocumento.Text = dgvData.Rows[indice].Cells["NroDocumento"]?.Value?.ToString() ?? "0";

                    // **Verificar FechaNacimiento antes de asignar**
                    if (dgvData.Rows[indice].Cells["FechaNacimiento"]?.Value != DBNull.Value &&
                        dgvData.Rows[indice].Cells["FechaNacimiento"]?.Value != null)
                    {
                        if (DateTime.TryParse(dgvData.Rows[indice].Cells["FechaNacimiento"].Value.ToString(), out DateTime fechaNacimiento))
                        {
                            dtpFechaNacimiento.Value = fechaNacimiento;
                        }
                        else
                        {
                            MessageBox.Show("El formato de la fecha es incorrecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dtpFechaNacimiento.Value = DateTime.Today;
                        }
                    }
                    else
                    {
                        dtpFechaNacimiento.Value = DateTime.Today;
                    }

                    txtNombreUsuario.Text = dgvData.Rows[indice].Cells["NombreUsuario"]?.Value?.ToString() ?? "";
                    txtClave.Text = dgvData.Rows[indice].Cells["Clave"]?.Value?.ToString() ?? "";
                    txtConfirmarClave.Text = dgvData.Rows[indice].Cells["Clave"]?.Value?.ToString() ?? "";

                    // **Verificar Genero antes de asignar**
                    if (dgvData.Rows[indice].Cells["Genero"]?.Value != DBNull.Value && dgvData.Rows[indice].Cells["Genero"]?.Value != null)
                    {
                        foreach (OpcionCombo oc in cboGenero.Items)
                        {
                            if (oc.Texto == dgvData.Rows[indice].Cells["Genero"].Value.ToString())
                            {
                                cboGenero.SelectedIndex = cboGenero.Items.IndexOf(oc);
                                break;
                            }
                        }
                    }
                    else
                    {
                        cboGenero.SelectedIndex = -1;
                    }

                    // **Verificar IdRol antes de asignar**
                    if (dgvData.Rows[indice].Cells["IdRol"]?.Value != DBNull.Value && dgvData.Rows[indice].Cells["IdRol"]?.Value != null)
                    {
                        if (int.TryParse(dgvData.Rows[indice].Cells["IdRol"].Value.ToString(), out int idRol))
                        {
                            foreach (OpcionCombo oc in cboRol.Items)
                            {
                                if (Convert.ToInt32(oc.Valor) == idRol)
                                {
                                    cboRol.SelectedIndex = cboRol.Items.IndexOf(oc);
                                    panelRol.Enabled = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        cboRol.SelectedIndex = -1;
                        panelRol.Enabled = false;
                    }

                    // **Verificar Estado antes de asignar**
                    if (dgvData.Rows[indice].Cells["EstadoValor"]?.Value != DBNull.Value && dgvData.Rows[indice].Cells["EstadoValor"]?.Value != null)
                    {
                        if (int.TryParse(dgvData.Rows[indice].Cells["EstadoValor"].Value.ToString(), out int estadoValor))
                        {
                            foreach (OpcionCombo oc in cboEstado.Items)
                            {
                                if (Convert.ToInt32(oc.Valor) == estadoValor)
                                {
                                    cboEstado.SelectedIndex = cboEstado.Items.IndexOf(oc);
                                    indiceEstado = cboEstado.Items.IndexOf(oc);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        cboEstado.SelectedIndex = -1;
                    }

                    if (int.TryParse(txtId.Text, out int idUsuario) && idUsuario == usuarioIniciado?.IdUsuario)
                    {
                        cboRol.Enabled = false;
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != usuarioIniciado.IdUsuario)
            {
                if (Convert.ToInt32(txtId.Text) != 0)
                {
                    if (MessageBox.Show("¿Desea eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string mensaje = string.Empty;
                        Usuario objusuario = new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(txtId.Text)
                        };

                        bool respuesta = new ControladorGymUsuario().Eliminar(objusuario, out mensaje);

                        if (respuesta)
                        {
                            dgvData.Rows.Clear();
                            cargarGrid();
                            limpiarCampos();
                            MessageBox.Show(mensaje, "Mensaje");
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No se puede eliminar el usuario actual");
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
            txtBusqueda.Clear();
        }

        private void btnGrupo_Click(object sender, EventArgs e)
        {
            panelRol.Enabled = true;

            //accionesSeleccionadas = new List<Accion>();
            //MessageBox.Show($"Se seleccionaron {accionesSeleccionadas.Count} acciones.");
            
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            
            cboRol.SelectedIndex = -1; // Se deselecciona el rol
            panelRol.Enabled = false;

            frmAcciones formAcciones = new frmAcciones(Convert.ToInt32(txtId.Text), accionesSeleccionadas);

            // Abre el formulario modal con la ubicación deseada
            AbrirFormulario(formAcciones);

            // Verifica si el usuario seleccionó acciones antes de cerrar el formulario
            if (formAcciones.DialogResult == DialogResult.OK)
            {
                //accionesSeleccionadas = formAcciones.AccionesSeleccionadas;
                accionesSeleccionadas = formAcciones.AccionesSeleccionadas ?? new List<Accion>(); // Asegura que no sea null

                if (accionesSeleccionadas != null && accionesSeleccionadas.Count > 0)
                {
                    //MessageBox.Show($"Se seleccionaron {accionesSeleccionadas.Count} acciones.");
                }
            }
            
            //// Intento de hacer que en el cboEstado se recupere el estado antes de apretar en acciones
            /*
            // Guardar los valores actuales antes de modificar
            int rolSeleccionadoIndex = cboRol.SelectedIndex;
            bool panelRolHabilitado = panelRol.Enabled;

            // Limpiar selección de rol para trabajar solo con acciones
            cboRol.SelectedIndex = -1;
            panelRol.Enabled = false;

            // Crear el formulario
            frmAcciones formAcciones = new frmAcciones(Convert.ToInt32(txtId.Text), accionesSeleccionadas);

            // Mostrar centrado usando tu método AbrirFormulario
            AbrirFormulario(formAcciones);

            // Una vez cerrado el formulario
            if (formAcciones.DialogResult == DialogResult.OK)
            {
                // Guardó cambios -> Actualizar acciones seleccionadas
                accionesSeleccionadas = formAcciones.AccionesSeleccionadas ?? new List<Accion>();
            }
            else
            {
                // Cerró con la X o canceló -> restaurar el estado previo
                cboRol.SelectedIndex = rolSeleccionadoIndex;
                panelRol.Enabled = panelRolHabilitado;
                if (accionesSeleccionadas.Count() == 0)
                {
                    btnGrupo.Select();
                } else btnAccion.Select();
            }
            */
        }

        private void cboRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstAccionesRol.Items.Clear();

            if (cboRol.SelectedItem != null)
            {
                int idRolSeleccionado = Convert.ToInt32(((OpcionCombo)cboRol.SelectedItem).Valor);

                // Buscar el Rol con ese ID
                Rol rolSeleccionado = new ControladorGymRol().ListarConAcciones()
                    .FirstOrDefault(r => r.IdRol == idRolSeleccionado);

                if (rolSeleccionado != null && rolSeleccionado.Acciones != null)
                {
                    foreach (var accion in rolSeleccionado.Acciones)
                    {
                        lstAccionesRol.Items.Add(accion);
                    }
                }
            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            /*
            if (panelAcciones.Visible)
            {
                panelAcciones.Visible = false;
            } else panelAcciones.Visible = true; 
            */
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            /*
            panelAcciones.Visible = false;
            */
        }
    }
}
