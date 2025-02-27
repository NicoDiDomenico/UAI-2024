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

namespace CapaPresentacion
{
    public partial class frmCategoria : Form
    {
        #region "Métodos"
        private void limpiarCampos()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtDescripcion.Text = "";
            cboEstado.SelectedIndex = -1;

            txtDescripcion.Select();
        }

        private void cargarGrid()
        {
            // Para el Grid - MOSTRAR TODOS LAS CATEGORIAS
            List<Categoria> listaCategoria = new CN_Categoria().Listar();

            foreach (Categoria item in listaCategoria)
            {
                dgvData.Rows.Add(new object[] {
                "",
                item.IdCategoria,
                item.Descripcion,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }
        #endregion

        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
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
                Categoria unaCategoria = new Categoria
                {
                    IdCategoria = Convert.ToInt32(txtId.Text),
                    Descripcion = txtDescripcion.Text,
                    Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
                };
                string mensaje = string.Empty;

                if (unaCategoria.IdCategoria == 0)
                {
                    int idCategoriaGenerado = new CN_Categoria().Registrar(unaCategoria, out mensaje);

                    if (idCategoriaGenerado != 0)
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
                    bool rta = new CN_Categoria().Editar(unaCategoria, out mensaje);

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
                    txtDescripcion.Text = dgvData.Rows[indice].Cells["Descripcion"].Value.ToString();

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
                if (MessageBox.Show("¿Desea eliminar la categoria?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Categoria objCategoria = new Categoria()
                    {
                        IdCategoria = Convert.ToInt32(txtId.Text)
                    };

                    bool respuesta = new CN_Categoria().Eliminar(objCategoria, out mensaje);

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

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
