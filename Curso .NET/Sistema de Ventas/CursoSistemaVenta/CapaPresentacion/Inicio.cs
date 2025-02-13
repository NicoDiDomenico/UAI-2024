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
using FontAwesome.Sharp;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {

        #region "Variables"
        private static Usuario usuarioActual;
        private static IconMenuItem MenuActivo = null;
        private static Form formularioActivo = null;
        #endregion

        #region "Métodos"
        private void PersonalizarFormulario(Form formulario)
        {
            formulario.TopLevel = false; // Indica que el formulario no es una ventana de nivel superior. Esto es necesario para poder insertar el formulario dentro de otro contenedor (como un panel).
            formulario.FormBorderStyle = FormBorderStyle.None; // Quita el borde del formulario para que se vea integrado en el contenedor sin la barra de título ni los bordes.
            formulario.Dock = DockStyle.Fill; // Hace que el formulario ocupe todo el espacio disponible en el contenedor.
            formulario.BackColor = Color.SteelBlue; // Cambia el color de fondo del formulario al color SteelBlue.
        }

        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.White;
            }

            menu.BackColor = Color.Silver;

            MenuActivo = menu;

            if(formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;

            PersonalizarFormulario(formulario);

            contenedor.Controls.Add(formulario);

            formulario.Show();
        }

        private void validarPermisos(List<Permiso> listaPermisos)
        {
            
            foreach (IconMenuItem iconMenu in menu.Items)
            {
                // Forma 1 - Mi forma, los IconMenuItem en false.
                /*
                foreach (Permiso unPermiso in listaPermisos)
                {
                    if (iconMenu.Name == unPermiso.NombreMenu)
                    {
                        iconMenu.Visible = true;
                    }
                }
                */
                // Forma 2 - Mas eficiente, los IconMenuItem en true.

                bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconMenu.Name); // m es una variable temporal que representa cada elemento de la lista ListaPermisos mientras se evalúa la condición dentro del método Any().

                if (encontrado == false) {
                    iconMenu.Visible = false;
                }
                
            }
        }
        #endregion

        public Inicio(Usuario objUsuario = null) // Este se ejecuta 1ro 
        {
            // Momentaneo, sacar '= null'
            if (objUsuario == null)
                usuarioActual = new Usuario() { NombreCompleto = "ADMIN PREDEFINIDO", IdUsuario = 1 };
            else
                usuarioActual = objUsuario;
            //

            // usuarioActual = objUsuario;

            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e) // Este se ejecuta 2do
        {
            List<Permiso> listaPermisos = new CN_Permiso().Listar(usuarioActual.IdUsuario);

            validarPermisos(listaPermisos);
            
            lblUsuario.Text = usuarioActual.NombreCompleto;
        }

        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmUsuarios());
            // 1er parametro: menuUsuarios, recordemos que el objeto que se envia es el propio iconMenuItem (se debera convertir de Objeto -> iconMenuItem)
            // 2do Parametro: el formulario correspondiente a menuUsuarios
        }

        private void subMenuCategoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmCategoria());
        }

        private void subMenuProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmProducto());
        }

        private void subMenuRegistrarVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuVentas, new frmVentas());
        }

        private void subMenuVerDetalleVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuVentas, new frmDetalleVenta());
        }

        private void subMenuRegistrarCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuCompras, new frmCompras());
        }

        private void subMenuVerDetalleCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuCompras, new frmDetalleCompra());
        }

        private void menuClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes());
        }

        private void menuProvedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores());
        }

        private void menuReportes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmReportes());
        }

        private void subMenuNegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmNegocio());
        }
    }
}
