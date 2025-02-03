namespace pjGestionEmpleados.Presentacion.Reportes
{
    partial class frmReporteEmpleados
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dataSet = new pjGestionEmpleados.Presentacion.Reportes.DataSet();
            this.sPLISTAREMPLEADOSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sP_LISTAR_EMPLEADOSTableAdapter = new pjGestionEmpleados.Presentacion.Reportes.DataSetTableAdapters.SP_LISTAR_EMPLEADOSTableAdapter();
            this.txtFiltrar = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sPLISTAREMPLEADOSBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.sPLISTAREMPLEADOSBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "pjGestionEmpleados.Presentacion.Reportes.ReporteEmpleados.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sPLISTAREMPLEADOSBindingSource
            // 
            this.sPLISTAREMPLEADOSBindingSource.DataMember = "SP_LISTAR_EMPLEADOS";
            this.sPLISTAREMPLEADOSBindingSource.DataSource = this.dataSet;
            // 
            // sP_LISTAR_EMPLEADOSTableAdapter
            // 
            this.sP_LISTAR_EMPLEADOSTableAdapter.ClearBeforeFill = true;
            // 
            // txtFiltrar
            // 
            this.txtFiltrar.Location = new System.Drawing.Point(161, 159);
            this.txtFiltrar.Name = "txtFiltrar";
            this.txtFiltrar.Size = new System.Drawing.Size(100, 20);
            this.txtFiltrar.TabIndex = 1;
            this.txtFiltrar.Visible = false;
            this.txtFiltrar.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // frmReporteEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtFiltrar);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmReporteEmpleados";
            this.Text = "Reporte Empleados";
            this.Load += new System.EventHandler(this.frmReporteEmpleados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sPLISTAREMPLEADOSBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource sPLISTAREMPLEADOSBindingSource;
        private DataSet dataSet;
        private DataSetTableAdapters.SP_LISTAR_EMPLEADOSTableAdapter sP_LISTAR_EMPLEADOSTableAdapter;
        internal System.Windows.Forms.TextBox txtFiltrar;
    }
}