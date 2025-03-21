using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Modelo;
using Controlador;
using System.IO;
using System.Drawing.Drawing2D;
using System.Net.Http;
using System.Windows.Documents;

namespace Vista
{
    public partial class Inicio : Form
    {
        #region "Variables"
        private static Usuario usuarioActual; // La variable usuarioActual es compartida por todas las instancias de la clase Inicio.
        private static IconMenuItem MenuActivo = null;
        private static Form formularioActivo = null;
        private int cupoAct;
        private int cupoMax;
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
                MenuActivo.BackColor = ColorTranslator.FromHtml("#2C4C7F");
            }

            menu.BackColor = Color.SteelBlue;
            menu.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            
            MenuActivo = menu;

            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;

            PersonalizarFormulario(formulario);

            panelPrincipal.Visible = false;
            botones.Visible = false;
            contenedor.Controls.Add(formulario);

            formulario.Show();
        }

        private void validarPermisos()
        {
            List<Permiso> listaPermisos = new ControladorGymPermiso().Listar(usuarioActual.IdUsuario);

            // 🔹 Deshabilitar y cambiar color en el MenuStrip (botones.Items)
            foreach (IconMenuItem iconMenu in botones.Items)
            {
                // Verificar si el usuario tiene permiso por Grupo o por Acción
                bool tienePermiso = listaPermisos.Any(p =>
                    (p.Grupo != null && p.Grupo.NombreMenu == iconMenu.Name) ||
                    (p.Accion != null && p.Accion.NombreAccion == iconMenu.Name)
                );

                if (!tienePermiso)
                {
                    iconMenu.Enabled = false;
                    iconMenu.BackColor = Color.Gainsboro;
                }
            }

            // 🔹 Deshabilitar y cambiar color en el otro MenuStrip (botonesTop.Items)
            foreach (IconMenuItem iconMenu in botonesTop.Items)
            {
                // 🔥 Eliminar "Top" del nombre antes de comparar
                string nombreNormalizado = iconMenu.Name.Replace("Top", "");

                // Verificar si el usuario tiene permiso por Grupo o por Acción
                bool tienePermiso = listaPermisos.Any(p =>
                    (p.Grupo != null && p.Grupo.NombreMenu == nombreNormalizado) ||
                    (p.Accion != null && p.Accion.NombreAccion == nombreNormalizado)
                );

                if (!tienePermiso)
                {
                    iconMenu.Enabled = false;
                    iconMenu.BackColor = Color.Gainsboro;
                }
            }
        }
        public Image ByteToImage(byte[] imageBytes)
        {
            // Crea un flujo de memoria
            MemoryStream ms = new MemoryStream();

            // Escribe los bytes de la imagen en el flujo de memoria
            ms.Write(imageBytes, 0, imageBytes.Length);

            // Crea un objeto Bitmap a partir del flujo de memoria
            Image image = new Bitmap(ms);

            // Retorna la imagen convertida
            return image;
        }

        private void cargarGrid()
        {
            List<Turno> turnos = new ControladorGymTurno().ListarTurnosHorarioActual();

            if (turnos.Count > 0) // Asegurar que haya registros
            {
                // Tomar el primer turno como referencia para obtener CupoActual y CupoMaximo
                cupoAct = turnos[0].unRangoHorario.CupoActual;
                cupoMax = turnos[0].unRangoHorario.CupoMaximo;
            }
            else
            {
                // Si no hay turnos en el rango horario actual, asignar valores por defecto
                cupoAct = 0;
                cupoMax = 0;
            }

            dgvData.Rows.Clear(); // Limpiar el DataGridView antes de agregar datos
            foreach (Turno item in turnos)
            {
                dgvData.Rows.Add(new object[] {
                    item.IdTurno,
                    item.FechaTurno.ToString("dd/MM/yyyy"),
                    item.unRangoHorario.IdRangoHorario,
                    item.unRangoHorario.HoraDesde.ToString(@"hh\:mm"),
                    item.unRangoHorario.HoraHasta.ToString(@"hh\:mm"),
                    item.CodigoIngreso,
                    item.unUsuario.IdUsuario,
                    item.unSocio.NombreYApellido,
                    item.unUsuario.NombreYApellido,
                    item.unSocio.IdSocio,
                    item.unRangoHorario.CupoActual, // **Ahora obtiene de CupoFecha**
                    item.unRangoHorario.CupoMaximo,
                    item.EstadoTurno
                });
                cupoAct = item.unRangoHorario.CupoActual;
                cupoMax = item.unRangoHorario.CupoMaximo;
            }
        }
        private void cargarTxt()
        {
            lblHoraActual.Text = "";

            List<RangoHorario> horarios = new ControladorGymRangoHorario().Listar();
            TimeSpan horaActual = DateTime.Now.TimeOfDay; // Obtener la hora actual como TimeSpan

            foreach (RangoHorario item in horarios)
            {
                if (item.HoraDesde <= horaActual && item.HoraHasta >= horaActual)
                {
                    cupoMax = item.CupoMaximo;
                }
            }

            lblHoraActual.Text = "Turnos " + DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("HH:00") + " hs - Cupos " + cupoAct + "/" + cupoMax + ": ";
        }

        #endregion

        public Inicio(Usuario ousuario)
        {
            usuarioActual = ousuario;

            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            // Variable para verificar si se obtuvo correctamente el logo
            bool obtenido = true;

            // Llama al método ObtenerLogo de la capa de negocio para recuperar el logo de la base de datos
            byte[] byteimage = new ControladorGymGimnasio().ObtenerLogo(out obtenido);

            // Si el logo se obtuvo correctamente, lo convierte a imagen y lo muestra en el PictureBox
            if (obtenido)
            {
                picLogoInicio.Image = ByteToImage(byteimage);
            }

            Gimnasio gym = new ControladorGymGimnasio().ObtenerDatos();

            lblLogo.Text = gym.NombreGimnasio;

            validarPermisos();
            lblUsuario.Text = usuarioActual.NombreYApellido;

            cargarGrid();

            cargarTxt();

            /* Actualizar todos los estados de Socios que estan en curso pero ya vencieron */
            List<Socio> socios = new ControladorGymSocio().Listar();

            foreach (Socio socio in socios)
            {
                List <Turno> sociosActualizados = new ControladorGymTurno().Listar(socio.IdSocio);
            }
        }
        // Gestionar Rutinas
        private void menuTopGestionarRutinas_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmGestionarRutinas());
            cargarGrid();
            cargarTxt();
        }

        private void menuGestionarRutinas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuTopGestionarRutinas, new frmGestionarRutinas());
            cargarGrid();
            cargarTxt();
        }

        // Ver Socios
        private void menuTopSocios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmSocios(usuarioActual));
            dgvData.Rows.Clear(); // Me parece que esta al pedo
            cargarGrid();
            cargarTxt();
        }

        private void menuSocios_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuTopSocios, new frmSocios(usuarioActual));
            dgvData.Rows.Clear();
            cargarGrid();
            cargarTxt();
        }

        // Gestionar Gimnasio
        private void menuTopGestionarGimnasio_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmGestionarGimnasio(usuarioActual));
            cargarGrid();
            cargarTxt();
        }

        private void menuGestionarGimnasio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuTopGestionarGimnasio, new frmGestionarGimnasio(usuarioActual));
            cargarGrid();
            cargarTxt();
        }

        private void picLogo_Click(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();
            cargarGrid();
            cargarTxt();

            if (formularioActivo != null)
            {
                formularioActivo.Close();
                botones.Visible = true;
                panelPrincipal.Visible = true;
                menuTopGestionarRutinas.BackColor = ColorTranslator.FromHtml("#2C4C7F");
                menuTopSocios.BackColor = ColorTranslator.FromHtml("#2C4C7F");
                menuTopGestionarGimnasio.BackColor = ColorTranslator.FromHtml("#2C4C7F");
                this.Refresh();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            frmValidarIngreso formulario = new frmValidarIngreso();
            formulario.ShowDialog(); // Muestra el formulario en modo modal
            dgvData.Rows.Clear();
            cargarGrid();
            cargarTxt();
        }
    }
}
