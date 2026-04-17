using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Controlador;
using Modelo;

namespace Vista
{
    public partial class frmReporteAuditoriaTurnos : Form
    {
        public frmReporteAuditoriaTurnos()
        {
            InitializeComponent();
        }

        private void frmReporteAuditoriaTurnos_Load(object sender, EventArgs e)
        {
            List<AuditoriaTurno> lista = new ControladorGymAuditoriaTurno().Listar();

            foreach (AuditoriaTurno item in lista)
            {
                dgvAuditoriaTurnos.Rows.Add(new object[]
                {
                    item.IdAuditoria,
                    item.IdTurno,
                    item.IdUsuario,
                    string.IsNullOrEmpty(item.Accion) ? "-" : item.Accion,
                    item.FechaHora,
                    string.IsNullOrEmpty(item.DatosOriginales) ? "-" : item.DatosOriginales,
                    string.IsNullOrEmpty(item.DatosNuevos) ? "-" : item.DatosNuevos
                });
            }
        }
    }
}
