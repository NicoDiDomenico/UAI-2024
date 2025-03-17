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
        #region "Métodos"
        private int idRangoHorarioActual; // Variable global para almacenar el Id del rango horario actual

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

            // Si se encontró un IdRangoHorario válido, podemos traer el usuario asociado
            if (idRangoHorarioActual != -1)
            {
                traerUsuarioDeRango(idRangoHorarioActual);
            }
        }

        // Método para traer al usuario que pertenece al rango horario actual
        private void traerUsuarioDeRango(int idRangoHorario)
        {
            // Falta implementar
            /*
            List<Usuario> usuarios = new ControladorGymUsuario().ObtenerPorRangoHorario(idRangoHorario);

            if (usuarios != null)
            {
                MessageBox.Show($"Usuario en el rango horario {idRangoHorario}: {usuario.NombreYApellido}",
                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarGridEntrenador(usuarios);
            }
            else
            {
                MessageBox.Show("No se encontró un usuario para este rango horario.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            */
        }

        private void cargarGridEntrenador(List<Usuario> listaUsuario)
        {
            if (listaUsuario == null || listaUsuario.Count == 0)
            {
                MessageBox.Show("No se encontraron usuarios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (Usuario item in listaUsuario)
            {
                // Validación para evitar un NullReferenceException
                if (item.Rol != null && item.Rol.IdRol == 3)
                {
                    dgvDataEntrenador.Rows.Add(new object[] {
                        "",
                        item.IdUsuario,
                        item.NombreYApellido,
                        item.Email,
                        item.Telefono,
                        item.Direccion,
                        item.Ciudad,
                        item.NroDocumento,
                        item.FechaNacimiento,
                        item.NombreUsuario,
                        item.Clave,
                        item.Genero == "Masculino" ? "Masculino" : "Femenino",
                        item.Rol.IdRol,
                        item.Rol.Descripcion,
                        item.Estado ? 1 : 0,
                        item.Estado ? "Activo" : "No Activo",
                        item.FechaRegistro
                    });
                }
            }
        }
        #endregion

        public frmGestionarRutinas()
        {
            InitializeComponent();
        }

        private void frmGestionarRutinas_Load(object sender, EventArgs e)
        {
            cargarCBO();
        }
    }
}
