using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmReporteAuditoria : Form
    {
        public frmReporteAuditoria()
        {
            InitializeComponent();
        }

        private void frmReporteAuditoria_Load(object sender, EventArgs e)
        {
            // Cargar Formulario A en la primera pestaña
            frmReporteAuditoriaTurnos formA = new frmReporteAuditoriaTurnos();
            formA.TopLevel = false;
            formA.FormBorderStyle = FormBorderStyle.None;
            formA.Dock = DockStyle.Fill;
            tpTurnos.Controls.Add(formA);
            formA.Show();

            // Cargar Formulario B en la segunda pestaña
            frmReporteAuditoriaAccesos formB = new frmReporteAuditoriaAccesos();
            formB.TopLevel = false;
            formB.FormBorderStyle = FormBorderStyle.None;
            formB.Dock = DockStyle.Fill;
            tpAccesos.Controls.Add(formB);
            formB.Show();
        }
    }
}
