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
using System.Windows.Forms;

namespace Vista.Modales
{
    public partial class mdEntrenador : Form
    {
        #region "Variables"
        public Usuario _Usuario { get; set; }
        #endregion
        #region "Métodos"

        private void cargarGrid()
        {
            // Para el Grid - MOSTRAR TODOS LOS USUARIOS
            List<Usuario> listaUsuario = new ControladorGymUsuario().Listar();

            if (listaUsuario == null || listaUsuario.Count == 0)
            {
                MessageBox.Show("No se encontraron usuarios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (Usuario item in listaUsuario)
            {
                // Validación para evitar un NullReferenceException
                if (item.Rol != null && item.Rol.IdRol == 3)
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
                item.Rol.IdRol,
                item.Rol.Descripcion,
                item.Estado ? 1 : 0,
                item.Estado ? "Activo" : "No Activo",
                item.FechaRegistro
            });
                }
            }
        }

        #endregion

        public mdEntrenador()
        {
            InitializeComponent();
        }

        private void mdEntrenador_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true)
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            cargarGrid();
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;  // Obtiene el índice de la fila seleccionada en el DataGridView.
            int iColumn = e.ColumnIndex; // Obtiene el índice de la columna seleccionada.

            if (iRow >= 0 && iColumn > 0)  // Verifica que la selección sea válida (no cabeceras).
            {
                _Usuario = new Usuario()  // Crea una nueva instancia de la clase Proveedor y asigna valores.
                {
                    IdUsuario = Convert.ToInt32(dgvData.Rows[iRow].Cells["Id"].Value.ToString()),
                    NombreYApellido = dgvData.Rows[iRow].Cells["NombreYApellido"].Value.ToString()
                };

                this.DialogResult = DialogResult.OK;  // Establece el resultado del cuadro de diálogo como "OK". 
                this.Close();  // Cierra el formulario actual.
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();
            // Obtiene el valor seleccionado en el ComboBox 'cbobusqueda' como el nombre de la columna a filtrar.

            if (dgvData.Rows.Count > 0) // Verifica si el DataGridView tiene filas.
            {
                foreach (DataGridViewRow row in dgvData.Rows) // Recorre cada fila del DataGridView.
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper()
                        .Contains(txtBusqueda.Text.Trim().ToUpper()))
                    {
                        row.Visible = true; // Si la celda contiene el texto de búsqueda, la fila se muestra.
                    }
                    else
                    {
                        row.Visible = false; // Si no coincide, la fila se oculta.
                    }
                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
