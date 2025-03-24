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
using Vista.Utilidades;

namespace Vista
{
    public partial class frmGestionarRutinas : Form
    {
        #region "Variables"
        private int idRangoHorarioActual; // Variable global para almacenar el Id del rango horario actual
        private List<Rutina> RutinasActuales;
        #endregion
        #region "Métodos"
        private void cargarCBO()
        {
            cboRangoHorario.Items.Clear();
            List<RangoHorario> listaRangos = new ControladorGymRangoHorario().Listar();

            // Obtener la hora actual sin minutos ni segundos
            int horaActual = DateTime.Now.Hour;
            idRangoHorarioActual = -1; // Inicializamos con un valor no válido

            foreach (RangoHorario item in listaRangos)
            {
                cboRangoHorario.Items.Add(new OpcionCombo()
                {
                    Valor = item.IdRangoHorario,
                    Texto = $"{item.HoraDesde} - {item.HoraHasta}"
                });
            }

            cboRangoHorario.DisplayMember = "Texto";
            cboRangoHorario.ValueMember = "Valor";

            // **Seleccionar el rango con la misma HoraDesde que la hora actual**
            for (int i = 0; i < cboRangoHorario.Items.Count; i++)
            {
                OpcionCombo oc = (OpcionCombo)cboRangoHorario.Items[i];

                // Obtener la hora de `HoraDesde` desde el texto
                string[] horas = oc.Texto.Split('-'); // Separa el rango "HH:mm - HH:mm"
                int horaDesdeCombo = Convert.ToInt32(horas[0].Trim().Split(':')[0]); // Obtiene la hora como int

                if (horaDesdeCombo == horaActual)
                {
                    cboRangoHorario.SelectedIndex = i;
                    idRangoHorarioActual = Convert.ToInt32(oc.Valor); // Guardar el Id del rango horario actual
                    break;
                }
            }
        }

        private void CargarSocios(int IdEntrenador)
        {
            lblMensaje.Visible = false;

            dgvDataSocio.Rows.Clear();

            List<Socio> socios = new ControladorGymSocio().ListarSociosActuales(IdEntrenador, idRangoHorarioActual);

            if (socios.Count > 0)
            {
                foreach (var socio in socios)
                {
                    dgvDataSocio.Rows.Add("", socio.IdSocio, socio.NombreYApellido);
                }
            } else
            {
                lblMensaje.Visible = true;
            }
        }
        private string TraducirDia(string diaIngles)
        {
            switch (diaIngles)
            {
                case "Monday": return "Lunes";
                case "Tuesday": return "Martes";
                case "Wednesday": return "Miercoles";
                case "Thursday": return "Jueves";
                case "Friday": return "Viernes";
                case "Saturday": return "Sabado";
                case "Sunday": return "Domingo";
                default: return "";
            }
        }

        private void PintarBotonDiaActual()
        {
            // Obtener el día actual en español
            string diaActual = DateTime.Now.DayOfWeek.ToString();

            // Convertir el día al formato esperado
            string diaBoton = TraducirDia(diaActual);

            if (btnLunes.Name == "btn" + diaBoton) btnLunes.BackColor = Color.SteelBlue;
            if (btnMartes.Name == "btn" + diaBoton) btnMartes.BackColor = Color.SteelBlue;
            if (btnMiercoles.Name == "btn" + diaBoton) btnMiercoles.BackColor = Color.SteelBlue;
            if (btnJueves.Name == "btn" + diaBoton) btnJueves.BackColor = Color.SteelBlue;
            if (btnViernes.Name == "btn" + diaBoton) btnViernes.BackColor = Color.SteelBlue;
            if (btnSabado.Name == "btn" + diaBoton) btnSabado.BackColor = Color.SteelBlue;
        }

        private void LimpiarBotones()
        {
            btnLunes.BackColor = ColorTranslator.FromHtml("#E6EAEA");
            btnMartes.BackColor = ColorTranslator.FromHtml("#E6EAEA");
            btnMiercoles.BackColor = ColorTranslator.FromHtml("#E6EAEA");
            btnJueves.BackColor = ColorTranslator.FromHtml("#E6EAEA");
            btnViernes.BackColor = ColorTranslator.FromHtml("#E6EAEA");
            btnSabado.BackColor = ColorTranslator.FromHtml("#E6EAEA");
        }
        #endregion

        public frmGestionarRutinas()
        {
            InitializeComponent();
        }

        private void frmGestionarRutinas_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#E6EAEA");
            lblMensaje.Visible = false;
            cargarCBO();
            LimpiarBotones();
        }

        private void cboRangoHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvDataSocio.Rows.Clear();
            LimpiarBotones();

            if (cboRangoHorario.SelectedItem != null)
            {
                OpcionCombo seleccion = (OpcionCombo)cboRangoHorario.SelectedItem;

                idRangoHorarioActual = Convert.ToInt32(seleccion.Valor);

                List<Usuario> usuarios = new ControladorGymUsuario().ObtenerPorRangoHorario(Convert.ToInt32(idRangoHorarioActual));

                dgvDataEntrenador.Rows.Clear();

                foreach (var usuario in usuarios)
                {
                    dgvDataEntrenador.Rows.Add("", usuario.IdUsuario, usuario.NombreYApellido);
                }
            }
        }

        private void dgvDataEntrenador_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Verifica que la fila no sea el encabezado (-1 indica el encabezado)
            if (e.RowIndex < 0)
                return;

            // Verifica que está en la primera columna (columna de botones)
            if (e.ColumnIndex == 0)
            {
                // Pinta la celda con sus partes predeterminadas
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                //// Verifica si la celda actual tiene el valor "true" en la columna "Seleccionado"
                bool isSelected = Convert.ToBoolean(dgvDataEntrenador.Rows[e.RowIndex].Cells["Seleccionado"].Value);

                if (isSelected) // Solo dibuja el check2 si la celda está seleccionada
                {
                    var w = Properties.Resources.check2.Width;
                    var h = Properties.Resources.check2.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(Properties.Resources.check2, new Rectangle(x, y, w, h));
                }
                e.Handled = true;
            }
        }

        private void dgvDataEntrenador_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LimpiarBotones();

            if (dgvDataEntrenador.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    // Desmarcar todas las filas primero
                    foreach (DataGridViewRow row in dgvDataEntrenador.Rows)
                    {
                        row.Cells["Seleccionado"].Value = false;
                    }

                    // Activar el check2 solo en la fila clickeada
                    dgvDataEntrenador.Rows[indice].Cells["Seleccionado"].Value = true;

                    // Refrescar la vista
                    dgvDataEntrenador.Refresh();

                    CargarSocios(Convert.ToInt32(dgvDataEntrenador.Rows[indice].Cells["Id"].Value));
                }
            }
        }

        private void dgvDataSocio_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Verifica que la fila no sea el encabezado (-1 indica el encabezado)
            if (e.RowIndex < 0)
                return;

            // Verifica que está en la primera columna (columna de botones)
            if (e.ColumnIndex == 0)
            {
                // Pinta la celda con sus partes predeterminadas
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                //// Verifica si la celda actual tiene el valor "true" en la columna "Seleccionado"
                bool isSelected = Convert.ToBoolean(dgvDataSocio.Rows[e.RowIndex].Cells["Seleccionado2"].Value);

                if (isSelected) // Solo dibuja el check2 si la celda está seleccionada
                {
                    var w = Properties.Resources.check2.Width;
                    var h = Properties.Resources.check2.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(Properties.Resources.check2, new Rectangle(x, y, w, h));
                }
                e.Handled = true;
            }
        }

        private void dgvDataSocio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDataSocio.Columns[e.ColumnIndex].Name == "btnSeleccionar2")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    // Desmarcar todas las filas primero
                    foreach (DataGridViewRow row in dgvDataSocio.Rows)
                    {
                        row.Cells["Seleccionado2"].Value = false;
                    }

                    // Activar el check2 solo en la fila clickeada
                    dgvDataSocio.Rows[indice].Cells["Seleccionado2"].Value = true;

                    // Refrescar la vista
                    dgvDataSocio.Refresh();

                    // Traer Rutinas
                    PintarBotonDiaActual();

                    int IdSocio = Convert.ToInt32(dgvDataSocio.Rows[indice].Cells["IdSocio"].Value);

                    // Acá hay un error que no encuentro!!!
                    //List<Rutina> rutinasSocio = new ControladorGymRutina().Listar(IdSocio);

                    //RutinasActuales = rutinasSocio;
                }
            }
        }

        private void btnLunes_Click(object sender, EventArgs e)
        {
            LimpiarBotones();
            btnLunes.BackColor = Color.SteelBlue;

            foreach (Rutina rutina in RutinasActuales)
            {
                if (rutina.Dia == "Lunes")
                {

                }
            }
        }

        private void btnMartes_Click(object sender, EventArgs e)
        {
            LimpiarBotones();
            btnMartes.BackColor = Color.SteelBlue;

            foreach (Rutina rutina in RutinasActuales)
            {
                if (rutina.Dia == "Martes")
                {

                }
            }
        }

        private void btnMiercoles_Click(object sender, EventArgs e)
        {
            LimpiarBotones();
            btnMiercoles.BackColor = Color.SteelBlue;

            foreach (Rutina rutina in RutinasActuales)
            {
                if (rutina.Dia == "Miercoles")
                {

                }
            }
        }

        private void btnJueves_Click(object sender, EventArgs e)
        {
            LimpiarBotones();
            btnJueves.BackColor = Color.SteelBlue;

            foreach (Rutina rutina in RutinasActuales)
            {
                if (rutina.Dia == "Jueves")
                {

                }
            }
        }

        private void btnViernes_Click(object sender, EventArgs e)
        {
            LimpiarBotones();
            btnViernes.BackColor = Color.SteelBlue;

            foreach (Rutina rutina in RutinasActuales)
            {
                if (rutina.Dia == "Viernes")
                {

                }
            }
        }

        private void btnSabado_Click(object sender, EventArgs e)
        {
            LimpiarBotones();
            btnSabado.BackColor = Color.SteelBlue;

            foreach (Rutina rutina in RutinasActuales)
            {
                if (rutina.Dia == "Sabado")
                {

                }
            }
        }
    }
}
