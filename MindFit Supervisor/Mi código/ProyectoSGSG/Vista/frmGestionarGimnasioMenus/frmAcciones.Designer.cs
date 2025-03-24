namespace Vista
{
    partial class frmAcciones
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
            this.btnGuardar = new FontAwesome.Sharp.IconButton();
            this.btnMenuAgregar = new System.Windows.Forms.CheckBox();
            this.gbGestionSocios = new System.Windows.Forms.GroupBox();
            this.btnMenuConsultar = new System.Windows.Forms.CheckBox();
            this.btnMenuEliminar = new System.Windows.Forms.CheckBox();
            this.btnMenuTurno = new System.Windows.Forms.CheckBox();
            this.gbGestionRutinas = new System.Windows.Forms.GroupBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.gbGestionGimnasion = new System.Windows.Forms.GroupBox();
            this.menuRangosHorarios = new System.Windows.Forms.CheckBox();
            this.menuEquipamiento = new System.Windows.Forms.CheckBox();
            this.menuEjercicios = new System.Windows.Forms.CheckBox();
            this.menuNegocio = new System.Windows.Forms.CheckBox();
            this.menuRoles = new System.Windows.Forms.CheckBox();
            this.menuMaquinas = new System.Windows.Forms.CheckBox();
            this.menuUsuarios = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbGestionSocios.SuspendLayout();
            this.gbGestionRutinas.SuspendLayout();
            this.gbGestionGimnasion.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnGuardar.IconColor = System.Drawing.Color.White;
            this.btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGuardar.IconSize = 24;
            this.btnGuardar.Location = new System.Drawing.Point(602, 660);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(121, 50);
            this.btnGuardar.TabIndex = 45;
            this.btnGuardar.Text = "Aceptar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnMenuAgregar
            // 
            this.btnMenuAgregar.AutoSize = true;
            this.btnMenuAgregar.Location = new System.Drawing.Point(29, 40);
            this.btnMenuAgregar.Name = "btnMenuAgregar";
            this.btnMenuAgregar.Size = new System.Drawing.Size(156, 28);
            this.btnMenuAgregar.TabIndex = 47;
            this.btnMenuAgregar.Text = "Registrar Socio";
            this.btnMenuAgregar.UseVisualStyleBackColor = true;
            // 
            // gbGestionSocios
            // 
            this.gbGestionSocios.Controls.Add(this.btnMenuConsultar);
            this.gbGestionSocios.Controls.Add(this.btnMenuEliminar);
            this.gbGestionSocios.Controls.Add(this.btnMenuTurno);
            this.gbGestionSocios.Controls.Add(this.btnMenuAgregar);
            this.gbGestionSocios.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGestionSocios.Location = new System.Drawing.Point(39, 73);
            this.gbGestionSocios.Name = "gbGestionSocios";
            this.gbGestionSocios.Size = new System.Drawing.Size(521, 246);
            this.gbGestionSocios.TabIndex = 48;
            this.gbGestionSocios.TabStop = false;
            this.gbGestionSocios.Text = "Gestión Socios";
            // 
            // btnMenuConsultar
            // 
            this.btnMenuConsultar.AutoSize = true;
            this.btnMenuConsultar.Location = new System.Drawing.Point(288, 40);
            this.btnMenuConsultar.Name = "btnMenuConsultar";
            this.btnMenuConsultar.Size = new System.Drawing.Size(130, 28);
            this.btnMenuConsultar.TabIndex = 50;
            this.btnMenuConsultar.Text = "Editar Socio";
            this.btnMenuConsultar.UseVisualStyleBackColor = true;
            // 
            // btnMenuEliminar
            // 
            this.btnMenuEliminar.AutoSize = true;
            this.btnMenuEliminar.Location = new System.Drawing.Point(29, 161);
            this.btnMenuEliminar.Name = "btnMenuEliminar";
            this.btnMenuEliminar.Size = new System.Drawing.Size(150, 28);
            this.btnMenuEliminar.TabIndex = 49;
            this.btnMenuEliminar.Text = "Eliminar Socio";
            this.btnMenuEliminar.UseVisualStyleBackColor = true;
            // 
            // btnMenuTurno
            // 
            this.btnMenuTurno.AutoSize = true;
            this.btnMenuTurno.Location = new System.Drawing.Point(288, 161);
            this.btnMenuTurno.Name = "btnMenuTurno";
            this.btnMenuTurno.Size = new System.Drawing.Size(174, 28);
            this.btnMenuTurno.TabIndex = 48;
            this.btnMenuTurno.Text = "Gestionar Turnos";
            this.btnMenuTurno.UseVisualStyleBackColor = true;
            // 
            // gbGestionRutinas
            // 
            this.gbGestionRutinas.Controls.Add(this.checkBox5);
            this.gbGestionRutinas.Controls.Add(this.checkBox6);
            this.gbGestionRutinas.Controls.Add(this.checkBox7);
            this.gbGestionRutinas.Controls.Add(this.checkBox8);
            this.gbGestionRutinas.Enabled = false;
            this.gbGestionRutinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGestionRutinas.Location = new System.Drawing.Point(39, 337);
            this.gbGestionRutinas.Name = "gbGestionRutinas";
            this.gbGestionRutinas.Size = new System.Drawing.Size(521, 246);
            this.gbGestionRutinas.TabIndex = 51;
            this.gbGestionRutinas.TabStop = false;
            this.gbGestionRutinas.Text = "Gestión Rutinas";
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(288, 55);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(123, 28);
            this.checkBox5.TabIndex = 50;
            this.checkBox5.Text = "checkBox5";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.Visible = false;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(29, 178);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(123, 28);
            this.checkBox6.TabIndex = 49;
            this.checkBox6.Text = "checkBox6";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.Visible = false;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(288, 178);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(123, 28);
            this.checkBox7.TabIndex = 48;
            this.checkBox7.Text = "checkBox7";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.Visible = false;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(29, 55);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(123, 28);
            this.checkBox8.TabIndex = 47;
            this.checkBox8.Text = "checkBox8";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.Visible = false;
            // 
            // gbGestionGimnasion
            // 
            this.gbGestionGimnasion.Controls.Add(this.menuRangosHorarios);
            this.gbGestionGimnasion.Controls.Add(this.menuEquipamiento);
            this.gbGestionGimnasion.Controls.Add(this.menuEjercicios);
            this.gbGestionGimnasion.Controls.Add(this.menuNegocio);
            this.gbGestionGimnasion.Controls.Add(this.menuRoles);
            this.gbGestionGimnasion.Controls.Add(this.menuMaquinas);
            this.gbGestionGimnasion.Controls.Add(this.menuUsuarios);
            this.gbGestionGimnasion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGestionGimnasion.Location = new System.Drawing.Point(615, 73);
            this.gbGestionGimnasion.Name = "gbGestionGimnasion";
            this.gbGestionGimnasion.Size = new System.Drawing.Size(539, 510);
            this.gbGestionGimnasion.TabIndex = 52;
            this.gbGestionGimnasion.TabStop = false;
            this.gbGestionGimnasion.Text = "Gestión Gimnasio";
            // 
            // menuRangosHorarios
            // 
            this.menuRangosHorarios.AutoSize = true;
            this.menuRangosHorarios.Location = new System.Drawing.Point(29, 375);
            this.menuRangosHorarios.Name = "menuRangosHorarios";
            this.menuRangosHorarios.Size = new System.Drawing.Size(302, 28);
            this.menuRangosHorarios.TabIndex = 53;
            this.menuRangosHorarios.Text = "Asignar Horarios y Entrenadores";
            this.menuRangosHorarios.UseVisualStyleBackColor = true;
            // 
            // menuEquipamiento
            // 
            this.menuEquipamiento.AutoSize = true;
            this.menuEquipamiento.Location = new System.Drawing.Point(29, 308);
            this.menuEquipamiento.Name = "menuEquipamiento";
            this.menuEquipamiento.Size = new System.Drawing.Size(231, 28);
            this.menuEquipamiento.TabIndex = 52;
            this.menuEquipamiento.Text = "Gestionar Equipamiento";
            this.menuEquipamiento.UseVisualStyleBackColor = true;
            // 
            // menuEjercicios
            // 
            this.menuEjercicios.AutoSize = true;
            this.menuEjercicios.Location = new System.Drawing.Point(29, 241);
            this.menuEjercicios.Name = "menuEjercicios";
            this.menuEjercicios.Size = new System.Drawing.Size(196, 28);
            this.menuEjercicios.TabIndex = 51;
            this.menuEjercicios.Text = "Gestionar Ejercicios";
            this.menuEjercicios.UseVisualStyleBackColor = true;
            // 
            // menuNegocio
            // 
            this.menuNegocio.AutoSize = true;
            this.menuNegocio.Location = new System.Drawing.Point(29, 442);
            this.menuNegocio.Name = "menuNegocio";
            this.menuNegocio.Size = new System.Drawing.Size(244, 28);
            this.menuNegocio.TabIndex = 50;
            this.menuNegocio.Text = "Editar del Datos Gimnasio";
            this.menuNegocio.UseVisualStyleBackColor = true;
            // 
            // menuRoles
            // 
            this.menuRoles.AutoSize = true;
            this.menuRoles.Location = new System.Drawing.Point(29, 107);
            this.menuRoles.Name = "menuRoles";
            this.menuRoles.Size = new System.Drawing.Size(315, 28);
            this.menuRoles.TabIndex = 49;
            this.menuRoles.Text = "Modificar Rol y consultar permisos";
            this.menuRoles.UseVisualStyleBackColor = true;
            // 
            // menuMaquinas
            // 
            this.menuMaquinas.AutoSize = true;
            this.menuMaquinas.Location = new System.Drawing.Point(29, 174);
            this.menuMaquinas.Name = "menuMaquinas";
            this.menuMaquinas.Size = new System.Drawing.Size(196, 28);
            this.menuMaquinas.TabIndex = 48;
            this.menuMaquinas.Text = "Gestionar Maquinas";
            this.menuMaquinas.UseVisualStyleBackColor = true;
            // 
            // menuUsuarios
            // 
            this.menuUsuarios.AutoSize = true;
            this.menuUsuarios.Location = new System.Drawing.Point(29, 40);
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.Size = new System.Drawing.Size(362, 28);
            this.menuUsuarios.TabIndex = 47;
            this.menuUsuarios.Text = "Administrar Usuario y Agregar Permisos";
            this.menuUsuarios.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(305, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(538, 29);
            this.label9.TabIndex = 53;
            this.label9.Text = "Asiganar permisos al socio dentro del sitema";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.gbGestionGimnasion);
            this.panel1.Controls.Add(this.gbGestionRutinas);
            this.panel1.Controls.Add(this.gbGestionSocios);
            this.panel1.Location = new System.Drawing.Point(67, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1203, 611);
            this.panel1.TabIndex = 54;
            // 
            // frmAcciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.ClientSize = new System.Drawing.Size(1347, 734);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnGuardar);
            this.Name = "frmAcciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAcciones";
            this.Load += new System.EventHandler(this.frmAcciones_Load);
            this.gbGestionSocios.ResumeLayout(false);
            this.gbGestionSocios.PerformLayout();
            this.gbGestionRutinas.ResumeLayout(false);
            this.gbGestionRutinas.PerformLayout();
            this.gbGestionGimnasion.ResumeLayout(false);
            this.gbGestionGimnasion.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnGuardar;
        private System.Windows.Forms.CheckBox btnMenuAgregar;
        private System.Windows.Forms.GroupBox gbGestionSocios;
        private System.Windows.Forms.CheckBox btnMenuConsultar;
        private System.Windows.Forms.CheckBox btnMenuEliminar;
        private System.Windows.Forms.CheckBox btnMenuTurno;
        private System.Windows.Forms.GroupBox gbGestionRutinas;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.GroupBox gbGestionGimnasion;
        private System.Windows.Forms.CheckBox menuRangosHorarios;
        private System.Windows.Forms.CheckBox menuEquipamiento;
        private System.Windows.Forms.CheckBox menuEjercicios;
        private System.Windows.Forms.CheckBox menuNegocio;
        private System.Windows.Forms.CheckBox menuRoles;
        private System.Windows.Forms.CheckBox menuMaquinas;
        private System.Windows.Forms.CheckBox menuUsuarios;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
    }
}