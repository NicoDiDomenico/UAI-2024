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
            this.menuRoles = new FontAwesome.Sharp.IconMenuItem();
            this.agregarRolToolStripMenuItem = new FontAwesome.Sharp.IconMenuItem();
            this.editarRolToolStripMenuItem = new FontAwesome.Sharp.IconMenuItem();
            this.editarAccionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCalentamiento = new FontAwesome.Sharp.IconMenuItem();
            this.menuElementosGym = new FontAwesome.Sharp.IconMenuItem();
            this.equipamientoToolStripMenuItem = new FontAwesome.Sharp.IconMenuItem();
            this.ejercicioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maquinaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEstiramiento = new FontAwesome.Sharp.IconMenuItem();
            this.menuRangosHorarios = new FontAwesome.Sharp.IconMenuItem();
            this.menuNegocio = new FontAwesome.Sharp.IconMenuItem();
            this.menuAcercaDe = new FontAwesome.Sharp.IconMenuItem();
            this.subContenedor = new System.Windows.Forms.Panel();
            this.asdsadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asdasdasdasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // subBotones
            // 
            this.subBotones.AutoSize = false;
            this.subBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.subBotones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUsuarios,
            this.menuRoles,
            this.menuCalentamiento,
            this.menuElementosGym,
            this.menuEstiramiento,
            this.menuRangosHorarios,
            this.menuNegocio,
            this.menuAcercaDe});
            this.subBotones.Location = new System.Drawing.Point(0, 0);
            this.subBotones.Name = "subBotones";
            this.subBotones.Padding = new System.Windows.Forms.Padding(60, 2, 0, 2);
            this.subBotones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.subBotones.Size = new System.Drawing.Size(1788, 119);
            this.subBotones.TabIndex = 0;
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
            this.menuUsuarios.Size = new System.Drawing.Size(210, 100);
            this.menuUsuarios.Text = "Usuarios";
            this.menuUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuUsuarios.Click += new System.EventHandler(this.menuUsuarios_Click);
            // 
            // menuRoles
            // 
            this.menuRoles.AutoSize = false;
            this.menuRoles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarRolToolStripMenuItem,
            this.editarRolToolStripMenuItem,
            this.editarAccionToolStripMenuItem});
            this.menuRoles.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuRoles.IconChar = FontAwesome.Sharp.IconChar.UserShield;
            this.menuRoles.IconColor = System.Drawing.Color.Black;
            this.menuRoles.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuRoles.IconSize = 55;
            this.menuRoles.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuRoles.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.menuRoles.Name = "menuRoles";
            this.menuRoles.Size = new System.Drawing.Size(210, 100);
            this.menuRoles.Text = "Roles y Permisos";
            this.menuRoles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // agregarRolToolStripMenuItem
            // 
            this.agregarRolToolStripMenuItem.IconChar = FontAwesome.Sharp.IconChar.None;
            this.agregarRolToolStripMenuItem.IconColor = System.Drawing.Color.Black;
            this.agregarRolToolStripMenuItem.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.agregarRolToolStripMenuItem.Name = "agregarRolToolStripMenuItem";
            this.agregarRolToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            this.agregarRolToolStripMenuItem.Text = "Agregar Rol";
            this.agregarRolToolStripMenuItem.Click += new System.EventHandler(this.agregarRolToolStripMenuItem_Click);
            // 
            // editarRolToolStripMenuItem
            // 
            this.editarRolToolStripMenuItem.IconChar = FontAwesome.Sharp.IconChar.None;
            this.editarRolToolStripMenuItem.IconColor = System.Drawing.Color.Black;
            this.editarRolToolStripMenuItem.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.editarRolToolStripMenuItem.Name = "editarRolToolStripMenuItem";
            this.editarRolToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            this.editarRolToolStripMenuItem.Text = "Editar Rol";
            this.editarRolToolStripMenuItem.Click += new System.EventHandler(this.editarRolToolStripMenuItem_Click);
            // 
            // editarAccionToolStripMenuItem
            // 
            this.editarAccionToolStripMenuItem.Name = "editarAccionToolStripMenuItem";
            this.editarAccionToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            this.editarAccionToolStripMenuItem.Text = "Editar Acción";
            this.editarAccionToolStripMenuItem.Click += new System.EventHandler(this.editarAccionToolStripMenuItem_Click);
            // 
            // menuCalentamiento
            // 
            this.menuCalentamiento.AutoSize = false;
            this.menuCalentamiento.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuCalentamiento.IconChar = FontAwesome.Sharp.IconChar.Running;
            this.menuCalentamiento.IconColor = System.Drawing.Color.Black;
            this.menuCalentamiento.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuCalentamiento.IconSize = 55;
            this.menuCalentamiento.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuCalentamiento.Name = "menuCalentamiento";
            this.menuCalentamiento.Size = new System.Drawing.Size(210, 100);
            this.menuCalentamiento.Text = "Calentamiento";
            this.menuCalentamiento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuCalentamiento.Click += new System.EventHandler(this.menuCalentamiento_Click);
            // 
            // menuElementosGym
            // 
            this.menuElementosGym.AutoSize = false;
            this.menuElementosGym.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.equipamientoToolStripMenuItem,
            this.ejercicioToolStripMenuItem,
            this.maquinaToolStripMenuItem});
            this.menuElementosGym.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuElementosGym.IconChar = FontAwesome.Sharp.IconChar.Dumbbell;
            this.menuElementosGym.IconColor = System.Drawing.Color.Black;
            this.menuElementosGym.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuElementosGym.IconSize = 55;
            this.menuElementosGym.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuElementosGym.Name = "menuElementosGym";
            this.menuElementosGym.Size = new System.Drawing.Size(210, 100);
            this.menuElementosGym.Text = "Elementos Gym";
            this.menuElementosGym.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // equipamientoToolStripMenuItem
            // 
            this.equipamientoToolStripMenuItem.IconChar = FontAwesome.Sharp.IconChar.None;
            this.equipamientoToolStripMenuItem.IconColor = System.Drawing.Color.Black;
            this.equipamientoToolStripMenuItem.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.equipamientoToolStripMenuItem.Name = "equipamientoToolStripMenuItem";
            this.equipamientoToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.equipamientoToolStripMenuItem.Text = "Equipamiento";
            this.equipamientoToolStripMenuItem.Click += new System.EventHandler(this.menuEquipamiento_Click);
            // 
            // ejercicioToolStripMenuItem
            // 
            this.ejercicioToolStripMenuItem.Name = "ejercicioToolStripMenuItem";
            this.ejercicioToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.ejercicioToolStripMenuItem.Text = "Ejercicio";
            this.ejercicioToolStripMenuItem.Click += new System.EventHandler(this.menuEjercicios_Click);
            // 
            // maquinaToolStripMenuItem
            // 
            this.maquinaToolStripMenuItem.Name = "maquinaToolStripMenuItem";
            this.maquinaToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.maquinaToolStripMenuItem.Text = "Máquina";
            this.maquinaToolStripMenuItem.Click += new System.EventHandler(this.menuMaquinas_Click);
            // 
            // menuEstiramiento
            // 
            this.menuEstiramiento.AutoSize = false;
            this.menuEstiramiento.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuEstiramiento.IconChar = FontAwesome.Sharp.IconChar.HandSpock;
            this.menuEstiramiento.IconColor = System.Drawing.Color.Black;
            this.menuEstiramiento.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuEstiramiento.IconSize = 55;
            this.menuEstiramiento.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuEstiramiento.Name = "menuEstiramiento";
            this.menuEstiramiento.Size = new System.Drawing.Size(210, 100);
            this.menuEstiramiento.Text = "Estiramiento";
            this.menuEstiramiento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuEstiramiento.Click += new System.EventHandler(this.menuEstiramiento_Click);
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
            this.menuRangosHorarios.Size = new System.Drawing.Size(210, 100);
            this.menuRangosHorarios.Text = "Rangos Horarios";
            this.menuRangosHorarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuRangosHorarios.Click += new System.EventHandler(this.menuRangosHorarios_Click);
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
            this.menuNegocio.Size = new System.Drawing.Size(210, 100);
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
            this.menuAcercaDe.Size = new System.Drawing.Size(210, 100);
            this.menuAcercaDe.Text = "Acerca de";
            this.menuAcercaDe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuAcercaDe.Click += new System.EventHandler(this.menuAcercaDe_Click);
            // 
            // subContenedor
            // 
            this.subContenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.subContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subContenedor.Location = new System.Drawing.Point(0, 119);
            this.subContenedor.Name = "subContenedor";
            this.subContenedor.Size = new System.Drawing.Size(1788, 621);
            this.subContenedor.TabIndex = 11;
            // 
            // asdsadsToolStripMenuItem
            // 
            this.asdsadsToolStripMenuItem.Name = "asdsadsToolStripMenuItem";
            this.asdsadsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.asdsadsToolStripMenuItem.Text = "asdsads";
            // 
            // asdasdasdasToolStripMenuItem
            // 
            this.asdasdasdasToolStripMenuItem.Name = "asdasdasdasToolStripMenuItem";
            this.asdasdasdasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.asdasdasdasToolStripMenuItem.Text = "asdasdasdas";
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
        private FontAwesome.Sharp.IconMenuItem menuCalentamiento;
        private FontAwesome.Sharp.IconMenuItem menuElementosGym;
        private FontAwesome.Sharp.IconMenuItem menuEstiramiento;

        private FontAwesome.Sharp.IconMenuItem menuRangosHorarios;
        private System.Windows.Forms.Panel subContenedor;
        private FontAwesome.Sharp.IconMenuItem menuRoles;
        private System.Windows.Forms.ToolStripMenuItem asdsadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asdasdasdasToolStripMenuItem;
        private FontAwesome.Sharp.IconMenuItem agregarRolToolStripMenuItem;
        private FontAwesome.Sharp.IconMenuItem editarRolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarAccionToolStripMenuItem;
        private FontAwesome.Sharp.IconMenuItem equipamientoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ejercicioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maquinaToolStripMenuItem;
    }
}