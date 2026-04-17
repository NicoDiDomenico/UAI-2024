namespace Vista
{
    partial class frmValidarIngreso
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodigoIngreso = new System.Windows.Forms.TextBox();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRegistrarTurno = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label3.Location = new System.Drawing.Point(65, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Codigo de Ingreso:";
            // 
            // txtCodigoIngreso
            // 
            this.txtCodigoIngreso.Location = new System.Drawing.Point(255, 44);
            this.txtCodigoIngreso.Name = "txtCodigoIngreso";
            this.txtCodigoIngreso.Size = new System.Drawing.Size(277, 20);
            this.txtCodigoIngreso.TabIndex = 9;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.iconPictureBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.UserLock;
            this.iconPictureBox1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 141;
            this.iconPictureBox1.Location = new System.Drawing.Point(368, 53);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(155, 141);
            this.iconPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.iconPictureBox1.TabIndex = 8;
            this.iconPictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCodigoIngreso);
            this.groupBox1.Location = new System.Drawing.Point(136, 264);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(601, 100);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // btnRegistrarTurno
            // 
            this.btnRegistrarTurno.BackColor = System.Drawing.Color.AliceBlue;
            this.btnRegistrarTurno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrarTurno.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRegistrarTurno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarTurno.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarTurno.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrarTurno.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnRegistrarTurno.IconColor = System.Drawing.Color.Black;
            this.btnRegistrarTurno.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRegistrarTurno.IconSize = 30;
            this.btnRegistrarTurno.Location = new System.Drawing.Point(368, 425);
            this.btnRegistrarTurno.Name = "btnRegistrarTurno";
            this.btnRegistrarTurno.Size = new System.Drawing.Size(155, 60);
            this.btnRegistrarTurno.TabIndex = 98;
            this.btnRegistrarTurno.Text = "Confirmar";
            this.btnRegistrarTurno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistrarTurno.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistrarTurno.UseVisualStyleBackColor = false;
            this.btnRegistrarTurno.Click += new System.EventHandler(this.btnRegistrarTurno_Click);
            // 
            // frmValidarIngreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.ClientSize = new System.Drawing.Size(881, 559);
            this.Controls.Add(this.btnRegistrarTurno);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.iconPictureBox1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.Name = "frmValidarIngreso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmValidarIngreso";
            this.Load += new System.EventHandler(this.frmValidarIngreso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodigoIngreso;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private FontAwesome.Sharp.IconButton btnRegistrarTurno;
    }
}