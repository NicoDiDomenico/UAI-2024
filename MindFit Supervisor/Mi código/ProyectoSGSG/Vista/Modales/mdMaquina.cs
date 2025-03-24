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
    public partial class mdMaquina : Form
    {
        #region "Variables"
        public Maquina _Maquina { get; set; }
        #endregion

        #region "Métodos"
        private void cargarGrid()
        {
            // Para el Grid - MOSTRAR TODOS LOS USUARIOS
            List<Maquina> listaMaquina = new ControladorGymMaquina().Listar();

            foreach (Maquina item in listaMaquina)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdElemento,
                    item.NombreElemento,
                    item.FechaFabricacion,
                    item.FechaCompra,
                    item.Precio,
                    item.Peso,
                    item.TipoMaquina,
                    item.EsElectrica == true ? "Si" : "No",
                });
            }
        }
        #endregion
        public mdMaquina()
        {
            InitializeComponent();
        }

        private void mdMaquina_Load(object sender, EventArgs e)
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

        private void dgvData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;  // Obtiene el índice de la fila seleccionada en el DataGridView.
            int iColumn = e.ColumnIndex; // Obtiene el índice de la columna seleccionada.

            if (iRow >= 0 && iColumn > 0)  // Verifica que la selección sea válida (no cabeceras).
            {
                _Maquina = new Maquina()  // Crea una nueva instancia de la clase Proveedor y asigna valores.
                {
                    IdElemento = Convert.ToInt32(dgvData.Rows[iRow].Cells["Id"].Value),
                    NombreElemento = dgvData.Rows[iRow].Cells["NombreElemento"].Value.ToString(),
                    FechaFabricacion = Convert.ToDateTime(dgvData.Rows[iRow].Cells["FechaFabricacion"].Value),
                    FechaCompra = Convert.ToDateTime(dgvData.Rows[iRow].Cells["FechaCompra"].Value),
                    Precio = Convert.ToSingle(dgvData.Rows[iRow].Cells["Precio"].Value),
                    Peso = Convert.ToInt32(dgvData.Rows[iRow].Cells["Peso"].Value),
                    TipoMaquina = dgvData.Rows[iRow].Cells["TipoMaquina"].Value.ToString(),
                    EsElectrica = dgvData.Rows[iRow].Cells["EsElectrica"].Value.ToString() == "Si" // desde string a bool
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
