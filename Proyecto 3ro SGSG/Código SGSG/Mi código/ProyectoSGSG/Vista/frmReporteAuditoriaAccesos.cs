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

namespace Vista
{
    public partial class frmReporteAuditoriaAccesos : Form
    {
        public frmReporteAuditoriaAccesos()
        {
            InitializeComponent();
        }

        private void frmReporteAuditoriaAccesos_Load(object sender, EventArgs e)
        {
            List<AuditoriaAcceso> lista = new ControladorGymAuditoriaAcceso().Listar();

            foreach (AuditoriaAcceso item in lista)
            {
                dgvAuditoriaAccesos.Rows.Add(new object[]
                {
                    item.IdAuditoria,
                    item.usuario.IdUsuario,
                    item.usuario.NombreYApellido,
                    item.FechaHora,
                    item.TipoEvento
                });
            }
        }
    }
}
