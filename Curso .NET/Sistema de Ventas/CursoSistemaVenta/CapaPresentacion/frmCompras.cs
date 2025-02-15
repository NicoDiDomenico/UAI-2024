using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
//using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmCompras : Form
    {
        #region "Variables"
        private Usuario usuarioActual;
        #endregion

        #region "Metodos"
        private void limpiarProducto()
        {
            txtIdProducto.Text = "0";
            txtCodProducto.Text = "";
            txtCodProducto.BackColor = Color.White;
            txtProducto.Text = "";
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";
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

        #endregion

        public frmCompras(Usuario usuarioActual)
        {
            this.usuarioActual = usuarioActual;
            InitializeComponent();
        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });

            cboTipoDocumento.DisplayMember = "Texto";
            cboTipoDocumento.ValueMember = "Valor";
            cboTipoDocumento.SelectedIndex = 0;

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            // Acá llenó los txt de id con 0 pero yo lo hice directamente en el form.
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            // Ejecuto un Modal Personalizado
            using (var modal = new mdProveedor()) // Crea una nueva instancia del formulario mdProveedor,  El using es una forma más segura y eficiente de trabajar con formularios modales, ya que garantiza que los recursos del formulario se liberen automáticamente.
            {
                var result = modal.ShowDialog();  // Muestra el cuadro de diálogo modal y almacena el resultado.

                if (result == DialogResult.OK)
                {
                    // Clave como usa las propiedades de otro formulario a traves de instanciarlo en la clase actual, de esta forma evito pasar por parámetros el objeto que necesito acá.
                    txtIdProveedor.Text = modal._Proveedor.IdProveedor.ToString();
                    txtDocProveedor.Text = modal._Proveedor.Documento;
                    txtNombreProveedor.Text = modal._Proveedor.RazonSocial;
                } else
                {
                    txtDocProveedor.Select(); // El método Select(); en txtDocProveedor se usa para establecer el foco en el campo de texto txtDocProveedor
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
                    txtPrecioCompra.Select();
                }
                else
                {
                    txtDocProveedor.Select(); 
                }
            }
        }

        private void txtCodProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) // Verifica si la tecla presionada es "Enter"
            {
                // Busca un producto en la lista de productos cuyo código coincida con el texto ingresado en txtcodproducto
                // y cuyo estado sea activo (true). Si no encuentra coincidencias, devuelve null.
                Producto oProducto = new CN_Producto().Listar()
                                        .Where(p => p.Codigo == txtCodProducto.Text && p.Estado == true)
                                        .FirstOrDefault();

                if (oProducto != null) // Si se encontró el producto en la lista
                {
                    txtCodProducto.BackColor = Color.Honeydew; // Cambia el color de fondo del campo de código a verde claro
                    txtIdProducto.Text = oProducto.IdProducto.ToString(); // Asigna el ID del producto al campo txtidproducto
                    txtProducto.Text = oProducto.Nombre; // Muestra el nombre del producto en el campo txtproducto
                    txtPrecioCompra.Select(); // Coloca el foco en el campo txtpreciocompra para que el usuario ingrese el precio
                }
                else // Si no se encontró el producto en la lista
                {
                    txtCodProducto.BackColor = Color.MistyRose; // Cambia el color de fondo del campo de código a rojo claro
                    txtIdProducto.Text = "0"; // Establece el ID del producto en "0" indicando que no se encontró
                    txtProducto.Text = ""; // Deja vacío el campo del nombre del producto
                }
            }

        }

        // Evento que se ejecuta cuando se presiona el botón "Agregar"
        private void btnAgreagar_Click(object sender, EventArgs e)
        {
            // Variables para almacenar los precios de compra y venta
            decimal preciocompra = 0;
            decimal precioventa = 0;

            // Variable para verificar si el producto ya existe en la lista
            bool producto_existe = false;

            // Verifica si se ha seleccionado un producto antes de agregarlo
            if (int.Parse(txtIdProducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return; // Se detiene la ejecución si no hay producto seleccionado
            }

            // Intenta convertir el valor del campo txtPrecioCompra a decimal
            if (!decimal.TryParse(txtPrecioCompra.Text, out preciocompra)) // Aquí faltaba el nombre del control txtPrecioCompra antes del ".Text"
            {
                MessageBox.Show("Precio Compra - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecioCompra.Select(); // Coloca el cursor en el campo con error para que el usuario lo corrija
                return;
            }

            // Intenta convertir el valor del campo txtPrecioVenta a decimal
            if (!decimal.TryParse(txtPrecioVenta.Text, out precioventa))
            {
                MessageBox.Show("Precio Venta - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecioVenta.Select(); // Coloca el cursor en el campo con error para que el usuario lo corrija
                return;
            }

            // Verifica si el producto ya existe en el DataGridView antes de agregarlo
            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtIdProducto.Text)
                {
                    producto_existe = true; // Se encontró el producto en la lista
                    break; // Se detiene la búsqueda para optimizar el rendimiento
                }
            }

            // Si el producto NO existe, se agrega una nueva fila al DataGridView
            if (!producto_existe)
            {
                dgvData.Rows.Add(new object[]
                {
                    txtIdProducto.Text,  // ID del producto
                    txtProducto.Text,    // Nombre del producto
                    preciocompra.ToString("0.00"),  // Precio de compra con formato de dos decimales
                    precioventa.ToString("0.00"),   // Precio de venta con formato de dos decimales
                    nudCantidad.Value.ToString(),   // Cantidad seleccionada
                    (nudCantidad.Value * preciocompra).ToString("0.00") // Total calculado (cantidad * precio de compra)
                });

                calcularTotal();
                limpiarProducto();
                txtCodProducto.Select();
            }
            else
            {
                MessageBox.Show("Este producto ya fue agregado.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Verifica que la fila no sea el encabezado (-1 indica el encabezado)
            if (e.RowIndex < 0)
                return;

            // Verifica que está en la primera columna (columna de botones)
            if (e.ColumnIndex == 6)
            {
                // Pinta la celda con sus partes predeterminadas
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // Obtiene el ancho y alto del recurso (icono o imagen "trash20")
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
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    dgvData.Rows.RemoveAt(indice);
                    calcularTotal();
                }
            }
        }

        // Evento KeyPress para controlar qué caracteres se pueden ingresar en un campo de texto
        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número (0-9)
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false; // Permite el ingreso del número
            }
            else
            {
                // Si el campo está vacío y se presiona ".", bloquea la entrada (no permite iniciar con un punto)
                if (txtPrecioCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true; // Bloquea la entrada del carácter "."
                }
                else
                {
                    // Permite teclas de control (como Backspace) o un solo punto decimal "."
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false; // Permite estas teclas
                    }
                    else
                    {
                        e.Handled = true; // Bloquea cualquier otro carácter que no sea un número, "." o control
                    }
                }
            }
        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false; 
            }
            else
            {
                if (txtPrecioCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false; 
                    }
                    else
                    {
                        e.Handled = true; 
                    }
                }
            }
        }
    }
}
