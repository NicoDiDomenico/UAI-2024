namespace Vista
{
    partial class frmEditarSocio
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
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtNombreYApellido = new System.Windows.Forms.TextBox();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.cboGenero = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNroDocumento = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCiudad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtObraSocial = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnObraSocial = new FontAwesome.Sharp.IconButton();
            this.btnEmail = new FontAwesome.Sharp.IconButton();
            this.btnTelefono = new FontAwesome.Sharp.IconButton();
            this.btnDireccion = new FontAwesome.Sharp.IconButton();
            this.btnCiudad = new FontAwesome.Sharp.IconButton();
            this.btnNroDocumento = new FontAwesome.Sharp.IconButton();
            this.btnNombreYApellido = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkSabado = new System.Windows.Forms.CheckBox();
            this.chkViernes = new System.Windows.Forms.CheckBox();
            this.chkJueves = new System.Windows.Forms.CheckBox();
            this.chkMiercoles = new System.Windows.Forms.CheckBox();
            this.chkMartes = new System.Windows.Forms.CheckBox();
            this.chkLunes = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblRenovar = new System.Windows.Forms.Label();
            this.btnRenovarCuota = new FontAwesome.Sharp.IconButton();
            this.lblMensajeEstado = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cboEstado = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.chkAnual = new System.Windows.Forms.CheckBox();
            this.chkMensual = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnConfirmar = new FontAwesome.Sharp.IconButton();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label10.Location = new System.Drawing.Point(135, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1500, 632);
            this.label10.TabIndex = 59;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label2.Location = new System.Drawing.Point(264, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 17);
            this.label2.TabIndex = 35;
            this.label2.Text = "Nombre y Apellido:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label3.Location = new System.Drawing.Point(346, 309);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 36;
            this.label3.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(414, 312);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(492, 23);
            this.txtEmail.TabIndex = 37;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyDown);
            // 
            // txtNombreYApellido
            // 
            this.txtNombreYApellido.Location = new System.Drawing.Point(414, 59);
            this.txtNombreYApellido.Name = "txtNombreYApellido";
            this.txtNombreYApellido.Size = new System.Drawing.Size(492, 23);
            this.txtNombreYApellido.TabIndex = 38;
            this.txtNombreYApellido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreYApellido_KeyDown);
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(414, 95);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(492, 23);
            this.dtpFechaNacimiento.TabIndex = 58;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label14.Location = new System.Drawing.Point(266, 100);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(125, 17);
            this.label14.TabIndex = 57;
            this.label14.Text = "Fecha Nacimiento:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label1.Location = new System.Drawing.Point(327, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 59;
            this.label1.Text = "ID Socio:";
            // 
            // txtId
            // 
            this.txtId.Enabled = false;
            this.txtId.Location = new System.Drawing.Point(414, 24);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(492, 23);
            this.txtId.TabIndex = 60;
            // 
            // cboGenero
            // 
            this.cboGenero.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cboGenero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGenero.FormattingEnabled = true;
            this.cboGenero.Location = new System.Drawing.Point(414, 131);
            this.cboGenero.Name = "cboGenero";
            this.cboGenero.Size = new System.Drawing.Size(190, 24);
            this.cboGenero.TabIndex = 65;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label13.Location = new System.Drawing.Point(332, 134);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 17);
            this.label13.TabIndex = 64;
            this.label13.Text = "Genero:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label12.Location = new System.Drawing.Point(285, 171);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 17);
            this.label12.TabIndex = 66;
            this.label12.Text = "NroDocumento:";
            // 
            // txtNroDocumento
            // 
            this.txtNroDocumento.Location = new System.Drawing.Point(414, 168);
            this.txtNroDocumento.Name = "txtNroDocumento";
            this.txtNroDocumento.Size = new System.Drawing.Size(492, 23);
            this.txtNroDocumento.TabIndex = 67;
            this.txtNroDocumento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNroDocumento_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label6.Location = new System.Drawing.Point(336, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 68;
            this.label6.Text = "Ciudad:";
            // 
            // txtCiudad
            // 
            this.txtCiudad.Location = new System.Drawing.Point(414, 204);
            this.txtCiudad.Name = "txtCiudad";
            this.txtCiudad.Size = new System.Drawing.Size(492, 23);
            this.txtCiudad.TabIndex = 69;
            this.txtCiudad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCiudad_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label5.Location = new System.Drawing.Point(321, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 70;
            this.label5.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.BackColor = System.Drawing.SystemColors.Window;
            this.txtDireccion.Location = new System.Drawing.Point(414, 240);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(492, 23);
            this.txtDireccion.TabIndex = 71;
            this.txtDireccion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDireccion_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label4.Location = new System.Drawing.Point(323, 276);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 72;
            this.label4.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(414, 276);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(492, 23);
            this.txtTelefono.TabIndex = 73;
            this.txtTelefono.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelefono_KeyDown);
            // 
            // txtObraSocial
            // 
            this.txtObraSocial.Location = new System.Drawing.Point(414, 348);
            this.txtObraSocial.Name = "txtObraSocial";
            this.txtObraSocial.Size = new System.Drawing.Size(492, 23);
            this.txtObraSocial.TabIndex = 74;
            this.txtObraSocial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtObraSocial_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label7.Location = new System.Drawing.Point(304, 351);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 17);
            this.label7.TabIndex = 75;
            this.label7.Text = "Obra Social";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label8.Location = new System.Drawing.Point(284, 392);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 17);
            this.label8.TabIndex = 76;
            this.label8.Text = "Dias Asistencia:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel2.Controls.Add(this.btnObraSocial);
            this.panel2.Controls.Add(this.btnEmail);
            this.panel2.Controls.Add(this.btnTelefono);
            this.panel2.Controls.Add(this.btnDireccion);
            this.panel2.Controls.Add(this.btnCiudad);
            this.panel2.Controls.Add(this.btnNroDocumento);
            this.panel2.Controls.Add(this.btnNombreYApellido);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtNombreYApellido);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtEmail);
            this.panel2.Controls.Add(this.txtObraSocial);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtTelefono);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.dtpFechaNacimiento);
            this.panel2.Controls.Add(this.txtDireccion);
            this.panel2.Controls.Add(this.txtId);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.txtCiudad);
            this.panel2.Controls.Add(this.cboGenero);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.txtNroDocumento);
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(199, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1361, 424);
            this.panel2.TabIndex = 79;
            // 
            // btnObraSocial
            // 
            this.btnObraSocial.BackColor = System.Drawing.Color.White;
            this.btnObraSocial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnObraSocial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnObraSocial.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnObraSocial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObraSocial.ForeColor = System.Drawing.Color.White;
            this.btnObraSocial.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnObraSocial.IconColor = System.Drawing.Color.Black;
            this.btnObraSocial.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnObraSocial.IconSize = 20;
            this.btnObraSocial.Location = new System.Drawing.Point(923, 348);
            this.btnObraSocial.Name = "btnObraSocial";
            this.btnObraSocial.Size = new System.Drawing.Size(36, 23);
            this.btnObraSocial.TabIndex = 88;
            this.btnObraSocial.UseVisualStyleBackColor = false;
            this.btnObraSocial.Click += new System.EventHandler(this.btnObraSocial_Click);
            // 
            // btnEmail
            // 
            this.btnEmail.BackColor = System.Drawing.Color.White;
            this.btnEmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmail.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmail.ForeColor = System.Drawing.Color.White;
            this.btnEmail.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnEmail.IconColor = System.Drawing.Color.Black;
            this.btnEmail.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEmail.IconSize = 20;
            this.btnEmail.Location = new System.Drawing.Point(923, 312);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(36, 23);
            this.btnEmail.TabIndex = 87;
            this.btnEmail.UseVisualStyleBackColor = false;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // btnTelefono
            // 
            this.btnTelefono.BackColor = System.Drawing.Color.White;
            this.btnTelefono.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTelefono.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTelefono.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTelefono.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTelefono.ForeColor = System.Drawing.Color.White;
            this.btnTelefono.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnTelefono.IconColor = System.Drawing.Color.Black;
            this.btnTelefono.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnTelefono.IconSize = 20;
            this.btnTelefono.Location = new System.Drawing.Point(923, 276);
            this.btnTelefono.Name = "btnTelefono";
            this.btnTelefono.Size = new System.Drawing.Size(36, 23);
            this.btnTelefono.TabIndex = 86;
            this.btnTelefono.UseVisualStyleBackColor = false;
            this.btnTelefono.Click += new System.EventHandler(this.btnTelefono_Click);
            // 
            // btnDireccion
            // 
            this.btnDireccion.BackColor = System.Drawing.Color.White;
            this.btnDireccion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDireccion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDireccion.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDireccion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDireccion.ForeColor = System.Drawing.Color.White;
            this.btnDireccion.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnDireccion.IconColor = System.Drawing.Color.Black;
            this.btnDireccion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDireccion.IconSize = 20;
            this.btnDireccion.Location = new System.Drawing.Point(923, 240);
            this.btnDireccion.Name = "btnDireccion";
            this.btnDireccion.Size = new System.Drawing.Size(36, 23);
            this.btnDireccion.TabIndex = 85;
            this.btnDireccion.UseVisualStyleBackColor = false;
            this.btnDireccion.Click += new System.EventHandler(this.btnDireccion_Click);
            // 
            // btnCiudad
            // 
            this.btnCiudad.BackColor = System.Drawing.Color.White;
            this.btnCiudad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCiudad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCiudad.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCiudad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCiudad.ForeColor = System.Drawing.Color.White;
            this.btnCiudad.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnCiudad.IconColor = System.Drawing.Color.Black;
            this.btnCiudad.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCiudad.IconSize = 20;
            this.btnCiudad.Location = new System.Drawing.Point(923, 204);
            this.btnCiudad.Name = "btnCiudad";
            this.btnCiudad.Size = new System.Drawing.Size(36, 23);
            this.btnCiudad.TabIndex = 84;
            this.btnCiudad.UseVisualStyleBackColor = false;
            this.btnCiudad.Click += new System.EventHandler(this.btnCiudad_Click);
            // 
            // btnNroDocumento
            // 
            this.btnNroDocumento.BackColor = System.Drawing.Color.White;
            this.btnNroDocumento.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNroDocumento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNroDocumento.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnNroDocumento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNroDocumento.ForeColor = System.Drawing.Color.White;
            this.btnNroDocumento.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnNroDocumento.IconColor = System.Drawing.Color.Black;
            this.btnNroDocumento.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNroDocumento.IconSize = 20;
            this.btnNroDocumento.Location = new System.Drawing.Point(923, 168);
            this.btnNroDocumento.Name = "btnNroDocumento";
            this.btnNroDocumento.Size = new System.Drawing.Size(36, 23);
            this.btnNroDocumento.TabIndex = 83;
            this.btnNroDocumento.UseVisualStyleBackColor = false;
            this.btnNroDocumento.Click += new System.EventHandler(this.btnNroDocumento_Click);
            // 
            // btnNombreYApellido
            // 
            this.btnNombreYApellido.BackColor = System.Drawing.Color.White;
            this.btnNombreYApellido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNombreYApellido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNombreYApellido.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnNombreYApellido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNombreYApellido.ForeColor = System.Drawing.Color.White;
            this.btnNombreYApellido.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnNombreYApellido.IconColor = System.Drawing.Color.Black;
            this.btnNombreYApellido.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNombreYApellido.IconSize = 20;
            this.btnNombreYApellido.Location = new System.Drawing.Point(923, 59);
            this.btnNombreYApellido.Name = "btnNombreYApellido";
            this.btnNombreYApellido.Size = new System.Drawing.Size(36, 23);
            this.btnNombreYApellido.TabIndex = 81;
            this.btnNombreYApellido.UseVisualStyleBackColor = false;
            this.btnNombreYApellido.Click += new System.EventHandler(this.btnNombreYApellido_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkSabado);
            this.panel1.Controls.Add(this.chkViernes);
            this.panel1.Controls.Add(this.chkJueves);
            this.panel1.Controls.Add(this.chkMiercoles);
            this.panel1.Controls.Add(this.chkMartes);
            this.panel1.Controls.Add(this.chkLunes);
            this.panel1.Location = new System.Drawing.Point(414, 381);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 35);
            this.panel1.TabIndex = 79;
            // 
            // chkSabado
            // 
            this.chkSabado.AutoSize = true;
            this.chkSabado.Location = new System.Drawing.Point(659, 7);
            this.chkSabado.Name = "chkSabado";
            this.chkSabado.Size = new System.Drawing.Size(76, 21);
            this.chkSabado.TabIndex = 82;
            this.chkSabado.Text = "Sabado";
            this.chkSabado.UseVisualStyleBackColor = true;
            // 
            // chkViernes
            // 
            this.chkViernes.AutoSize = true;
            this.chkViernes.Location = new System.Drawing.Point(530, 7);
            this.chkViernes.Name = "chkViernes";
            this.chkViernes.Size = new System.Drawing.Size(75, 21);
            this.chkViernes.TabIndex = 81;
            this.chkViernes.Text = "Viernes";
            this.chkViernes.UseVisualStyleBackColor = true;
            // 
            // chkJueves
            // 
            this.chkJueves.AutoSize = true;
            this.chkJueves.Location = new System.Drawing.Point(401, 7);
            this.chkJueves.Name = "chkJueves";
            this.chkJueves.Size = new System.Drawing.Size(72, 21);
            this.chkJueves.TabIndex = 80;
            this.chkJueves.Text = "Jueves";
            this.chkJueves.UseVisualStyleBackColor = true;
            // 
            // chkMiercoles
            // 
            this.chkMiercoles.AutoSize = true;
            this.chkMiercoles.Location = new System.Drawing.Point(272, 7);
            this.chkMiercoles.Name = "chkMiercoles";
            this.chkMiercoles.Size = new System.Drawing.Size(87, 21);
            this.chkMiercoles.TabIndex = 79;
            this.chkMiercoles.Text = "Miercoles";
            this.chkMiercoles.UseVisualStyleBackColor = true;
            // 
            // chkMartes
            // 
            this.chkMartes.AutoSize = true;
            this.chkMartes.Location = new System.Drawing.Point(143, 7);
            this.chkMartes.Name = "chkMartes";
            this.chkMartes.Size = new System.Drawing.Size(70, 21);
            this.chkMartes.TabIndex = 78;
            this.chkMartes.Text = "Martes";
            this.chkMartes.UseVisualStyleBackColor = true;
            // 
            // chkLunes
            // 
            this.chkLunes.AutoSize = true;
            this.chkLunes.Location = new System.Drawing.Point(14, 7);
            this.chkLunes.Name = "chkLunes";
            this.chkLunes.Size = new System.Drawing.Size(66, 21);
            this.chkLunes.TabIndex = 77;
            this.chkLunes.Text = "Lunes";
            this.chkLunes.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(196, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(178, 24);
            this.label9.TabIndex = 80;
            this.label9.Text = "Datos Personales:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(196, 504);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(126, 24);
            this.label15.TabIndex = 92;
            this.label15.Text = "Facturación:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel4.Controls.Add(this.lblRenovar);
            this.panel4.Controls.Add(this.btnRenovarCuota);
            this.panel4.Controls.Add(this.lblMensajeEstado);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.cboEstado);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(200, 531);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1359, 104);
            this.panel4.TabIndex = 91;
            // 
            // lblRenovar
            // 
            this.lblRenovar.AutoSize = true;
            this.lblRenovar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblRenovar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenovar.Location = new System.Drawing.Point(696, 26);
            this.lblRenovar.Name = "lblRenovar";
            this.lblRenovar.Size = new System.Drawing.Size(129, 20);
            this.lblRenovar.TabIndex = 94;
            this.lblRenovar.Text = "Renovar Cuota";
            // 
            // btnRenovarCuota
            // 
            this.btnRenovarCuota.IconChar = FontAwesome.Sharp.IconChar.SyncAlt;
            this.btnRenovarCuota.IconColor = System.Drawing.Color.Black;
            this.btnRenovarCuota.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRenovarCuota.Location = new System.Drawing.Point(639, 12);
            this.btnRenovarCuota.Margin = new System.Windows.Forms.Padding(0);
            this.btnRenovarCuota.Name = "btnRenovarCuota";
            this.btnRenovarCuota.Size = new System.Drawing.Size(45, 45);
            this.btnRenovarCuota.TabIndex = 93;
            this.btnRenovarCuota.UseVisualStyleBackColor = true;
            this.btnRenovarCuota.Click += new System.EventHandler(this.btnRenovarCuota_Click);
            // 
            // lblMensajeEstado
            // 
            this.lblMensajeEstado.AutoSize = true;
            this.lblMensajeEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblMensajeEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeEstado.ForeColor = System.Drawing.Color.Black;
            this.lblMensajeEstado.Location = new System.Drawing.Point(636, 71);
            this.lblMensajeEstado.Name = "lblMensajeEstado";
            this.lblMensajeEstado.Size = new System.Drawing.Size(77, 17);
            this.lblMensajeEstado.TabIndex = 89;
            this.lblMensajeEstado.Text = "<Mensaje>";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label17.Location = new System.Drawing.Point(335, 71);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 17);
            this.label17.TabIndex = 89;
            this.label17.Text = "Estado:";
            // 
            // cboEstado
            // 
            this.cboEstado.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(413, 68);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(190, 24);
            this.cboEstado.TabIndex = 90;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.chkAnual);
            this.panel5.Controls.Add(this.chkMensual);
            this.panel5.Location = new System.Drawing.Point(413, 12);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(213, 45);
            this.panel5.TabIndex = 90;
            // 
            // chkAnual
            // 
            this.chkAnual.AutoSize = true;
            this.chkAnual.Location = new System.Drawing.Point(143, 13);
            this.chkAnual.Name = "chkAnual";
            this.chkAnual.Size = new System.Drawing.Size(63, 21);
            this.chkAnual.TabIndex = 78;
            this.chkAnual.Text = "Anual";
            this.chkAnual.UseVisualStyleBackColor = true;
            this.chkAnual.CheckedChanged += new System.EventHandler(this.chkAnual_CheckedChanged);
            // 
            // chkMensual
            // 
            this.chkMensual.AutoSize = true;
            this.chkMensual.Location = new System.Drawing.Point(14, 12);
            this.chkMensual.Name = "chkMensual";
            this.chkMensual.Size = new System.Drawing.Size(80, 21);
            this.chkMensual.TabIndex = 77;
            this.chkMensual.Text = "Mensual";
            this.chkMensual.UseVisualStyleBackColor = true;
            this.chkMensual.CheckedChanged += new System.EventHandler(this.chkMensual_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label16.Location = new System.Drawing.Point(351, 25);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 17);
            this.label16.TabIndex = 89;
            this.label16.Text = "Plan:";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.IconChar = FontAwesome.Sharp.IconChar.UserCheck;
            this.btnConfirmar.IconColor = System.Drawing.Color.White;
            this.btnConfirmar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnConfirmar.IconSize = 25;
            this.btnConfirmar.Location = new System.Drawing.Point(822, 665);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(150, 50);
            this.btnConfirmar.TabIndex = 93;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // frmEditarSocio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1788, 727);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label10);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditarSocio";
            this.Text = "frmEditarSocio";
            this.Load += new System.EventHandler(this.frmEditarSocio_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtNombreYApellido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboGenero;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNroDocumento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCiudad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtObraSocial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkSabado;
        private System.Windows.Forms.CheckBox chkViernes;
        private System.Windows.Forms.CheckBox chkJueves;
        private System.Windows.Forms.CheckBox chkMiercoles;
        private System.Windows.Forms.CheckBox chkMartes;
        private System.Windows.Forms.CheckBox chkLunes;
        private System.Windows.Forms.Label label9;
        private FontAwesome.Sharp.IconButton btnObraSocial;
        private FontAwesome.Sharp.IconButton btnEmail;
        private FontAwesome.Sharp.IconButton btnTelefono;
        private FontAwesome.Sharp.IconButton btnDireccion;
        private FontAwesome.Sharp.IconButton btnCiudad;
        private FontAwesome.Sharp.IconButton btnNroDocumento;
        private FontAwesome.Sharp.IconButton btnNombreYApellido;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox chkAnual;
        private System.Windows.Forms.CheckBox chkMensual;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblMensajeEstado;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cboEstado;
        private FontAwesome.Sharp.IconButton btnConfirmar;
        private System.Windows.Forms.Label lblRenovar;
        private FontAwesome.Sharp.IconButton btnRenovarCuota;
    }
}