using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Modales
{
    public partial class mdProveedor : Form
    {
        #region "Variables"
        public Proveedor _Proveedor { get; set; }
        #endregion

        public mdProveedor()
        {
            InitializeComponent();
        }

        private void mdProveedor_Load(object sender, EventArgs e)
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

            List<Proveedor> lista = new CN_Proveedor().Listar();

            foreach (Proveedor item in lista)
            {
                dgvData.Rows.Add(new object[] { item.IdProveedor, item.Documento, item.RazonSocial });
            }

        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;  // Obtiene el índice de la fila seleccionada en el DataGridView.
            int iColumn = e.ColumnIndex; // Obtiene el índice de la columna seleccionada.

            if (iRow >= 0 && iColumn > 0)  // Verifica que la selección sea válida (no cabeceras).
            {
                _Proveedor = new Proveedor()  // Crea una nueva instancia de la clase Proveedor y asigna valores.
                {
                    IdProveedor = Convert.ToInt32(dgvData.Rows[iRow].Cells["Id"].Value.ToString()), // Convierte el ID de la celda seleccionada a entero.
                    Documento = dgvData.Rows[iRow].Cells["Documento"].Value.ToString(),  // Obtiene el valor del documento como string.
                    RazonSocial = dgvData.Rows[iRow].Cells["RazonSocial"].Value.ToString() // Obtiene la razón social del proveedor.
                };

                this.DialogResult = DialogResult.OK;  // Establece el resultado del cuadro de diálogo como "OK". 
                this.Close();  // Cierra el formulario actual.
            }

        }

        //// (1) y (2) es la forma que el profe hace los filtros, yo lo hacia con el evento TextChanged.
        // (1)
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
        // (2)
        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }

        }
        ////
        
    }
}
