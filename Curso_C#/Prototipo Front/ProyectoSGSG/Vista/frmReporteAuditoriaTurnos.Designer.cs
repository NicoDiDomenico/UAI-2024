namespace Vista
{
    partial class frmReporteAuditoriaTurnos
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
            this.dgvAuditoriaTurnos = new System.Windows.Forms.DataGridView();
            this.IdAuditoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdTurno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Accion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaHora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatosOriginales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatosNuevos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditoriaTurnos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAuditoriaTurnos
            // 
            this.dgvAuditoriaTurnos.BackgroundColor = System.Drawing.Color.White;
            this.dgvAuditoriaTurnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAuditoriaTurnos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdAuditoria,
            this.IdTurno,
            this.IdUsuario,
            this.Accion,
            this.FechaHora,
            this.DatosOriginales,
            this.DatosNuevos});
            this.dgvAuditoriaTurnos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAuditoriaTurnos.Location = new System.Drawing.Point(0, 0);
            this.dgvAuditoriaTurnos.Name = "dgvAuditoriaTurnos";
            this.dgvAuditoriaTurnos.RowHeadersVisible = false;
            this.dgvAuditoriaTurnos.Size = new System.Drawing.Size(1153, 494);
            this.dgvAuditoriaTurnos.TabIndex = 0;
            // 
            // IdAuditoria
            // 
            this.IdAuditoria.HeaderText = "IdAuditoria";
            this.IdAuditoria.Name = "IdAuditoria";
            this.IdAuditoria.Visible = false;
            // 
            // IdTurno
            // 
            this.IdTurno.HeaderText = "IdTurno";
            this.IdTurno.Name = "IdTurno";
            this.IdTurno.Visible = false;
            // 
            // IdUsuario
            // 
            this.IdUsuario.HeaderText = "IdUsuario";
            this.IdUsuario.Name = "IdUsuario";
            this.IdUsuario.Visible = false;
            // 
            // Accion
            // 
            this.Accion.HeaderText = "Accion";
            this.Accion.Name = "Accion";
            this.Accion.Width = 80;
            // 
            // FechaHora
            // 
            this.FechaHora.HeaderText = "Fecha y Hora Acción";
            this.FechaHora.Name = "FechaHora";
            this.FechaHora.Width = 130;
            // 
            // DatosOriginales
            // 
            this.DatosOriginales.HeaderText = "Datos Originales";
            this.DatosOriginales.Name = "DatosOriginales";
            this.DatosOriginales.Width = 470;
            // 
            // DatosNuevos
            // 
            this.DatosNuevos.HeaderText = "Datos Nuevos";
            this.DatosNuevos.Name = "DatosNuevos";
            this.DatosNuevos.Width = 470;
            // 
            // frmReporteAuditoriaTurnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.ClientSize = new System.Drawing.Size(1153, 494);
            this.Controls.Add(this.dgvAuditoriaTurnos);
            this.Name = "frmReporteAuditoriaTurnos";
            this.Text = "frmReporteAuditoriaTurnos";
            this.Load += new System.EventHandler(this.frmReporteAuditoriaTurnos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditoriaTurnos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAuditoriaTurnos;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdAuditoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTurno;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Accion;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaHora;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatosOriginales;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatosNuevos;
    }
}