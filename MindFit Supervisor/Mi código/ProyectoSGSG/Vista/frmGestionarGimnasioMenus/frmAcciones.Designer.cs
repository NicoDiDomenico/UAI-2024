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
            this.btnRestaurar = new System.Windows.Forms.CheckBox();
            this.btnHistorial = new System.Windows.Forms.CheckBox();
            this.btnEliminar = new System.Windows.Forms.CheckBox();
            this.btnGuardarRutina = new System.Windows.Forms.CheckBox();
            this.gbGestionGimnasion = new System.Windows.Forms.GroupBox();
            this.menuRangosHorarios = new System.Windows.Forms.CheckBox();
            this.menuEstiramiento = new System.Windows.Forms.CheckBox();
            this.menuElementosGym = new System.Windows.Forms.CheckBox();
            this.menuNegocio = new System.Windows.Forms.CheckBox();
            this.menuRoles = new System.Windows.Forms.CheckBox();
            this.menuCalentamiento = new System.Windows.Forms.CheckBox();
            this.menuUsuarios = new System.Windows.Forms.CheckBox();
            this.Panel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbTodo = new System.Windows.Forms.CheckBox();
            this.gbGestionSocios.SuspendLayout();
            this.gbGestionRutinas.SuspendLayout();
            this.gbGestionGimnasion.SuspendLayout();
            this.Panel.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.btnGuardar.Location = new System.Drawing.Point(608, 546);
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
            this.gbGestionSocios.Location = new System.Drawing.Point(43, 23);
            this.gbGestionSocios.Name = "gbGestionSocios";
            this.gbGestionSocios.Size = new System.Drawing.Size(520, 200);
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
            this.btnMenuEliminar.Location = new System.Drawing.Point(29, 146);
            this.btnMenuEliminar.Name = "btnMenuEliminar";
            this.btnMenuEliminar.Size = new System.Drawing.Size(150, 28);
            this.btnMenuEliminar.TabIndex = 49;
            this.btnMenuEliminar.Text = "Eliminar Socio";
            this.btnMenuEliminar.UseVisualStyleBackColor = true;
            // 
            // btnMenuTurno
            // 
            this.btnMenuTurno.AutoSize = true;
            this.btnMenuTurno.Location = new System.Drawing.Point(288, 146);
            this.btnMenuTurno.Name = "btnMenuTurno";
            this.btnMenuTurno.Size = new System.Drawing.Size(174, 28);
            this.btnMenuTurno.TabIndex = 48;
            this.btnMenuTurno.Text = "Gestionar Turnos";
            this.btnMenuTurno.UseVisualStyleBackColor = true;
            // 
            // gbGestionRutinas
            // 
            this.gbGestionRutinas.Controls.Add(this.btnRestaurar);
            this.gbGestionRutinas.Controls.Add(this.btnHistorial);
            this.gbGestionRutinas.Controls.Add(this.btnEliminar);
            this.gbGestionRutinas.Controls.Add(this.btnGuardarRutina);
            this.gbGestionRutinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGestionRutinas.Location = new System.Drawing.Point(43, 233);
            this.gbGestionRutinas.Name = "gbGestionRutinas";
            this.gbGestionRutinas.Size = new System.Drawing.Size(520, 200);
            this.gbGestionRutinas.TabIndex = 51;
            this.gbGestionRutinas.TabStop = false;
            this.gbGestionRutinas.Text = "Gestión Rutinas";
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.AutoSize = true;
            this.btnRestaurar.Location = new System.Drawing.Point(288, 40);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(167, 28);
            this.btnRestaurar.TabIndex = 50;
            this.btnRestaurar.Text = "Restaurar Rutina";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            // 
            // btnHistorial
            // 
            this.btnHistorial.AutoSize = true;
            this.btnHistorial.Location = new System.Drawing.Point(29, 146);
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.Size = new System.Drawing.Size(197, 28);
            this.btnHistorial.TabIndex = 49;
            this.btnHistorial.Text = "Ver Historial Rutinas";
            this.btnHistorial.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.AutoSize = true;
            this.btnEliminar.Location = new System.Drawing.Point(288, 146);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(214, 28);
            this.btnEliminar.TabIndex = 48;
            this.btnEliminar.Text = "Eliminar Dia de Rutina";
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnGuardarRutina
            // 
            this.btnGuardarRutina.AutoSize = true;
            this.btnGuardarRutina.Location = new System.Drawing.Point(29, 40);
            this.btnGuardarRutina.Name = "btnGuardarRutina";
            this.btnGuardarRutina.Size = new System.Drawing.Size(168, 28);
            this.btnGuardarRutina.TabIndex = 47;
            this.btnGuardarRutina.Text = "Actualizar Rutina";
            this.btnGuardarRutina.UseVisualStyleBackColor = true;
            // 
            // gbGestionGimnasion
            // 
            this.gbGestionGimnasion.Controls.Add(this.menuRangosHorarios);
            this.gbGestionGimnasion.Controls.Add(this.menuEstiramiento);
            this.gbGestionGimnasion.Controls.Add(this.menuElementosGym);
            this.gbGestionGimnasion.Controls.Add(this.menuNegocio);
            this.gbGestionGimnasion.Controls.Add(this.menuRoles);
            this.gbGestionGimnasion.Controls.Add(this.menuCalentamiento);
            this.gbGestionGimnasion.Controls.Add(this.menuUsuarios);
            this.gbGestionGimnasion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGestionGimnasion.Location = new System.Drawing.Point(619, 23);
            this.gbGestionGimnasion.Name = "gbGestionGimnasion";
            this.gbGestionGimnasion.Size = new System.Drawing.Size(540, 410);
            this.gbGestionGimnasion.TabIndex = 52;
            this.gbGestionGimnasion.TabStop = false;
            this.gbGestionGimnasion.Text = "Gestión Gimnasio";
            // 
            // menuRangosHorarios
            // 
            this.menuRangosHorarios.AutoSize = true;
            this.menuRangosHorarios.Location = new System.Drawing.Point(29, 303);
            this.menuRangosHorarios.Name = "menuRangosHorarios";
            this.menuRangosHorarios.Size = new System.Drawing.Size(302, 28);
            this.menuRangosHorarios.TabIndex = 53;
            this.menuRangosHorarios.Text = "Asignar Horarios y Entrenadores";
            this.menuRangosHorarios.UseVisualStyleBackColor = true;
            // 
            // menuEstiramiento
            // 
            this.menuEstiramiento.AutoSize = true;
            this.menuEstiramiento.Location = new System.Drawing.Point(29, 250);
            this.menuEstiramiento.Name = "menuEstiramiento";
            this.menuEstiramiento.Size = new System.Drawing.Size(326, 28);
            this.menuEstiramiento.TabIndex = 52;
            this.menuEstiramiento.Text = "Gestionar Técnicas de Estiramiento";
            this.menuEstiramiento.UseVisualStyleBackColor = true;
            // 
            // menuElementosGym
            // 
            this.menuElementosGym.AutoSize = true;
            this.menuElementosGym.Location = new System.Drawing.Point(29, 198);
            this.menuElementosGym.Name = "menuElementosGym";
            this.menuElementosGym.Size = new System.Drawing.Size(364, 28);
            this.menuElementosGym.TabIndex = 51;
            this.menuElementosGym.Text = "Gestionar Elementos de Entrenamiento ";
            this.menuElementosGym.UseVisualStyleBackColor = true;
            // 
            // menuNegocio
            // 
            this.menuNegocio.AutoSize = true;
            this.menuNegocio.Location = new System.Drawing.Point(29, 356);
            this.menuNegocio.Name = "menuNegocio";
            this.menuNegocio.Size = new System.Drawing.Size(244, 28);
            this.menuNegocio.TabIndex = 50;
            this.menuNegocio.Text = "Editar del Datos Gimnasio";
            this.menuNegocio.UseVisualStyleBackColor = true;
            // 
            // menuRoles
            // 
            this.menuRoles.AutoSize = true;
            this.menuRoles.Location = new System.Drawing.Point(29, 93);
            this.menuRoles.Name = "menuRoles";
            this.menuRoles.Size = new System.Drawing.Size(319, 28);
            this.menuRoles.TabIndex = 49;
            this.menuRoles.Text = "Modificar Rol y Consultar Permisos";
            this.menuRoles.UseVisualStyleBackColor = true;
            // 
            // menuCalentamiento
            // 
            this.menuCalentamiento.AutoSize = true;
            this.menuCalentamiento.Location = new System.Drawing.Point(29, 146);
            this.menuCalentamiento.Name = "menuCalentamiento";
            this.menuCalentamiento.Size = new System.Drawing.Size(396, 28);
            this.menuCalentamiento.TabIndex = 48;
            this.menuCalentamiento.Text = "Gestionar Calentamientos con/sin Máquinas";
            this.menuCalentamiento.UseVisualStyleBackColor = true;
            // 
            // menuUsuarios
            // 
            this.menuUsuarios.AutoSize = true;
            this.menuUsuarios.Location = new System.Drawing.Point(31, 40);
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.Size = new System.Drawing.Size(362, 28);
            this.menuUsuarios.TabIndex = 47;
            this.menuUsuarios.Text = "Administrar Usuario y Agregar Permisos";
            this.menuUsuarios.UseVisualStyleBackColor = true;
            // 
            // Panel
            // 
            this.Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel.Controls.Add(this.gbGestionRutinas);
            this.Panel.Controls.Add(this.panel2);
            this.Panel.Controls.Add(this.gbGestionGimnasion);
            this.Panel.Controls.Add(this.gbGestionSocios);
            this.Panel.Location = new System.Drawing.Point(67, 19);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(1203, 515);
            this.Panel.TabIndex = 54;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.cbTodo);
            this.panel2.Location = new System.Drawing.Point(505, 452);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(177, 40);
            this.panel2.TabIndex = 55;
            // 
            // cbTodo
            // 
            this.cbTodo.AutoSize = true;
            this.cbTodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTodo.Location = new System.Drawing.Point(14, 7);
            this.cbTodo.Name = "cbTodo";
            this.cbTodo.Size = new System.Drawing.Size(151, 24);
            this.cbTodo.TabIndex = 51;
            this.cbTodo.Text = "Seleccionar Todo";
            this.cbTodo.UseVisualStyleBackColor = true;
            this.cbTodo.CheckedChanged += new System.EventHandler(this.cbTodo_CheckedChanged);
            // 
            // frmAcciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.ClientSize = new System.Drawing.Size(1319, 611);
            this.Controls.Add(this.Panel);
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
            this.Panel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.CheckBox btnRestaurar;
        private System.Windows.Forms.CheckBox btnHistorial;
        private System.Windows.Forms.CheckBox btnEliminar;
        private System.Windows.Forms.CheckBox btnGuardarRutina;
        private System.Windows.Forms.GroupBox gbGestionGimnasion;
        private System.Windows.Forms.CheckBox menuRangosHorarios;
        private System.Windows.Forms.CheckBox menuEstiramiento;
        private System.Windows.Forms.CheckBox menuElementosGym;
        private System.Windows.Forms.CheckBox menuNegocio;
        private System.Windows.Forms.CheckBox menuRoles;
        private System.Windows.Forms.CheckBox menuCalentamiento;
        private System.Windows.Forms.CheckBox menuUsuarios;
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbTodo;
    }
}