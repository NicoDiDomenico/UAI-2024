namespace Vista
{
    partial class frmHistorialRutinas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label10 = new System.Windows.Forms.Label();
            this.tlpEstiramiento = new System.Windows.Forms.TableLayoutPanel();
            this.gbEstiramiento = new System.Windows.Forms.GroupBox();
            this.panelEstiramiento = new System.Windows.Forms.Panel();
            this.tlpEntrenamiento = new System.Windows.Forms.TableLayoutPanel();
            this.panelEntrenamiento = new System.Windows.Forms.Panel();
            this.msjRutina = new System.Windows.Forms.Label();
            this.gbEntrenamiento = new System.Windows.Forms.GroupBox();
            this.tlpCalentamiento = new System.Windows.Forms.TableLayoutPanel();
            this.panelCalentamiento = new System.Windows.Forms.Panel();
            this.gbCalentamiento = new System.Windows.Forms.GroupBox();
            this.dgvPanel = new System.Windows.Forms.DataGridView();
            this.panelDias = new System.Windows.Forms.Panel();
            this.btnMartes = new FontAwesome.Sharp.IconButton();
            this.btnSabado = new FontAwesome.Sharp.IconButton();
            this.btnViernes = new FontAwesome.Sharp.IconButton();
            this.btnJueves = new FontAwesome.Sharp.IconButton();
            this.btnMiercoles = new FontAwesome.Sharp.IconButton();
            this.btnLunes = new FontAwesome.Sharp.IconButton();
            this.Seleccionado2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NombreSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSeleccionar2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Seleccionado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NombreYApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSeleccionar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panelRutinas = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnRestaurar = new FontAwesome.Sharp.IconButton();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.msjDgvData = new System.Windows.Forms.Label();
            this.btnSeleccionar3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdHistorial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdSocioSeleccionado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seleccionado3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.gbEstiramiento.SuspendLayout();
            this.panelEstiramiento.SuspendLayout();
            this.panelEntrenamiento.SuspendLayout();
            this.gbEntrenamiento.SuspendLayout();
            this.panelCalentamiento.SuspendLayout();
            this.gbCalentamiento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPanel)).BeginInit();
            this.panelDias.SuspendLayout();
            this.panelRutinas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label10.Location = new System.Drawing.Point(570, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1218, 727);
            this.label10.TabIndex = 60;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpEstiramiento
            // 
            this.tlpEstiramiento.AutoSize = true;
            this.tlpEstiramiento.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpEstiramiento.ColumnCount = 3;
            this.tlpEstiramiento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpEstiramiento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpEstiramiento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpEstiramiento.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpEstiramiento.Location = new System.Drawing.Point(0, 0);
            this.tlpEstiramiento.Name = "tlpEstiramiento";
            this.tlpEstiramiento.RowCount = 1;
            this.tlpEstiramiento.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEstiramiento.Size = new System.Drawing.Size(1162, 0);
            this.tlpEstiramiento.TabIndex = 3;
            // 
            // gbEstiramiento
            // 
            this.gbEstiramiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.gbEstiramiento.Controls.Add(this.panelEstiramiento);
            this.gbEstiramiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbEstiramiento.ForeColor = System.Drawing.Color.White;
            this.gbEstiramiento.Location = new System.Drawing.Point(595, 445);
            this.gbEstiramiento.Name = "gbEstiramiento";
            this.gbEstiramiento.Size = new System.Drawing.Size(1168, 190);
            this.gbEstiramiento.TabIndex = 111;
            this.gbEstiramiento.TabStop = false;
            this.gbEstiramiento.Text = "Estiramiento";
            // 
            // panelEstiramiento
            // 
            this.panelEstiramiento.AutoScroll = true;
            this.panelEstiramiento.Controls.Add(this.tlpEstiramiento);
            this.panelEstiramiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEstiramiento.Location = new System.Drawing.Point(3, 18);
            this.panelEstiramiento.Name = "panelEstiramiento";
            this.panelEstiramiento.Size = new System.Drawing.Size(1162, 169);
            this.panelEstiramiento.TabIndex = 4;
            // 
            // tlpEntrenamiento
            // 
            this.tlpEntrenamiento.AutoSize = true;
            this.tlpEntrenamiento.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpEntrenamiento.ColumnCount = 5;
            this.tlpEntrenamiento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpEntrenamiento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpEntrenamiento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpEntrenamiento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpEntrenamiento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpEntrenamiento.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpEntrenamiento.Location = new System.Drawing.Point(0, 0);
            this.tlpEntrenamiento.Name = "tlpEntrenamiento";
            this.tlpEntrenamiento.RowCount = 1;
            this.tlpEntrenamiento.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEntrenamiento.Size = new System.Drawing.Size(1162, 0);
            this.tlpEntrenamiento.TabIndex = 3;
            // 
            // panelEntrenamiento
            // 
            this.panelEntrenamiento.AutoScroll = true;
            this.panelEntrenamiento.Controls.Add(this.msjRutina);
            this.panelEntrenamiento.Controls.Add(this.tlpEntrenamiento);
            this.panelEntrenamiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEntrenamiento.Location = new System.Drawing.Point(3, 18);
            this.panelEntrenamiento.Name = "panelEntrenamiento";
            this.panelEntrenamiento.Size = new System.Drawing.Size(1162, 169);
            this.panelEntrenamiento.TabIndex = 4;
            // 
            // msjRutina
            // 
            this.msjRutina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.msjRutina.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msjRutina.ForeColor = System.Drawing.Color.White;
            this.msjRutina.Location = new System.Drawing.Point(434, 42);
            this.msjRutina.Name = "msjRutina";
            this.msjRutina.Size = new System.Drawing.Size(262, 83);
            this.msjRutina.TabIndex = 109;
            this.msjRutina.Text = "Seleccione una fecha ";
            this.msjRutina.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.msjRutina.Visible = false;
            // 
            // gbEntrenamiento
            // 
            this.gbEntrenamiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.gbEntrenamiento.Controls.Add(this.panelEntrenamiento);
            this.gbEntrenamiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbEntrenamiento.ForeColor = System.Drawing.Color.White;
            this.gbEntrenamiento.Location = new System.Drawing.Point(595, 255);
            this.gbEntrenamiento.Name = "gbEntrenamiento";
            this.gbEntrenamiento.Size = new System.Drawing.Size(1168, 190);
            this.gbEntrenamiento.TabIndex = 109;
            this.gbEntrenamiento.TabStop = false;
            this.gbEntrenamiento.Text = "Entrenamiento";
            // 
            // tlpCalentamiento
            // 
            this.tlpCalentamiento.AutoSize = true;
            this.tlpCalentamiento.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpCalentamiento.ColumnCount = 3;
            this.tlpCalentamiento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpCalentamiento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpCalentamiento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpCalentamiento.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpCalentamiento.Location = new System.Drawing.Point(0, 0);
            this.tlpCalentamiento.Name = "tlpCalentamiento";
            this.tlpCalentamiento.RowCount = 1;
            this.tlpCalentamiento.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCalentamiento.Size = new System.Drawing.Size(1162, 0);
            this.tlpCalentamiento.TabIndex = 3;
            // 
            // panelCalentamiento
            // 
            this.panelCalentamiento.AutoScroll = true;
            this.panelCalentamiento.Controls.Add(this.tlpCalentamiento);
            this.panelCalentamiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCalentamiento.Location = new System.Drawing.Point(3, 18);
            this.panelCalentamiento.Name = "panelCalentamiento";
            this.panelCalentamiento.Size = new System.Drawing.Size(1162, 169);
            this.panelCalentamiento.TabIndex = 3;
            // 
            // gbCalentamiento
            // 
            this.gbCalentamiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.gbCalentamiento.Controls.Add(this.panelCalentamiento);
            this.gbCalentamiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCalentamiento.ForeColor = System.Drawing.Color.White;
            this.gbCalentamiento.Location = new System.Drawing.Point(595, 65);
            this.gbCalentamiento.Name = "gbCalentamiento";
            this.gbCalentamiento.Size = new System.Drawing.Size(1168, 190);
            this.gbCalentamiento.TabIndex = 108;
            this.gbCalentamiento.TabStop = false;
            this.gbCalentamiento.Text = "Calentamiento";
            // 
            // dgvPanel
            // 
            this.dgvPanel.AllowUserToAddRows = false;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.White;
            this.dgvPanel.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle33;
            this.dgvPanel.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvPanel.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle34.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle34.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPanel.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle34;
            this.dgvPanel.ColumnHeadersHeight = 50;
            this.dgvPanel.ColumnHeadersVisible = false;
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPanel.DefaultCellStyle = dataGridViewCellStyle35;
            this.dgvPanel.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvPanel.Location = new System.Drawing.Point(583, 61);
            this.dgvPanel.MultiSelect = false;
            this.dgvPanel.Name = "dgvPanel";
            this.dgvPanel.ReadOnly = true;
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle36.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle36.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle36.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPanel.RowHeadersDefaultCellStyle = dataGridViewCellStyle36;
            this.dgvPanel.RowHeadersVisible = false;
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle37.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle37.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle37.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle37.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvPanel.RowsDefaultCellStyle = dataGridViewCellStyle37;
            this.dgvPanel.RowTemplate.Height = 35;
            this.dgvPanel.Size = new System.Drawing.Size(1193, 654);
            this.dgvPanel.TabIndex = 107;
            // 
            // panelDias
            // 
            this.panelDias.BackColor = System.Drawing.Color.Black;
            this.panelDias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDias.Controls.Add(this.btnMartes);
            this.panelDias.Controls.Add(this.btnSabado);
            this.panelDias.Controls.Add(this.btnViernes);
            this.panelDias.Controls.Add(this.btnJueves);
            this.panelDias.Controls.Add(this.btnMiercoles);
            this.panelDias.Controls.Add(this.btnLunes);
            this.panelDias.Location = new System.Drawing.Point(0, 0);
            this.panelDias.Name = "panelDias";
            this.panelDias.Size = new System.Drawing.Size(1195, 52);
            this.panelDias.TabIndex = 100;
            // 
            // btnMartes
            // 
            this.btnMartes.BackColor = System.Drawing.Color.White;
            this.btnMartes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMartes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnMartes.ForeColor = System.Drawing.Color.Black;
            this.btnMartes.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnMartes.IconColor = System.Drawing.Color.Black;
            this.btnMartes.IconFont = FontAwesome.Sharp.IconFont.Regular;
            this.btnMartes.Location = new System.Drawing.Point(199, 0);
            this.btnMartes.Margin = new System.Windows.Forms.Padding(0);
            this.btnMartes.Name = "btnMartes";
            this.btnMartes.Size = new System.Drawing.Size(198, 50);
            this.btnMartes.TabIndex = 1;
            this.btnMartes.Text = "Martes";
            this.btnMartes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMartes.UseVisualStyleBackColor = false;
            this.btnMartes.Click += new System.EventHandler(this.btnMartes_Click);
            // 
            // btnSabado
            // 
            this.btnSabado.BackColor = System.Drawing.Color.White;
            this.btnSabado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSabado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSabado.ForeColor = System.Drawing.Color.Black;
            this.btnSabado.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnSabado.IconColor = System.Drawing.Color.Black;
            this.btnSabado.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSabado.Location = new System.Drawing.Point(993, 0);
            this.btnSabado.Name = "btnSabado";
            this.btnSabado.Size = new System.Drawing.Size(200, 50);
            this.btnSabado.TabIndex = 5;
            this.btnSabado.Text = "Sábado";
            this.btnSabado.UseVisualStyleBackColor = false;
            this.btnSabado.Click += new System.EventHandler(this.btnSabado_Click);
            // 
            // btnViernes
            // 
            this.btnViernes.BackColor = System.Drawing.Color.White;
            this.btnViernes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViernes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnViernes.ForeColor = System.Drawing.Color.Black;
            this.btnViernes.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnViernes.IconColor = System.Drawing.Color.Black;
            this.btnViernes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnViernes.Location = new System.Drawing.Point(794, 0);
            this.btnViernes.Name = "btnViernes";
            this.btnViernes.Size = new System.Drawing.Size(200, 50);
            this.btnViernes.TabIndex = 4;
            this.btnViernes.Text = "Viernes";
            this.btnViernes.UseVisualStyleBackColor = false;
            this.btnViernes.Click += new System.EventHandler(this.btnViernes_Click);
            // 
            // btnJueves
            // 
            this.btnJueves.BackColor = System.Drawing.Color.White;
            this.btnJueves.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJueves.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnJueves.ForeColor = System.Drawing.Color.Black;
            this.btnJueves.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnJueves.IconColor = System.Drawing.Color.Black;
            this.btnJueves.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnJueves.Location = new System.Drawing.Point(595, 0);
            this.btnJueves.Name = "btnJueves";
            this.btnJueves.Size = new System.Drawing.Size(200, 50);
            this.btnJueves.TabIndex = 3;
            this.btnJueves.Text = "Jueves";
            this.btnJueves.UseVisualStyleBackColor = false;
            this.btnJueves.Click += new System.EventHandler(this.btnJueves_Click);
            // 
            // btnMiercoles
            // 
            this.btnMiercoles.BackColor = System.Drawing.Color.White;
            this.btnMiercoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMiercoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnMiercoles.ForeColor = System.Drawing.Color.Black;
            this.btnMiercoles.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnMiercoles.IconColor = System.Drawing.Color.Black;
            this.btnMiercoles.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMiercoles.Location = new System.Drawing.Point(396, 0);
            this.btnMiercoles.Name = "btnMiercoles";
            this.btnMiercoles.Size = new System.Drawing.Size(200, 50);
            this.btnMiercoles.TabIndex = 2;
            this.btnMiercoles.Text = "Miércoles";
            this.btnMiercoles.UseVisualStyleBackColor = false;
            this.btnMiercoles.Click += new System.EventHandler(this.btnMiercoles_Click);
            // 
            // btnLunes
            // 
            this.btnLunes.BackColor = System.Drawing.Color.White;
            this.btnLunes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLunes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnLunes.ForeColor = System.Drawing.Color.Black;
            this.btnLunes.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnLunes.IconColor = System.Drawing.Color.Black;
            this.btnLunes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLunes.Location = new System.Drawing.Point(0, 0);
            this.btnLunes.Margin = new System.Windows.Forms.Padding(0);
            this.btnLunes.Name = "btnLunes";
            this.btnLunes.Size = new System.Drawing.Size(200, 50);
            this.btnLunes.TabIndex = 0;
            this.btnLunes.Text = "Lunes";
            this.btnLunes.UseVisualStyleBackColor = false;
            this.btnLunes.Click += new System.EventHandler(this.btnLunes_Click);
            // 
            // Seleccionado2
            // 
            this.Seleccionado2.HeaderText = "Seleccionado";
            this.Seleccionado2.Name = "Seleccionado2";
            this.Seleccionado2.ReadOnly = true;
            this.Seleccionado2.Visible = false;
            // 
            // NombreSocio
            // 
            this.NombreSocio.HeaderText = "Socio";
            this.NombreSocio.Name = "NombreSocio";
            this.NombreSocio.ReadOnly = true;
            this.NombreSocio.Width = 243;
            // 
            // IdSocio
            // 
            this.IdSocio.HeaderText = "IdSocio";
            this.IdSocio.Name = "IdSocio";
            this.IdSocio.ReadOnly = true;
            this.IdSocio.Visible = false;
            // 
            // btnSeleccionar2
            // 
            this.btnSeleccionar2.HeaderText = "";
            this.btnSeleccionar2.Name = "btnSeleccionar2";
            this.btnSeleccionar2.ReadOnly = true;
            this.btnSeleccionar2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnSeleccionar2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnSeleccionar2.Width = 35;
            // 
            // Seleccionado
            // 
            this.Seleccionado.HeaderText = "Seleccionado";
            this.Seleccionado.Name = "Seleccionado";
            this.Seleccionado.ReadOnly = true;
            this.Seleccionado.Visible = false;
            // 
            // NombreYApellido
            // 
            this.NombreYApellido.HeaderText = "Entrenador";
            this.NombreYApellido.Name = "NombreYApellido";
            this.NombreYApellido.ReadOnly = true;
            this.NombreYApellido.Width = 243;
            // 
            // Id
            // 
            this.Id.HeaderText = "IdUsuario";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.HeaderText = "";
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.ReadOnly = true;
            this.btnSeleccionar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnSeleccionar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnSeleccionar.Width = 35;
            // 
            // panelRutinas
            // 
            this.panelRutinas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.panelRutinas.Controls.Add(this.panelDias);
            this.panelRutinas.Location = new System.Drawing.Point(582, 11);
            this.panelRutinas.Name = "panelRutinas";
            this.panelRutinas.Size = new System.Drawing.Size(1196, 704);
            this.panelRutinas.TabIndex = 106;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.dgvData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle38.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle38.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle38.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle38.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle38;
            this.dgvData.ColumnHeadersHeight = 50;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnSeleccionar3,
            this.Fecha,
            this.IdHistorial,
            this.IdSocioSeleccionado,
            this.Dia,
            this.Seleccionado3});
            this.dgvData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvData.Location = new System.Drawing.Point(14, 12);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle39.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle39.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle39.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle39;
            this.dgvData.RowHeadersVisible = false;
            dataGridViewCellStyle40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle40.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle40.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle40;
            this.dgvData.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvData.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvData.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvData.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvData.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.Height = 35;
            this.dgvData.Size = new System.Drawing.Size(541, 703);
            this.dgvData.TabIndex = 113;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvData_CellPainting);
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.BackColor = System.Drawing.Color.Teal;
            this.btnRestaurar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRestaurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestaurar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRestaurar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestaurar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestaurar.ForeColor = System.Drawing.Color.White;
            this.btnRestaurar.IconChar = FontAwesome.Sharp.IconChar.Upload;
            this.btnRestaurar.IconColor = System.Drawing.Color.White;
            this.btnRestaurar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRestaurar.IconSize = 24;
            this.btnRestaurar.Location = new System.Drawing.Point(475, 15);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(165, 50);
            this.btnRestaurar.TabIndex = 47;
            this.btnRestaurar.Text = "Restarurar";
            this.btnRestaurar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRestaurar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestaurar.UseVisualStyleBackColor = false;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.panelBotones.Controls.Add(this.btnRestaurar);
            this.panelBotones.Location = new System.Drawing.Point(595, 635);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1168, 78);
            this.panelBotones.TabIndex = 110;
            // 
            // msjDgvData
            // 
            this.msjDgvData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.msjDgvData.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msjDgvData.ForeColor = System.Drawing.Color.Black;
            this.msjDgvData.Location = new System.Drawing.Point(137, 315);
            this.msjDgvData.Name = "msjDgvData";
            this.msjDgvData.Size = new System.Drawing.Size(298, 83);
            this.msjDgvData.TabIndex = 114;
            this.msjDgvData.Text = "No se han registrado rutinas para este dia";
            this.msjDgvData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.msjDgvData.Visible = false;
            // 
            // btnSeleccionar3
            // 
            this.btnSeleccionar3.HeaderText = "";
            this.btnSeleccionar3.Name = "btnSeleccionar3";
            this.btnSeleccionar3.ReadOnly = true;
            this.btnSeleccionar3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnSeleccionar3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnSeleccionar3.Width = 35;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha y Hora";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 304;
            // 
            // IdHistorial
            // 
            this.IdHistorial.HeaderText = "IdHistorial";
            this.IdHistorial.Name = "IdHistorial";
            this.IdHistorial.ReadOnly = true;
            this.IdHistorial.Visible = false;
            // 
            // IdSocioSeleccionado
            // 
            this.IdSocioSeleccionado.HeaderText = "Socio";
            this.IdSocioSeleccionado.Name = "IdSocioSeleccionado";
            this.IdSocioSeleccionado.ReadOnly = true;
            this.IdSocioSeleccionado.Visible = false;
            // 
            // Dia
            // 
            this.Dia.HeaderText = "Dia";
            this.Dia.Name = "Dia";
            this.Dia.ReadOnly = true;
            this.Dia.Width = 200;
            // 
            // Seleccionado3
            // 
            this.Seleccionado3.HeaderText = "Seleccionado";
            this.Seleccionado3.Name = "Seleccionado3";
            this.Seleccionado3.ReadOnly = true;
            this.Seleccionado3.Visible = false;
            // 
            // frmHistorialRutinas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.ClientSize = new System.Drawing.Size(1788, 727);
            this.Controls.Add(this.msjDgvData);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.gbEstiramiento);
            this.Controls.Add(this.gbEntrenamiento);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.gbCalentamiento);
            this.Controls.Add(this.dgvPanel);
            this.Controls.Add(this.panelRutinas);
            this.Controls.Add(this.label10);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHistorialRutinas";
            this.Text = "frmHistorialRutinas";
            this.Load += new System.EventHandler(this.frmHistorialRutinas_Load);
            this.gbEstiramiento.ResumeLayout(false);
            this.panelEstiramiento.ResumeLayout(false);
            this.panelEstiramiento.PerformLayout();
            this.panelEntrenamiento.ResumeLayout(false);
            this.panelEntrenamiento.PerformLayout();
            this.gbEntrenamiento.ResumeLayout(false);
            this.panelCalentamiento.ResumeLayout(false);
            this.panelCalentamiento.PerformLayout();
            this.gbCalentamiento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPanel)).EndInit();
            this.panelDias.ResumeLayout(false);
            this.panelRutinas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tlpEstiramiento;
        private System.Windows.Forms.GroupBox gbEstiramiento;
        private System.Windows.Forms.Panel panelEstiramiento;
        private System.Windows.Forms.TableLayoutPanel tlpEntrenamiento;
        private System.Windows.Forms.Panel panelEntrenamiento;
        private System.Windows.Forms.Label msjRutina;
        private System.Windows.Forms.GroupBox gbEntrenamiento;
        private System.Windows.Forms.TableLayoutPanel tlpCalentamiento;
        private System.Windows.Forms.Panel panelCalentamiento;
        private System.Windows.Forms.GroupBox gbCalentamiento;
        private System.Windows.Forms.DataGridView dgvPanel;
        private System.Windows.Forms.Panel panelDias;
        private FontAwesome.Sharp.IconButton btnMartes;
        private FontAwesome.Sharp.IconButton btnSabado;
        private FontAwesome.Sharp.IconButton btnViernes;
        private FontAwesome.Sharp.IconButton btnJueves;
        private FontAwesome.Sharp.IconButton btnMiercoles;
        private FontAwesome.Sharp.IconButton btnLunes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionado2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdSocio;
        private System.Windows.Forms.DataGridViewButtonColumn btnSeleccionar2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionado;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreYApellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewButtonColumn btnSeleccionar;
        private System.Windows.Forms.Panel panelRutinas;
        private System.Windows.Forms.DataGridView dgvData;
        private FontAwesome.Sharp.IconButton btnRestaurar;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Label msjDgvData;
        private System.Windows.Forms.DataGridViewButtonColumn btnSeleccionar3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdHistorial;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdSocioSeleccionado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dia;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionado3;
    }
}