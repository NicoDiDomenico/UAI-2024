using Controlador;
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
    public partial class frmValidarIngreso : Form
    {
        public frmValidarIngreso()
        {
            InitializeComponent();
        }

        private void frmValidarIngreso_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistrarTurno_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigoIngreso.Text.Trim();

            if (string.IsNullOrEmpty(codigo))
            {
                MessageBox.Show("Ingrese un código de ingreso válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idTurno, idRangoHorario;
            DateTime fechaTurno;
            string mensaje = "";
            bool turnoValido = new ControladorGymTurno().ValidarCodigoIngreso(codigo, out idTurno, out idRangoHorario, out fechaTurno, out mensaje);

            if (!turnoValido)
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Si el código es válido, actualizar el estado del turno y restar el cupo
            bool actualizado = new ControladorGymTurno().ActualizarEstadoTurno(idTurno, idRangoHorario, fechaTurno);

            if (actualizado)
            {
                MessageBox.Show("Ingreso validado correctamente. Turno finalizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigoIngreso.Clear();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hubo un problema al actualizar el estado del turno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
