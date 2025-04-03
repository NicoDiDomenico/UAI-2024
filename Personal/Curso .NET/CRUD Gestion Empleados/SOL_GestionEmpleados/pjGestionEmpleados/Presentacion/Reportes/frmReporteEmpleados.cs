using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjGestionEmpleados.Presentacion.Reportes
{
    public partial class frmReporteEmpleados : Form
    {
        public frmReporteEmpleados()
        {
            InitializeComponent();
        }

        private void frmReporteEmpleados_Load(object sender, EventArgs e)
        {
            this.sP_LISTAR_EMPLEADOSTableAdapter.Fill(this.dataSet.SP_LISTAR_EMPLEADOS, cBusqueda:txtFiltrar.Text);
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
