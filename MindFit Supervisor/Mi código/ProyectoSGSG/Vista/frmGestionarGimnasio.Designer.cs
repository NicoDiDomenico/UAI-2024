namespace Vista
{
    partial class frmGestionarGimnasio
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
            this.subBotones = new System.Windows.Forms.MenuStrip();
            this.menuUsuarios = new FontAwesome.Sharp.IconMenuItem();
            this.menuMaquinas = new FontAwesome.Sharp.IconMenuItem();
            this.menuEjercicios = new FontAwesome.Sharp.IconMenuItem();
            this.menuEquipamiento = new FontAwesome.Sharp.IconMenuItem();
            this.menuRangosHorarios = new FontAwesome.Sharp.IconMenuItem();
            this.menuHistorialTurnos = new FontAwesome.Sharp.IconMenuItem();
            this.menuNegocio = new FontAwesome.Sharp.IconMenuItem();
            this.menuAcercaDe = new FontAwesome.Sharp.IconMenuItem();
            this.subContenedor = new System.Windows.Forms.Panel();
            this.subBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // subBotones
            // 
            this.subBotones.AutoSize = false;
            this.subBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.subBotones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUsuarios,
            this.menuMaquinas,
            this.menuEjercicios,
            this.menuEquipamiento,
            this.menuRangosHorarios,
            this.menuHistorialTurnos,
            this.menuNegocio,
            this.menuAcercaDe});
            this.subBotones.Location = new System.Drawing.Point(0, 0);
            this.subBotones.Name = "subBotones";
            this.subBotones.Padding = new System.Windows.Forms.Padding(12, 2, 0, 2);
            this.subBotones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.subBotones.Size = new System.Drawing.Size(1788, 95);
            this.subBotones.TabIndex = 1;
            this.subBotones.Text = "menuStrip1";
            // 
            // menuUsuarios
            // 
            this.menuUsuarios.AutoSize = false;
            this.menuUsuarios.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuUsuarios.IconChar = FontAwesome.Sharp.IconChar.UsersGear;
            this.menuUsuarios.IconColor = System.Drawing.Color.Black;
            this.menuUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuUsuarios.IconSize = 55;
            this.menuUsuarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuUsuarios.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.Size = new System.Drawing.Size(222, 100);
            this.menuUsuarios.Text = "Usuarios";
            this.menuUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuUsuarios.Click += new System.EventHandler(this.menuUsuarios_Click);
            // 
            // menuMaquinas
            // 
            this.menuMaquinas.AutoSize = false;
            this.menuMaquinas.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuMaquinas.IconChar = FontAwesome.Sharp.IconChar.ScrewdriverWrench;
            this.menuMaquinas.IconColor = System.Drawing.Color.Black;
            this.menuMaquinas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuMaquinas.IconSize = 55;
            this.menuMaquinas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuMaquinas.Name = "menuMaquinas";
            this.menuMaquinas.Size = new System.Drawing.Size(221, 100);
            this.menuMaquinas.Text = "Maquinas";
            this.menuMaquinas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuMaquinas.Click += new System.EventHandler(this.menuMaquinas_Click);
            // 
            // menuEjercicios
            // 
            this.menuEjercicios.AutoSize = false;
            this.menuEjercicios.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuEjercicios.IconChar = FontAwesome.Sharp.IconChar.Running;
            this.menuEjercicios.IconColor = System.Drawing.Color.Black;
            this.menuEjercicios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuEjercicios.IconSize = 55;
            this.menuEjercicios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuEjercicios.Name = "menuEjercicios";
            this.menuEjercicios.Size = new System.Drawing.Size(221, 100);
            this.menuEjercicios.Text = "Ejercicios";
            this.menuEjercicios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuEjercicios.Click += new System.EventHandler(this.menuEjercicios_Click);
            // 
            // menuEquipamiento
            // 
            this.menuEquipamiento.AutoSize = false;
            this.menuEquipamiento.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuEquipamiento.IconChar = FontAwesome.Sharp.IconChar.Dumbbell;
            this.menuEquipamiento.IconColor = System.Drawing.Color.Black;
            this.menuEquipamiento.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuEquipamiento.IconSize = 55;
            this.menuEquipamiento.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuEquipamiento.Name = "menuEquipamiento";
            this.menuEquipamiento.Size = new System.Drawing.Size(221, 100);
            this.menuEquipamiento.Text = "Equipamientos";
            this.menuEquipamiento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuEquipamiento.Click += new System.EventHandler(this.menuEquipamiento_Click);
            // 
            // menuRangosHorarios
            // 
            this.menuRangosHorarios.AutoSize = false;
            this.menuRangosHorarios.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuRangosHorarios.IconChar = FontAwesome.Sharp.IconChar.ClockFour;
            this.menuRangosHorarios.IconColor = System.Drawing.Color.Black;
            this.menuRangosHorarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuRangosHorarios.IconSize = 55;
            this.menuRangosHorarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuRangosHorarios.Name = "menuRangosHorarios";
            this.menuRangosHorarios.Size = new System.Drawing.Size(221, 100);
            this.menuRangosHorarios.Text = "Rangos Horarios";
            this.menuRangosHorarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuRangosHorarios.Click += new System.EventHandler(this.menuRangosHorarios_Click);
            // 
            // menuHistorialTurnos
            // 
            this.menuHistorialTurnos.AutoSize = false;
            this.menuHistorialTurnos.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuHistorialTurnos.IconChar = FontAwesome.Sharp.IconChar.History;
            this.menuHistorialTurnos.IconColor = System.Drawing.Color.Black;
            this.menuHistorialTurnos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuHistorialTurnos.IconSize = 55;
            this.menuHistorialTurnos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuHistorialTurnos.Name = "menuHistorialTurnos";
            this.menuHistorialTurnos.Size = new System.Drawing.Size(222, 100);
            this.menuHistorialTurnos.Text = "Historial Turnos";
            this.menuHistorialTurnos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuHistorialTurnos.Click += new System.EventHandler(this.menuHistorialTurnos_Click);
            // 
            // menuNegocio
            // 
            this.menuNegocio.AutoSize = false;
            this.menuNegocio.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuNegocio.IconChar = FontAwesome.Sharp.IconChar.Briefcase;
            this.menuNegocio.IconColor = System.Drawing.Color.Black;
            this.menuNegocio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuNegocio.IconSize = 55;
            this.menuNegocio.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuNegocio.Name = "menuNegocio";
            this.menuNegocio.Size = new System.Drawing.Size(222, 100);
            this.menuNegocio.Text = "Negocio";
            this.menuNegocio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuNegocio.Click += new System.EventHandler(this.menuNegocio_Click);
            // 
            // menuAcercaDe
            // 
            this.menuAcercaDe.AutoSize = false;
            this.menuAcercaDe.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuAcercaDe.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            this.menuAcercaDe.IconColor = System.Drawing.Color.Black;
            this.menuAcercaDe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuAcercaDe.IconSize = 55;
            this.menuAcercaDe.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuAcercaDe.Name = "menuAcercaDe";
            this.menuAcercaDe.Size = new System.Drawing.Size(222, 100);
            this.menuAcercaDe.Text = "Acerca de";
            this.menuAcercaDe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuAcercaDe.Click += new System.EventHandler(this.menuAcercaDe_Click);
            // 
            // subContenedor
            // 
            this.subContenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.subContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subContenedor.Location = new System.Drawing.Point(0, 95);
            this.subContenedor.Name = "subContenedor";
            this.subContenedor.Size = new System.Drawing.Size(1788, 645);
            this.subContenedor.TabIndex = 11;
            // 
            // frmGestionarGimnasio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1788, 740);
            this.Controls.Add(this.subContenedor);
            this.Controls.Add(this.subBotones);
            this.Name = "frmGestionarGimnasio";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "frmGestionarGimnasio";
            this.Load += new System.EventHandler(this.frmGestionarGimnasio_Load);
            this.subBotones.ResumeLayout(false);
            this.subBotones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip subBotones;
        private FontAwesome.Sharp.IconMenuItem menuUsuarios;
        private FontAwesome.Sharp.IconMenuItem menuAcercaDe;
        private FontAwesome.Sharp.IconMenuItem menuNegocio;
        private FontAwesome.Sharp.IconMenuItem menuMaquinas;
        private FontAwesome.Sharp.IconMenuItem menuEjercicios;
        private FontAwesome.Sharp.IconMenuItem menuEquipamiento;

        private FontAwesome.Sharp.IconMenuItem menuRangosHorarios;
        private FontAwesome.Sharp.IconMenuItem menuHistorialTurnos;
        private System.Windows.Forms.Panel subContenedor;
    }
}