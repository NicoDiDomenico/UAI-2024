using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class mdProducto : Form
    {
        #region "Métodos"
        public Producto _Producto { get; set; }
        #endregion

        #region "Métodos"
        private void cargarGrid()
        {
            List<Producto> lista = new CN_Producto().Listar();

            foreach (Producto item in lista)
            {
                dgvData.Rows.Add(new object[] {
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.oCategoria.Descripcion,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
                });
            }
        }
        #endregion

        public mdProducto()
        {
            InitializeComponent();
        }

        private void mdProducto_Load(object sender, EventArgs e)
        {
            // Para ComboBox de filtrado - con Items.Add
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true)
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = -1;

            cargarGrid();
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;  
            int iColumn = e.ColumnIndex; 

            if (iRow >= 0 && iColumn > 0)  
            {
                _Producto = new Producto()
                {
                    IdProducto = Convert.ToInt32(dgvData.Rows[iRow].Cells["Id"].Value.ToString()), // Convierte el ID de la celda seleccionada a entero.
                    Codigo = dgvData.Rows[iRow].Cells["Codigo"].Value.ToString(),
                    Nombre = dgvData.Rows[iRow].Cells["Nombre"].Value.ToString(), 
                    //Categoria = dgvData.Rows[iRow].Cells["RazonSocial"].Value.ToString(), 
                    Stock = Convert.ToInt32(dgvData.Rows[iRow].Cells["Stock"].Value),
                    PrecioCompra = Convert.ToDecimal(dgvData.Rows[iRow].Cells["PrecioCompra"].Value),
                    PrecioVenta = Convert.ToDecimal(dgvData.Rows[iRow].Cells["PrecioVenta"].Value)
                    
                };

                this.DialogResult = DialogResult.OK;  // Establece el resultado del cuadro de diálogo como "OK". 
                this.Close();  // Cierra el formulario actual.
            }
        }
        private void btnBusqueda_Click(object sender, EventArgs e)
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
