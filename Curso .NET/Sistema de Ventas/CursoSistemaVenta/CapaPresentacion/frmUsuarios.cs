using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;

namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {
        #region "Métodos"
        private void limpiarCampos()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtDocumento.Text = "";
            txtNombreCompleto.Text = "";
            txtCorreo.Text = "";
            txtClave.Text = "";
            txtConfirmarClave.Text = "";
            cboRol.SelectedIndex = -1;
            cboEstado.SelectedIndex = -1;

            txtDocumento.Select();
        }

        private void cargarGrid()
        {
            // Para el Grid - MOSTRAR TODOS LOS USUARIOS
            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach (Usuario item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] {
                "",
                item.IdUsuario,
                item.Documento,
                item.NombreCompleto,
                item.Correo,
                item.Clave,
                item.oRol.IdRol,
                item.oRol.Descripcion,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }
        #endregion

        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            //// Los ComboBox se pueden rellerar de dos maneras:
            /// Items.Add: Si tenés pocos datos que no cambian mucho (como "Activo"/"No Activo").
            /// DataSource: Si los datos vienen de una base de datos y pueden cambiar o si hay muchos registros.

            // Para ComboBox de Estado
            // Agregar elementos al ComboBox:
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            // Configurar qué se muestra y qué se usa como valor:
            cboEstado.DisplayMember = "Texto"; // Le dice al ComboBox que lo que debe mostrar es el valor de la propiedad Texto de OpcionCombo. Así, en la interfaz se verá "Activo" o "No Activo".
            cboEstado.ValueMember = "Valor"; // El ValueMember en un ComboBox indica qué propiedad del objeto agregado al ComboBox será considerada como el valor interno.
            cboEstado.SelectedIndex = -1; // Esto selecciona el primer elemento de la lista por defecto, es decir, "Activo".

            // Para ComboBox de Rol 
            List<Rol> listaRol = new CN_Rol().Listar();

            foreach (Rol item in listaRol)
            {

                cboRol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }

            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = -1;

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

            // Para ComboBox de filtrado - con DataSource
            /*
            // Crear una lista de objetos OpcionCombo
            List<OpcionCombo> listaOpciones = new List<OpcionCombo>();

            // Recorrer las columnas del DataGridView y llenar la lista
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible && columna.Name != "btnseleccionar")
                {
                    listaOpciones.Add(new OpcionCombo { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }

            // Asignar la lista como origen de datos del ComboBox
            cbobusqueda.DataSource = listaOpciones;

            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = -1;
            */

            cargarGrid();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try {
                Usuario unUsuario = new Usuario {
                    IdUsuario = Convert.ToInt32(txtId.Text), // Este id va a servir para saber cuando hay que editar o registrar un usuario
                    Documento = txtDocumento.Text,
                    NombreCompleto = txtNombreCompleto.Text,
                    Correo = txtCorreo.Text,
                    Clave = txtClave.Text,
                    oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cboRol.SelectedItem).Valor) },
                    Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
                }; 
                string mensaje = string.Empty;

                if (unUsuario.IdUsuario == 0)
                {
                    int idUsuarioGenerado = new CN_Usuario().Registrar(unUsuario, out mensaje);

                    if (idUsuarioGenerado != 0)
                    {
                        // Mi Forma:
                        dgvData.Rows.Clear();
                        cargarGrid();
                        /*
                        // La del Profe:
                        // Rows.Add(new object[] { ... }): Este método añade una nueva fila a la tabla. La fila se construye como un array de objetos (object[]), donde cada elemento del array corresponde a una celda de esa fila.
                        // new object[] { ... } --> Forma basica para declarar, definir y asignar un arreglo de objetos.
                        dgvData.Rows.Add(new object[] { "", idusuariogenerado, txtDocumento.Text, txtNombreCompleto.Text, txtCorreo.Text, txtClave.Text,
                            ((OpcionCombo)cboRol.SelectedItem).Valor.ToString(),
                            ((OpcionCombo)cboRol.SelectedItem).Texto.ToString(),
                            ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(), // Se hace un casting a OpcionCombo para obtener el Valor y Texto de los ComboBox, ya que porque cboEstado.SelectedItem devuelve un object.
                            ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()
                        });
                        */
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(mensaje);
                    }
                } else
                {
                    bool rta = new CN_Usuario().Editar(unUsuario, out mensaje);

                    if (rta)
                    {
                        dgvData.Rows.Clear();
                        cargarGrid();
                        /*
                        // La del Profe:
                        DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                        row.Cells["Id"].Value = txtid.Text;
                        row.Cells["Documento"].Value = txtdocumento.Text;
                        row.Cells["NombreCompleto"].Value = txtnombrecompleto.Text;
                        row.Cells["Correo"].Value = txtcorreo.Text;
                        row.Cells["Clave"].Value = txtclave.Text;
                        row.Cells["IdRol"].Value = ((OpcionCombo)cborol.SelectedItem).Valor.ToString();
                        row.Cells["Rol"].Value = ((OpcionCombo)cborol.SelectedItem).Texto.ToString();
                        row.Cells["EstadoValor"].Value = ((OpcionCombo)cboestado.SelectedItem).Valor.ToString();
                        row.Cells["Estado"].Value = ((OpcionCombo)cboestado.SelectedItem).Texto.ToString();
                        });
                        */
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(mensaje);
                    }
                }
            } catch (Exception ex)
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

                // Obtiene el ancho y alto del recurso (icono o imagen "check20")
                var w = Properties.Resources.check.Width;
                var h = Properties.Resources.check.Height;

                // Calcula la posición X para centrar la imagen horizontalmente
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;

                // Calcula la posición Y para centrar la imagen verticalmente
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                // Dibuja la imagen "check" en la celda, centrada
                e.Graphics.DrawImage(Properties.Resources.check, new Rectangle(x, y, w, h));

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
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtNombreCompleto.Text = dgvData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtCorreo.Text = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    txtClave.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();
                    txtConfirmarClave.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();

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

                    bool respuesta = new CN_Usuario().Eliminar(objusuario, out mensaje);

                    if (respuesta)
                    {
                        dgvData.Rows.Clear();
                        cargarGrid();
                        /*
                        // La del Profe:
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        */
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        // Lo cambie por txtBusqueda_TextChanged(...){...} => La visibilidad de btnBuscar lo puse en false
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
            // Mio - Ya que usé txtBusqueda_TextChanged
            txtBusqueda.Clear();

            // Del Profe - Ya que usó btnBuscar_Click
            /*
            txtbusqueda.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
            */
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
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
            } catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } 
        }
    }
}
