namespace Vista
{
    partial class frmReporteAuditoria
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
            this.tcTurnos = new System.Windows.Forms.TabControl();
            this.tpTurnos = new System.Windows.Forms.TabPage();
            this.tpAccesos = new System.Windows.Forms.TabPage();
            this.tcTurnos.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcTurnos
            // 
            this.tcTurnos.Controls.Add(this.tpTurnos);
            this.tcTurnos.Controls.Add(this.tpAccesos);
            this.tcTurnos.Location = new System.Drawing.Point(8, 12);
            this.tcTurnos.Name = "tcTurnos";
            this.tcTurnos.SelectedIndex = 0;
            this.tcTurnos.Size = new System.Drawing.Size(1177, 559);
            this.tcTurnos.TabIndex = 0;
            // 
            // tpTurnos
            // 
            this.tpTurnos.Location = new System.Drawing.Point(4, 22);
            this.tpTurnos.Name = "tpTurnos";
            this.tpTurnos.Padding = new System.Windows.Forms.Padding(3);
            this.tpTurnos.Size = new System.Drawing.Size(1169, 533);
            this.tpTurnos.TabIndex = 0;
            this.tpTurnos.Text = "Turnos";
            this.tpTurnos.UseVisualStyleBackColor = true;
            // 
            // tpAccesos
            // 
            this.tpAccesos.Location = new System.Drawing.Point(4, 22);
            this.tpAccesos.Name = "tpAccesos";
            this.tpAccesos.Padding = new System.Windows.Forms.Padding(3);
            this.tpAccesos.Size = new System.Drawing.Size(1012, 533);
            this.tpAccesos.TabIndex = 1;
            this.tpAccesos.Text = "Accesos";
            this.tpAccesos.UseVisualStyleBackColor = true;
            // 
            // frmReporteAuditoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.ClientSize = new System.Drawing.Size(1191, 583);
            this.Controls.Add(this.tcTurnos);
            this.Name = "frmReporteAuditoria";
            this.Text = "frmReporteAuditoria";
            this.Load += new System.EventHandler(this.frmReporteAuditoria_Load);
            this.tcTurnos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcTurnos;
        private System.Windows.Forms.TabPage tpTurnos;
        private System.Windows.Forms.TabPage tpAccesos;
    }
}