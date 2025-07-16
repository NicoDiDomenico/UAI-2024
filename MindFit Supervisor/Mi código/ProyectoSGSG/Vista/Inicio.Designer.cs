namespace Vista
{
    partial class Inicio
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.contenedor = new System.Windows.Forms.Panel();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.lblHoraActual = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.IdTurno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaTurno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdRangoHorario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoraDesde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoraHasta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoIngreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CupoActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CupoMaximo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoTurno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.botones = new System.Windows.Forms.MenuStrip();
            this.menuGestionarRutinas = new FontAwesome.Sharp.IconMenuItem();
            this.menuSocios = new FontAwesome.Sharp.IconMenuItem();
            this.menuGestionarGimnasio = new FontAwesome.Sharp.IconMenuItem();
            this.menuTitulo = new System.Windows.Forms.MenuStrip();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.botonesTop = new System.Windows.Forms.MenuStrip();
            this.menuTopGestionarRutinas = new FontAwesome.Sharp.IconMenuItem();
            this.menuTopSocios = new FontAwesome.Sharp.IconMenuItem();
            this.menuTopGestionarGimnasio = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem6 = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem7 = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem8 = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem9 = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem10 = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem11 = new FontAwesome.Sharp.IconMenuItem();
            this.btnValidar = new FontAwesome.Sharp.IconButton();
            this.picLogoInicio = new System.Windows.Forms.PictureBox();
            this.btnSalir = new FontAwesome.Sharp.IconButton();
            this.iconSplitButton1 = new FontAwesome.Sharp.IconSplitButton();
            this.iconSplitButton2 = new FontAwesome.Sharp.IconSplitButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblRol = new System.Windows.Forms.Label();
            this.contenedor.SuspendLayout();
            this.panelPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.botones.SuspendLayout();
            this.botonesTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoInicio)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1315, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Usuario:";
            // 
            // contenedor
            // 
            this.contenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.contenedor.Controls.Add(this.panelPrincipal);
            this.contenedor.Controls.Add(this.botones);
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(0, 162);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(1796, 772);
            this.contenedor.TabIndex = 10;
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.Controls.Add(this.lblHoraActual);
            this.panelPrincipal.Controls.Add(this.dgvData);
            this.panelPrincipal.Location = new System.Drawing.Point(24, 18);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(1746, 622);
            this.panelPrincipal.TabIndex = 2;
            // 
            // lblHoraActual
            // 
            this.lblHoraActual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblHoraActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoraActual.ForeColor = System.Drawing.Color.Black;
            this.lblHoraActual.Location = new System.Drawing.Point(165, 2);
            this.lblHoraActual.Name = "lblHoraActual";
            this.lblHoraActual.Size = new System.Drawing.Size(790, 36);
            this.lblHoraActual.TabIndex = 17;
            this.lblHoraActual.Text = "Turnos ...";
            this.lblHoraActual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdTurno,
            this.FechaTurno,
            this.IdRangoHorario,
            this.HoraDesde,
            this.HoraHasta,
            this.CodigoIngreso,
            this.IdUsuario,
            this.NombreSocio,
            this.NombreEntrenador,
            this.IdSocio,
            this.CupoActual,
            this.CupoMaximo,
            this.EstadoTurno});
            this.dgvData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvData.Location = new System.Drawing.Point(165, 51);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvData.RowHeadersVisible = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvData.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvData.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.dgvData.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvData.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.dgvData.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.Height = 40;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1401, 568);
            this.dgvData.TabIndex = 66;
            // 
            // IdTurno
            // 
            this.IdTurno.HeaderText = "IdTurno";
            this.IdTurno.Name = "IdTurno";
            this.IdTurno.ReadOnly = true;
            this.IdTurno.Visible = false;
            // 
            // FechaTurno
            // 
            this.FechaTurno.HeaderText = "Fecha Turno";
            this.FechaTurno.Name = "FechaTurno";
            this.FechaTurno.ReadOnly = true;
            this.FechaTurno.Visible = false;
            this.FechaTurno.Width = 250;
            // 
            // IdRangoHorario
            // 
            this.IdRangoHorario.HeaderText = "IdRangoHorario";
            this.IdRangoHorario.Name = "IdRangoHorario";
            this.IdRangoHorario.ReadOnly = true;
            this.IdRangoHorario.Visible = false;
            // 
            // HoraDesde
            // 
            this.HoraDesde.HeaderText = "Hora Desde";
            this.HoraDesde.Name = "HoraDesde";
            this.HoraDesde.ReadOnly = true;
            this.HoraDesde.Visible = false;
            this.HoraDesde.Width = 170;
            // 
            // HoraHasta
            // 
            this.HoraHasta.HeaderText = "Hora Hasta";
            this.HoraHasta.Name = "HoraHasta";
            this.HoraHasta.ReadOnly = true;
            this.HoraHasta.Visible = false;
            this.HoraHasta.Width = 170;
            // 
            // CodigoIngreso
            // 
            this.CodigoIngreso.HeaderText = "Codigo Ingreso";
            this.CodigoIngreso.Name = "CodigoIngreso";
            this.CodigoIngreso.ReadOnly = true;
            this.CodigoIngreso.Visible = false;
            this.CodigoIngreso.Width = 250;
            // 
            // IdUsuario
            // 
            this.IdUsuario.HeaderText = "IdUsuario";
            this.IdUsuario.Name = "IdUsuario";
            this.IdUsuario.ReadOnly = true;
            this.IdUsuario.Visible = false;
            // 
            // NombreSocio
            // 
            this.NombreSocio.HeaderText = "Socio";
            this.NombreSocio.Name = "NombreSocio";
            this.NombreSocio.ReadOnly = true;
            this.NombreSocio.Width = 550;
            // 
            // NombreEntrenador
            // 
            this.NombreEntrenador.HeaderText = "Entrenador";
            this.NombreEntrenador.Name = "NombreEntrenador";
            this.NombreEntrenador.ReadOnly = true;
            this.NombreEntrenador.Width = 550;
            // 
            // IdSocio
            // 
            this.IdSocio.HeaderText = "IdSocio";
            this.IdSocio.Name = "IdSocio";
            this.IdSocio.ReadOnly = true;
            this.IdSocio.Visible = false;
            // 
            // CupoActual
            // 
            this.CupoActual.HeaderText = "CupoActual";
            this.CupoActual.Name = "CupoActual";
            this.CupoActual.ReadOnly = true;
            this.CupoActual.Visible = false;
            // 
            // CupoMaximo
            // 
            this.CupoMaximo.HeaderText = "CupoMaximo";
            this.CupoMaximo.Name = "CupoMaximo";
            this.CupoMaximo.ReadOnly = true;
            this.CupoMaximo.Visible = false;
            // 
            // EstadoTurno
            // 
            this.EstadoTurno.HeaderText = "Estado del Turno";
            this.EstadoTurno.Name = "EstadoTurno";
            this.EstadoTurno.ReadOnly = true;
            this.EstadoTurno.Width = 300;
            // 
            // botones
            // 
            this.botones.AutoSize = false;
            this.botones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.botones.Dock = System.Windows.Forms.DockStyle.None;
            this.botones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGestionarRutinas,
            this.menuSocios,
            this.menuGestionarGimnasio});
            this.botones.Location = new System.Drawing.Point(590, 643);
            this.botones.Name = "botones";
            this.botones.Size = new System.Drawing.Size(646, 127);
            this.botones.TabIndex = 1;
            this.botones.Text = "menuStrip1";
            // 
            // menuGestionarRutinas
            // 
            this.menuGestionarRutinas.AutoSize = false;
            this.menuGestionarRutinas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.menuGestionarRutinas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuGestionarRutinas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.menuGestionarRutinas.ForeColor = System.Drawing.Color.White;
            this.menuGestionarRutinas.IconChar = FontAwesome.Sharp.IconChar.None;
            this.menuGestionarRutinas.IconColor = System.Drawing.Color.White;
            this.menuGestionarRutinas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuGestionarRutinas.IconSize = 1;
            this.menuGestionarRutinas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuGestionarRutinas.Margin = new System.Windows.Forms.Padding(20);
            this.menuGestionarRutinas.Name = "menuGestionarRutinas";
            this.menuGestionarRutinas.Padding = new System.Windows.Forms.Padding(0);
            this.menuGestionarRutinas.Size = new System.Drawing.Size(170, 80);
            this.menuGestionarRutinas.Text = "Gestionar Rutinas";
            this.menuGestionarRutinas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuGestionarRutinas.Click += new System.EventHandler(this.menuGestionarRutinas_Click);
            // 
            // menuSocios
            // 
            this.menuSocios.AutoSize = false;
            this.menuSocios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.menuSocios.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuSocios.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.menuSocios.ForeColor = System.Drawing.Color.White;
            this.menuSocios.IconChar = FontAwesome.Sharp.IconChar.ScrewdriverWrench;
            this.menuSocios.IconColor = System.Drawing.Color.White;
            this.menuSocios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuSocios.IconSize = 50;
            this.menuSocios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuSocios.Margin = new System.Windows.Forms.Padding(20);
            this.menuSocios.Name = "menuSocios";
            this.menuSocios.Size = new System.Drawing.Size(170, 80);
            this.menuSocios.Text = "Ver Socios";
            this.menuSocios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuSocios.Click += new System.EventHandler(this.menuSocios_Click);
            // 
            // menuGestionarGimnasio
            // 
            this.menuGestionarGimnasio.AutoSize = false;
            this.menuGestionarGimnasio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.menuGestionarGimnasio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuGestionarGimnasio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.menuGestionarGimnasio.ForeColor = System.Drawing.Color.White;
            this.menuGestionarGimnasio.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.menuGestionarGimnasio.IconColor = System.Drawing.Color.White;
            this.menuGestionarGimnasio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuGestionarGimnasio.IconSize = 50;
            this.menuGestionarGimnasio.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuGestionarGimnasio.Margin = new System.Windows.Forms.Padding(20);
            this.menuGestionarGimnasio.Name = "menuGestionarGimnasio";
            this.menuGestionarGimnasio.Size = new System.Drawing.Size(170, 80);
            this.menuGestionarGimnasio.Text = "Gestionar Gimnasio";
            this.menuGestionarGimnasio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuGestionarGimnasio.Click += new System.EventHandler(this.menuGestionarGimnasio_Click);
            // 
            // menuTitulo
            // 
            this.menuTitulo.AutoSize = false;
            this.menuTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.menuTitulo.Location = new System.Drawing.Point(0, 0);
            this.menuTitulo.Name = "menuTitulo";
            this.menuTitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuTitulo.Size = new System.Drawing.Size(1796, 162);
            this.menuTitulo.TabIndex = 8;
            this.menuTitulo.Text = "menuStrip2";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(1393, 25);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(79, 20);
            this.lblUsuario.TabIndex = 12;
            this.lblUsuario.Text = "lblUsuario";
            // 
            // lblLogo
            // 
            this.lblLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.lblLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(22, 112);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(180, 36);
            this.lblLogo.TabIndex = 15;
            this.lblLogo.Text = "<nombreGym>";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // botonesTop
            // 
            this.botonesTop.AutoSize = false;
            this.botonesTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.botonesTop.Dock = System.Windows.Forms.DockStyle.None;
            this.botonesTop.GripMargin = new System.Windows.Forms.Padding(2);
            this.botonesTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTopGestionarRutinas,
            this.menuTopSocios,
            this.menuTopGestionarGimnasio,
            this.iconMenuItem6,
            this.iconMenuItem7,
            this.iconMenuItem8,
            this.iconMenuItem9,
            this.iconMenuItem10,
            this.iconMenuItem11});
            this.botonesTop.Location = new System.Drawing.Point(1316, 98);
            this.botonesTop.Name = "botonesTop";
            this.botonesTop.Padding = new System.Windows.Forms.Padding(1);
            this.botonesTop.Size = new System.Drawing.Size(454, 52);
            this.botonesTop.TabIndex = 2;
            this.botonesTop.Text = "menuStrip1";
            // 
            // menuTopGestionarRutinas
            // 
            this.menuTopGestionarRutinas.AutoSize = false;
            this.menuTopGestionarRutinas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.menuTopGestionarRutinas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuTopGestionarRutinas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.menuTopGestionarRutinas.ForeColor = System.Drawing.Color.White;
            this.menuTopGestionarRutinas.IconChar = FontAwesome.Sharp.IconChar.None;
            this.menuTopGestionarRutinas.IconColor = System.Drawing.Color.White;
            this.menuTopGestionarRutinas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuTopGestionarRutinas.IconSize = 1;
            this.menuTopGestionarRutinas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuTopGestionarRutinas.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.menuTopGestionarRutinas.Name = "menuTopGestionarRutinas";
            this.menuTopGestionarRutinas.Padding = new System.Windows.Forms.Padding(0);
            this.menuTopGestionarRutinas.Size = new System.Drawing.Size(150, 50);
            this.menuTopGestionarRutinas.Text = "Gestionar Rutinas";
            this.menuTopGestionarRutinas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuTopGestionarRutinas.Click += new System.EventHandler(this.menuTopGestionarRutinas_Click);
            // 
            // menuTopSocios
            // 
            this.menuTopSocios.AutoSize = false;
            this.menuTopSocios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.menuTopSocios.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuTopSocios.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.menuTopSocios.ForeColor = System.Drawing.Color.White;
            this.menuTopSocios.IconChar = FontAwesome.Sharp.IconChar.ScrewdriverWrench;
            this.menuTopSocios.IconColor = System.Drawing.Color.White;
            this.menuTopSocios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuTopSocios.IconSize = 50;
            this.menuTopSocios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuTopSocios.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.menuTopSocios.Name = "menuTopSocios";
            this.menuTopSocios.Padding = new System.Windows.Forms.Padding(0);
            this.menuTopSocios.Size = new System.Drawing.Size(150, 50);
            this.menuTopSocios.Text = "Ver Socios";
            this.menuTopSocios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuTopSocios.Click += new System.EventHandler(this.menuTopSocios_Click);
            // 
            // menuTopGestionarGimnasio
            // 
            this.menuTopGestionarGimnasio.AutoSize = false;
            this.menuTopGestionarGimnasio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.menuTopGestionarGimnasio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuTopGestionarGimnasio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.menuTopGestionarGimnasio.ForeColor = System.Drawing.Color.White;
            this.menuTopGestionarGimnasio.IconChar = FontAwesome.Sharp.IconChar.None;
            this.menuTopGestionarGimnasio.IconColor = System.Drawing.Color.White;
            this.menuTopGestionarGimnasio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuTopGestionarGimnasio.IconSize = 1;
            this.menuTopGestionarGimnasio.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuTopGestionarGimnasio.Name = "menuTopGestionarGimnasio";
            this.menuTopGestionarGimnasio.Padding = new System.Windows.Forms.Padding(0);
            this.menuTopGestionarGimnasio.Size = new System.Drawing.Size(150, 50);
            this.menuTopGestionarGimnasio.Text = "Gestionar Gimnasio";
            this.menuTopGestionarGimnasio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuTopGestionarGimnasio.Click += new System.EventHandler(this.menuTopGestionarGimnasio_Click);
            // 
            // iconMenuItem6
            // 
            this.iconMenuItem6.AutoSize = false;
            this.iconMenuItem6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.iconMenuItem6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.iconMenuItem6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.iconMenuItem6.ForeColor = System.Drawing.Color.White;
            this.iconMenuItem6.IconChar = FontAwesome.Sharp.IconChar.ScrewdriverWrench;
            this.iconMenuItem6.IconColor = System.Drawing.Color.White;
            this.iconMenuItem6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem6.IconSize = 50;
            this.iconMenuItem6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItem6.Name = "iconMenuItem6";
            this.iconMenuItem6.Size = new System.Drawing.Size(160, 80);
            this.iconMenuItem6.Text = "Ver Socios";
            this.iconMenuItem6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // iconMenuItem7
            // 
            this.iconMenuItem7.AutoSize = false;
            this.iconMenuItem7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.iconMenuItem7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.iconMenuItem7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.iconMenuItem7.ForeColor = System.Drawing.Color.White;
            this.iconMenuItem7.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.iconMenuItem7.IconColor = System.Drawing.Color.White;
            this.iconMenuItem7.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem7.IconSize = 50;
            this.iconMenuItem7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItem7.Name = "iconMenuItem7";
            this.iconMenuItem7.Size = new System.Drawing.Size(170, 80);
            this.iconMenuItem7.Text = "Gestionar Gimnasio";
            this.iconMenuItem7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // iconMenuItem8
            // 
            this.iconMenuItem8.AutoSize = false;
            this.iconMenuItem8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.iconMenuItem8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.iconMenuItem8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.iconMenuItem8.ForeColor = System.Drawing.Color.White;
            this.iconMenuItem8.IconChar = FontAwesome.Sharp.IconChar.ScrewdriverWrench;
            this.iconMenuItem8.IconColor = System.Drawing.Color.White;
            this.iconMenuItem8.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem8.IconSize = 50;
            this.iconMenuItem8.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItem8.Margin = new System.Windows.Forms.Padding(20);
            this.iconMenuItem8.Name = "iconMenuItem8";
            this.iconMenuItem8.Size = new System.Drawing.Size(160, 80);
            this.iconMenuItem8.Text = "Ver Socios";
            this.iconMenuItem8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // iconMenuItem9
            // 
            this.iconMenuItem9.AutoSize = false;
            this.iconMenuItem9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.iconMenuItem9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.iconMenuItem9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.iconMenuItem9.ForeColor = System.Drawing.Color.White;
            this.iconMenuItem9.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.iconMenuItem9.IconColor = System.Drawing.Color.White;
            this.iconMenuItem9.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem9.IconSize = 50;
            this.iconMenuItem9.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItem9.Margin = new System.Windows.Forms.Padding(20);
            this.iconMenuItem9.Name = "iconMenuItem9";
            this.iconMenuItem9.Size = new System.Drawing.Size(170, 80);
            this.iconMenuItem9.Text = "Gestionar Gimnasio";
            this.iconMenuItem9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // iconMenuItem10
            // 
            this.iconMenuItem10.AutoSize = false;
            this.iconMenuItem10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.iconMenuItem10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.iconMenuItem10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.iconMenuItem10.ForeColor = System.Drawing.Color.White;
            this.iconMenuItem10.IconChar = FontAwesome.Sharp.IconChar.ScrewdriverWrench;
            this.iconMenuItem10.IconColor = System.Drawing.Color.White;
            this.iconMenuItem10.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem10.IconSize = 50;
            this.iconMenuItem10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItem10.Margin = new System.Windows.Forms.Padding(20);
            this.iconMenuItem10.Name = "iconMenuItem10";
            this.iconMenuItem10.Size = new System.Drawing.Size(160, 80);
            this.iconMenuItem10.Text = "Ver Socios";
            this.iconMenuItem10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // iconMenuItem11
            // 
            this.iconMenuItem11.AutoSize = false;
            this.iconMenuItem11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.iconMenuItem11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.iconMenuItem11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.iconMenuItem11.ForeColor = System.Drawing.Color.White;
            this.iconMenuItem11.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.iconMenuItem11.IconColor = System.Drawing.Color.White;
            this.iconMenuItem11.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem11.IconSize = 50;
            this.iconMenuItem11.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItem11.Margin = new System.Windows.Forms.Padding(20);
            this.iconMenuItem11.Name = "iconMenuItem11";
            this.iconMenuItem11.Size = new System.Drawing.Size(160, 80);
            this.iconMenuItem11.Text = "Gestionar Gimnasio";
            this.iconMenuItem11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnValidar
            // 
            this.btnValidar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.btnValidar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnValidar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnValidar.FlatAppearance.BorderSize = 3;
            this.btnValidar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValidar.IconChar = FontAwesome.Sharp.IconChar.UserCheck;
            this.btnValidar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnValidar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnValidar.IconSize = 130;
            this.btnValidar.Location = new System.Drawing.Point(813, 21);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Padding = new System.Windows.Forms.Padding(25, 5, 0, 0);
            this.btnValidar.Size = new System.Drawing.Size(166, 123);
            this.btnValidar.TabIndex = 10;
            this.btnValidar.UseVisualStyleBackColor = false;
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // picLogoInicio
            // 
            this.picLogoInicio.BackColor = System.Drawing.Color.Black;
            this.picLogoInicio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picLogoInicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogoInicio.Location = new System.Drawing.Point(22, 21);
            this.picLogoInicio.Name = "picLogoInicio";
            this.picLogoInicio.Size = new System.Drawing.Size(180, 88);
            this.picLogoInicio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogoInicio.TabIndex = 14;
            this.picLogoInicio.TabStop = false;
            this.picLogoInicio.Click += new System.EventHandler(this.picLogo_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.IconChar = FontAwesome.Sharp.IconChar.SignInAlt;
            this.btnSalir.IconColor = System.Drawing.Color.White;
            this.btnSalir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSalir.IconSize = 60;
            this.btnSalir.Location = new System.Drawing.Point(1690, 20);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btnSalir.Size = new System.Drawing.Size(80, 60);
            this.btnSalir.TabIndex = 13;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // iconSplitButton1
            // 
            this.iconSplitButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconSplitButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconSplitButton1.IconColor = System.Drawing.Color.Black;
            this.iconSplitButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconSplitButton1.IconSize = 48;
            this.iconSplitButton1.Name = "iconSplitButton1";
            this.iconSplitButton1.Rotation = 0D;
            this.iconSplitButton1.Size = new System.Drawing.Size(23, 23);
            this.iconSplitButton1.Text = "iconSplitButton1";
            // 
            // iconSplitButton2
            // 
            this.iconSplitButton2.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconSplitButton2.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconSplitButton2.IconColor = System.Drawing.Color.Black;
            this.iconSplitButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconSplitButton2.IconSize = 48;
            this.iconSplitButton2.Name = "iconSplitButton2";
            this.iconSplitButton2.Rotation = 0D;
            this.iconSplitButton2.Size = new System.Drawing.Size(23, 23);
            this.iconSplitButton2.Text = "iconSplitButton2";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1346, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Rol:";
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = true;
            this.lblRol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.lblRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRol.ForeColor = System.Drawing.Color.White;
            this.lblRol.Location = new System.Drawing.Point(1393, 55);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(48, 20);
            this.lblRol.TabIndex = 17;
            this.lblRol.Text = "lblRol";
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1796, 934);
            this.Controls.Add(this.lblRol);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.botonesTop);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.btnValidar);
            this.Controls.Add(this.picLogoInicio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.menuTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.contenedor.ResumeLayout(false);
            this.panelPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.botones.ResumeLayout(false);
            this.botones.PerformLayout();
            this.botonesTop.ResumeLayout(false);
            this.botonesTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoInicio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnSalir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel contenedor;
        private System.Windows.Forms.MenuStrip menuTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.PictureBox picLogoInicio;
        private FontAwesome.Sharp.IconButton btnValidar;
        private FontAwesome.Sharp.IconSplitButton iconSplitButton1;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.MenuStrip botones;
        private FontAwesome.Sharp.IconMenuItem menuGestionarRutinas;
        private FontAwesome.Sharp.IconMenuItem menuSocios;
        private FontAwesome.Sharp.IconMenuItem menuGestionarGimnasio;
        private System.Windows.Forms.MenuStrip botonesTop;
        private FontAwesome.Sharp.IconMenuItem menuTopGestionarRutinas;
        private FontAwesome.Sharp.IconMenuItem menuTopSocios;
        private FontAwesome.Sharp.IconMenuItem menuTopGestionarGimnasio;
        private FontAwesome.Sharp.IconSplitButton iconSplitButton2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.Label lblHoraActual;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTurno;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaTurno;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRangoHorario;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoraDesde;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoraHasta;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoIngreso;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreEntrenador;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn CupoActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn CupoMaximo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoTurno;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem6;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem7;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem8;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem9;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem10;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRol;
    }
}