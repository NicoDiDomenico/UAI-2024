using CapaEntidad;
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
    public partial class frmCompras : Form
    {
        #region "Variables"
        private Usuario usuarioActual;
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
            using (var modal = new mdProveedor()) // Crea una nueva instancia del formulario mdProveedor
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
    }
}
