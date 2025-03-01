using FontAwesome.Sharp;
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


namespace Vista
{
    public partial class frmGestionarGimnasio : Form
    {
        #region "Variables"
        private static Usuario usuario;
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
                MenuActivo.BackColor = ColorTranslator.FromHtml("#E6EAEA");
            }

            menu.BackColor = Color.SteelBlue;

            MenuActivo = menu;

            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;

            PersonalizarFormulario(formulario);

            subContenedor.Controls.Add(formulario);

            formulario.Show();
        }
        #endregion

        public frmGestionarGimnasio(Usuario usuarioActual)
        {
            usuario = usuarioActual;
            InitializeComponent();
        }

        private void frmGestionarGimnasio_Load(object sender, EventArgs e)
        {
            // No hace falta validar nada, ya se sabe que es el Admin.
        }

        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmMenuUsuarios(usuario));
        }
        private void agregarRolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuRoles, new frmMenuRolesYPermisos());
        }

        private void editarRolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuRoles, new frmModificarRoles());
        }

        private void menuMaquinas_Click(object sender, EventArgs e)
        {
           AbrirFormulario((IconMenuItem)sender, new frmMenuMaquinas());
        }

        private void menuEjercicios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmMenuEjercicios());
        }

        private void menuEquipamiento_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmMenuEquipamiento());
        }

        private void menuRangosHorarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmMenuRangosHorarios());
        }

        private void menuHistorialTurnos_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmMenuHistorialTurnos());
        }

        private void menuNegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmMenuNegocio());
        }

        private void menuAcercaDe_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmMenuAcercaDe());
        }
    }
}
