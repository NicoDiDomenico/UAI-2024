namespace Vista
{
    partial class frmReporteAuditoriaAccesos
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
            this.dgvAuditoriaAccesos = new System.Windows.Forms.DataGridView();
            this.IdAuditoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaHora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoEvento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditoriaAccesos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAuditoriaAccesos
            // 
            this.dgvAuditoriaAccesos.BackgroundColor = System.Drawing.Color.White;
            this.dgvAuditoriaAccesos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAuditoriaAccesos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdAuditoria,
            this.IdUsuario,
            this.FechaHora,
            this.TipoEvento});
            this.dgvAuditoriaAccesos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAuditoriaAccesos.Location = new System.Drawing.Point(0, 0);
            this.dgvAuditoriaAccesos.Name = "dgvAuditoriaAccesos";
            this.dgvAuditoriaAccesos.RowHeadersVisible = false;
            this.dgvAuditoriaAccesos.Size = new System.Drawing.Size(1153, 494);
            this.dgvAuditoriaAccesos.TabIndex = 1;
            // 
            // IdAuditoria
            // 
            this.IdAuditoria.HeaderText = "IdAuditoria";
            this.IdAuditoria.Name = "IdAuditoria";
            // 
            // IdUsuario
            // 
            this.IdUsuario.HeaderText = "IdUsuario";
            this.IdUsuario.Name = "IdUsuario";
            // 
            // FechaHora
            // 
            this.FechaHora.HeaderText = "Fecha y Hora Acción";
            this.FechaHora.Name = "FechaHora";
            this.FechaHora.Width = 130;
            // 
            // TipoEvento
            // 
            this.TipoEvento.HeaderText = "Tipo Evento";
            this.TipoEvento.Name = "TipoEvento";
            // 
            // frmReporteAuditoriaAccesos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.ClientSize = new System.Drawing.Size(1153, 494);
            this.Controls.Add(this.dgvAuditoriaAccesos);
            this.Name = "frmReporteAuditoriaAccesos";
            this.Text = "frmReporteAuditoriaAccesos";
            this.Load += new System.EventHandler(this.frmReporteAuditoriaAccesos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditoriaAccesos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAuditoriaAccesos;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdAuditoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaHora;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoEvento;
    }
}