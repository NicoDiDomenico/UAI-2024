using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
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
    public partial class frmVentas : Form
    {
        #region "Variables"
        private Usuario usuario;
        #endregion

        #region "Metodos"
        private void limpiarProducto()
        {
            txtIdProducto.Text = "0";
            txtCodProducto.Text = "";
            //txtCodProducto.BackColor = Color.White;
            txtProducto.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            nudCantidad.Value = 1;
        }

        private void calcularTotal()
        {
            // Declara una variable decimal para almacenar el total
            decimal total = 0;

            // Verifica si la tabla tiene filas antes de calcular el total
            if (dgvData.Rows.Count > 0)
            {
                // Recorre todas las filas del DataGridView
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    // Convierte el valor de la celda "SubTotal" a decimal y lo suma al total
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
                }
            }

            // Asigna el total calculado al cuadro de texto, formateándolo con dos decimales
            txtTotalPagar.Text = total.ToString("0.00");
        }

        private void calcularCambio()
        {
            // Verifica si hay un total a pagar, si no hay productos en la venta, muestra un mensaje y sale del método
            if (txtTotalPagar.Text.Trim() == "")
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagoCon; // Variable que almacenará el pago del cliente
            decimal total = Convert.ToDecimal(txtTotalPagar.Text); // Convierte el total a pagar a decimal

            // Si el campo de pago está vacío, lo establece en "0"
            if (txtPagoCon.Text.Trim() == "")
            {
                txtPagoCon.Text = "0";
            }

            // Intenta convertir el texto del pago en un valor decimal
            if (decimal.TryParse(txtPagoCon.Text.Trim(), out pagoCon))
            {
                // Si el pago del cliente es menor al total a pagar, el cambio es 0
                if (pagoCon < total)
                {
                    txtCambio.Text = "0.00";
                }
                else
                {
                    // Calcula el cambio y lo asigna al campo txtCambio con formato de dos decimales
                    decimal cambio = pagoCon - total;
                    txtCambio.Text = cambio.ToString("0.00");
                }
            }
        }
        #endregion

        public frmVentas(Usuario oUsuario = null)
        {
            usuario = oUsuario;
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            /* Iniciaizo */
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
            cboTipoDocumento.DisplayMember = "Texto";
            cboTipoDocumento.ValueMember = "Valor";
            cboTipoDocumento.SelectedIndex = 0;

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtIdProducto.Text = "0";

            txtPagoCon.Text = "";

            txtCambio.Text = "";

            txtTotalPagar.Text = "0";
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtDocumentoCliente.Text = modal._Cliente.Documento;
                    txtNombreCliente.Text = modal._Cliente.NombreCompleto;
                    txtCodProducto.Select();
                }
                else
                {
                    txtDocumentoCliente.Select();
                }
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdProducto.Text = modal._Producto.IdProducto.ToString();
                    txtCodProducto.Text = modal._Producto.Codigo;
                    txtProducto.Text = modal._Producto.Nombre;
                    txtPrecio.Select();
                    txtPrecio.Text = modal._Producto.PrecioVenta.ToString("0.00");
                    txtStock.Text = modal._Producto.Stock.ToString();
                    nudCantidad.Select();
                }
                else
                {
                    txtCodProducto.Select();
                }
            }
        }

        private void txtCodProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtCodProducto.Text && p.Estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    txtCodProducto.BackColor = Color.Honeydew;
                    txtIdProducto.Text = oProducto.IdProducto.ToString();
                    txtProducto.Text = oProducto.Nombre;
                    txtPrecio.Text = oProducto.PrecioVenta.ToString("0.00");
                    txtStock.Text = oProducto.Stock.ToString();
                    nudCantidad.Select();
                }
                else
                {
                    txtCodProducto.BackColor = Color.MistyRose;
                    txtIdProducto.Text = "0";
                    txtProducto.Text = "";
                    txtPrecio.Text = "";
                    txtStock.Text = "";
                    nudCantidad.Value = 1;
                }
            }

        }

        private void btnAgreagar_Click(object sender, EventArgs e)
        {
            decimal precio = 0;
            bool productoExiste = false;

            if (int.Parse(txtIdProducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // No entiendo
            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Precio - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecio.Select();
                return;
            }

            if (Convert.ToInt32(txtStock.Text) < Convert.ToInt32(nudCantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad no puede ser mayor al stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtIdProducto.Text)
                {
                    productoExiste = true;
                    break;
                }
            }

            if (!productoExiste)
            {
                bool respuesta = new CN_Venta().RestarStock(
                    Convert.ToInt32(txtIdProducto.Text),
                    Convert.ToInt32(nudCantidad.Value.ToString())
                );

                if (respuesta)
                {
                    dgvData.Rows.Add(new object[] {
                        txtIdProducto.Text,
                        txtProducto.Text,
                        precio.ToString("0.00"),
                        nudCantidad.Value.ToString(),
                        (nudCantidad.Value * precio).ToString("0.00")
                    });

                    calcularTotal();
                    limpiarProducto();
                    txtCodProducto.Select();
                }
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Verifica que la fila no sea el encabezado (-1 indica el encabezado)
            if (e.RowIndex < 0)
                return;

            // Verifica que está en la primera columna (columna de botones)
            if (e.ColumnIndex == 5)
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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    bool respuesta = new CN_Venta().SumarStock(
                        Convert.ToInt32(dgvData.Rows[index].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dgvData.Rows[index].Cells["Cantidad"].Value.ToString())
                    );

                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(index);
                        calcularTotal();
                    }
                }
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false; // Permite la entrada si es un número
            }
            else
            {
                // Verifica si el primer carácter es un punto
                if (txtPrecio.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true; // No permite que un punto sea el primer carácter
                }
                else
                {
                    // Permite caracteres de control (borrar, enter) y el punto decimal
                    if (char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false; // Permite su ingreso
                    }
                    else
                    {
                        e.Handled = true; // Bloquea cualquier otro carácter
                    }
                }
            }
        }

        private void txtPagoCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false; // Permite la entrada si es un número
            }
            else
            {
                // Verifica si el primer carácter es un punto
                if (txtPagoCon.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true; // No permite que un punto sea el primer carácter
                }
                else
                {
                    // Permite caracteres de control (borrar, enter) y el punto decimal
                    if (char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false; // Permite su ingreso
                    }
                    else
                    {
                        e.Handled = true; // Bloquea cualquier otro carácter
                    }
                }
            }
        }
        /*
        1) KeyPress:
        📌 Se activa cuando el usuario PRESIONA una tecla, pero solo para caracteres imprimibles.

        - Solo captura caracteres imprimibles (a-z, 0-9, .,-+, etc.).
        - NO captura teclas especiales como Ctrl, Shift, Alt, F1, Escape, Enter, Backspace, etc.
        - Propiedad clave: e.KeyChar → Devuelve el carácter ingresado.
        */

        private void txtPagoCon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                calcularCambio();
            }
        }
        /*
        2) KeyDown:
        📌 Se activa cuando el usuario PRESIONA una tecla, antes de que esta se muestre en el control.

        - Se dispara antes de que la tecla tenga un efecto en el control.
        - Captura cualquier tecla, incluyendo teclas de función (F1-F12), Shift, Ctrl, Alt, Escape, Enter, Backspace, Flechas, etc.
        - Útil para detectar combinaciones de teclas (Ctrl + C, Alt + Tab, etc.).
        - Proporciona más información, como si Shift, Ctrl o Alt están activos (e.Modifiers).
        - Propiedad clave: e.KeyCode → Devuelve la tecla presionada.
        */

        private void btnCrearVenta_Click(object sender, EventArgs e)
        {
            // Verifica si los campos esenciales están completos antes de continuar con la venta
            if (txtDocumentoCliente.Text == "")
            {
                MessageBox.Show("Debe ingresar documento del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtNombreCliente.Text == "")
            {
                MessageBox.Show("Debe ingresar nombre del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Se crea una tabla temporal para almacenar los detalles de la venta
            DataTable detalleVenta = new DataTable();
            detalleVenta.Columns.Add("IdProducto", typeof(int));
            detalleVenta.Columns.Add("PrecioVenta", typeof(decimal));
            detalleVenta.Columns.Add("Cantidad", typeof(int));
            detalleVenta.Columns.Add("SubTotal", typeof(decimal));

            // Se recorren los productos agregados en el DataGridView y se añaden a la tabla de detalles
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                detalleVenta.Rows.Add(new object[]
                {
                    row.Cells["IdProducto"].Value.ToString(),
                    row.Cells["Precio"].Value.ToString(),
                    row.Cells["Cantidad"].Value.ToString(),
                    row.Cells["SubTotal"].Value.ToString()
                });
            }

            // Se obtiene el siguiente número correlativo para la venta
            int idCorrelativo = new CN_Venta().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:000000}", idCorrelativo);

            // Se crea el objeto de venta con la información ingresada
            Venta oVenta = new Venta()
            {
                oUsuario = new Usuario() { IdUsuario = usuario.IdUsuario },
                TipoDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem).Texto,
                NumeroDocumento = numeroDocumento,
                DocumentoCliente = txtDocumentoCliente.Text,
                NombreCliente = txtNombreCliente.Text,
                MontoPago = Convert.ToDecimal(txtPagoCon.Text),
                MontoCambio = Convert.ToDecimal(txtCambio.Text),
                MontoTotal = Convert.ToDecimal(txtTotalPagar.Text)
            };

            // Variable para almacenar el mensaje de resultado de la operación
            string mensaje = string.Empty;

            // Se intenta registrar la venta en la base de datos
            bool respuesta = new CN_Venta().Registrar(oVenta, detalleVenta, out mensaje);

            // Si la venta se registra correctamente, se muestra un mensaje y se pregunta si desea copiar el número de venta
            if (respuesta)
            {
                var result = MessageBox.Show("Número de venta generada:\n" + numeroDocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // Si el usuario elige "Sí", el número de venta se copia al portapapeles
                if (result == DialogResult.Yes)
                {
                    Clipboard.SetText(numeroDocumento);
                }

                // Se limpia la interfaz para permitir el ingreso de una nueva venta
                txtDocumentoCliente.Text = "";
                txtNombreCliente.Text = "";
                dgvData.Rows.Clear();
                calcularTotal();
                txtPagoCon.Text = "";
                txtCambio.Text = "";
            }
            else
            {
                // Si la venta no se registra correctamente, se muestra un mensaje de error
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
