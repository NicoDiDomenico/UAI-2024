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

namespace Vista
{
    public partial class frmMenuUsuarios : Form
    {
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

            cboGenero.SelectedIndex = -1;
            cboRol.SelectedIndex = -1;
            cboEstado.SelectedIndex = -1;

            txtNombreYApellido.Select();
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
                //item.Genero == true ? 1 : 0,
                item.Genero == "Masculino" ? "Masculino" : "Femenino",
                item.Rol.IdRol,
                item.Rol.Descripcion,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo" : "No Activo",
                item.FechaRegistro
                });
            }
        }
        #endregion

        public frmMenuUsuarios()
        {
            InitializeComponent();
        }

        private void frmMenuUsuarios_Load(object sender, EventArgs e)
        {
            // Para ComboBox de Genero
            cboGenero.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Femenino" });
            cboGenero.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Masculino" });

            cboGenero.DisplayMember = "Texto";
            cboGenero.ValueMember = "Valor";
            cboGenero.SelectedIndex = -1;

            // Para ComboBox de Rol 
            List<Rol> listaRol = new ControladorGymRol().Listar();

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
                    Genero = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? "Femenino" : "Masculino",
                    Rol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cboRol.SelectedItem).Valor) },
                    Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
                };
                string mensaje = string.Empty;

                if (unUsuario.IdUsuario == 0)
                {
                    int idUsuarioGenerado = new ControladorGymUsuario().Registrar(unUsuario, out mensaje);

                    if (idUsuarioGenerado != 0)
                    {
                        dgvData.Rows.Clear();
                        cargarGrid();
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(mensaje);
                    }
                }
                else
                {
                    bool rta = new ControladorGymUsuario().Editar(unUsuario, out mensaje);

                    if (rta)
                    {
                        dgvData.Rows.Clear();
                        cargarGrid();
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(mensaje);
                    }
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
                /*
                // Obtiene el ancho y alto del recurso (icono o imagen "check220")
                var w = Properties.Resources.check2.Width;
                var h = Properties.Resources.check2.Height;

                // Calcula la posición X para centrar la imagen horizontalmente
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;

                // Calcula la posición Y para centrar la imagen verticalmente
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                // Dibuja la imagen "check2" en la celda, centrada
                e.Graphics.DrawImage(Properties.Resources.check2, new Rectangle(x, y, w, h));
                */
                // Indica que la celda ha sido manejada y no necesita ser pintada nuevamente
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
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtNombreYApellido.Text = dgvData.Rows[indice].Cells["NombreYApellido"].Value.ToString();
                    txtEmail.Text = dgvData.Rows[indice].Cells["Email"].Value.ToString();
                    txtTelefono.Text = dgvData.Rows[indice].Cells["Telefono"].Value?.ToString();
                    txtDireccion.Text = dgvData.Rows[indice].Cells["Direccion"].Value?.ToString();
                    txtCiudad.Text = dgvData.Rows[indice].Cells["Ciudad"].Value?.ToString();
                    txtNroDocumento.Text = dgvData.Rows[indice].Cells["NroDocumento"].Value.ToString();
                    //dtpFechaNacimiento.Value = Convert.ToDateTime(dgvData.Rows[indice].Cells["FechaNacimiento"].Value);
                    if (dgvData.Rows[indice].Cells["FechaNacimiento"].Value != DBNull.Value &&
                        dgvData.Rows[indice].Cells["FechaNacimiento"].Value != null)
                    {
                        // Intentar parsear el valor a DateTime
                        if (DateTime.TryParse(dgvData.Rows[indice].Cells["FechaNacimiento"].Value.ToString(), out DateTime fechaNacimiento))
                        {
                            dtpFechaNacimiento.Value = fechaNacimiento;
                        }
                        else
                        {
                            MessageBox.Show("El formato de la fecha es incorrecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dtpFechaNacimiento.Value = DateTime.Today; // Asigna una fecha por defecto si hay un error
                        }
                    }
                    else
                    {
                        dtpFechaNacimiento.Value = DateTime.Today; // Valor por defecto si la celda está vacía o es nula
                    }

                    txtNombreUsuario.Text = dgvData.Rows[indice].Cells["NombreUsuario"].Value.ToString();
                    txtClave.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();
                    txtConfirmarClave.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();

                    // Recorre todos los elementos dentro del ComboBox 'cboGenero'
                    foreach (OpcionCombo oc in cboGenero.Items)
                    {
                        // Compara el texto del ítem del ComboBox con el valor de la celda "Genero" en el DataGridView
                        // La comparación es entre cadenas de texto (Ej: "Masculino" o "Femenino")
                        if (oc.Texto == dgvData.Rows[indice].Cells["Genero"].Value.ToString())
                        {
                            // Si encuentra coincidencia, obtiene el índice del ítem dentro del ComboBox
                            int indice_combo = cboGenero.Items.IndexOf(oc);

                            // Establece el índice encontrado como el seleccionado en el ComboBox
                            cboGenero.SelectedIndex = indice_combo;

                            // Termina el bucle porque ya encontró el valor correcto y no necesita seguir buscando
                            break;
                        }
                    }

                    // Recorre todos los ítems del combo box 'cboRol'
                    foreach (OpcionCombo oc in cboRol.Items)
                    {
                        // Compara el valor del ítem del combo con el valor 'IdRol' de la fila seleccionada en el DataGridView
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdRol"].Value))
                        {
                            // Si encuentra coincidencia, obtiene el índice del ítem correspondiente en el combo box
                            int indice_combo = cboRol.Items.IndexOf(oc);

                            // Establece el ítem encontrado como el seleccionado en el combo box
                            cboRol.SelectedIndex = indice_combo;

                            // Sale del bucle una vez encontrado el valor para evitar búsquedas innecesarias
                            break;
                        }
                    }

                    // Recorre todos los ítems del combo box 'cboEstado'
                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        // Compara el valor del ítem del combo con el valor 'EstadoValor' de la fila seleccionada en el DataGridView
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            // Si encuentra coincidencia, obtiene el índice del ítem correspondiente en el combo box
                            int indice_combo = cboEstado.Items.IndexOf(oc);

                            // Establece el ítem encontrado como el seleccionado en el combo box
                            cboEstado.SelectedIndex = indice_combo;

                            // Sale del bucle una vez encontrado el valor para evitar búsquedas innecesarias
                            break;
                        }
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
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
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
            txtBusqueda.Clear();
        }
    }
}
