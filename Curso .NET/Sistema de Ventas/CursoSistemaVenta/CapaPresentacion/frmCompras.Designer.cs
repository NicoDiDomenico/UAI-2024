namespace CapaPresentacion
{
    partial class frmCompras
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.cboTipoDocumento = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDocProveedor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIdProveedor = new System.Windows.Forms.TextBox();
            this.btnBuscarProveedor = new FontAwesome.Sharp.IconButton();
            this.txtNombreProveedor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPrecioVenta = new System.Windows.Forms.TextBox();
            this.txtPrecioCompra = new System.Windows.Forms.TextBox();
            this.txtProducto = new System.Windows.Forms.TextBox();
            this.txtIdProducto = new System.Windows.Forms.TextBox();
            this.btnBuscarProducto = new FontAwesome.Sharp.IconButton();
            this.txtCodProducto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.IdProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.txtTotalPagar = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnRegistrar = new FontAwesome.Sharp.IconButton();
            this.btnAgreagar = new FontAwesome.Sharp.IconButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(28, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(998, 515);
            this.label10.TabIndex = 21;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(35, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(170, 25);
            this.label9.TabIndex = 22;
            this.label9.Text = "Registrar Compra:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(206, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Tipo Documento:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(23, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Fecha:";
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(26, 44);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(157, 20);
            this.txtFecha.TabIndex = 26;
            // 
            // cboTipoDocumento
            // 
            this.cboTipoDocumento.FormattingEnabled = true;
            this.cboTipoDocumento.Location = new System.Drawing.Point(209, 43);
            this.cboTipoDocumento.Name = "cboTipoDocumento";
            this.cboTipoDocumento.Size = new System.Drawing.Size(217, 21);
            this.cboTipoDocumento.TabIndex = 27;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cboTipoDocumento);
            this.groupBox1.Controls.Add(this.txtFecha);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(63, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 85);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información Compra";
            // 
            // txtDocProveedor
            // 
            this.txtDocProveedor.Location = new System.Drawing.Point(26, 44);
            this.txtDocProveedor.Name = "txtDocProveedor";
            this.txtDocProveedor.Size = new System.Drawing.Size(157, 20);
            this.txtDocProveedor.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(244, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Razón Social:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.txtIdProveedor);
            this.groupBox2.Controls.Add(this.btnBuscarProveedor);
            this.groupBox2.Controls.Add(this.txtNombreProveedor);
            this.groupBox2.Controls.Add(this.txtDocProveedor);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(527, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 85);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información Proveedor";
            // 
            // txtIdProveedor
            // 
            this.txtIdProveedor.Location = new System.Drawing.Point(408, 18);
            this.txtIdProveedor.Name = "txtIdProveedor";
            this.txtIdProveedor.Size = new System.Drawing.Size(28, 20);
            this.txtIdProveedor.TabIndex = 30;
            this.txtIdProveedor.Text = "0";
            this.txtIdProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnBuscarProveedor
            // 
            this.btnBuscarProveedor.BackColor = System.Drawing.Color.White;
            this.btnBuscarProveedor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscarProveedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarProveedor.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBuscarProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarProveedor.ForeColor = System.Drawing.Color.White;
            this.btnBuscarProveedor.IconChar = FontAwesome.Sharp.IconChar.Searchengin;
            this.btnBuscarProveedor.IconColor = System.Drawing.Color.Black;
            this.btnBuscarProveedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarProveedor.IconSize = 20;
            this.btnBuscarProveedor.Location = new System.Drawing.Point(189, 44);
            this.btnBuscarProveedor.Name = "btnBuscarProveedor";
            this.btnBuscarProveedor.Size = new System.Drawing.Size(40, 20);
            this.btnBuscarProveedor.TabIndex = 29;
            this.btnBuscarProveedor.UseVisualStyleBackColor = false;
            this.btnBuscarProveedor.Click += new System.EventHandler(this.btnBuscarProveedor_Click);
            // 
            // txtNombreProveedor
            // 
            this.txtNombreProveedor.Location = new System.Drawing.Point(247, 44);
            this.txtNombreProveedor.Name = "txtNombreProveedor";
            this.txtNombreProveedor.Size = new System.Drawing.Size(189, 20);
            this.txtNombreProveedor.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(23, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Nro Documento:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.nudCantidad);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtPrecioVenta);
            this.groupBox3.Controls.Add(this.txtPrecioCompra);
            this.groupBox3.Controls.Add(this.txtProducto);
            this.groupBox3.Controls.Add(this.txtIdProducto);
            this.groupBox3.Controls.Add(this.btnBuscarProducto);
            this.groupBox3.Controls.Add(this.txtCodProducto);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(63, 136);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(763, 85);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Información Producto";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(613, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 38;
            this.label11.Text = "Cantidad";
            // 
            // nudCantidad
            // 
            this.nudCantidad.Location = new System.Drawing.Point(616, 44);
            this.nudCantidad.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(118, 20);
            this.nudCantidad.TabIndex = 37;
            this.nudCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(501, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 36;
            this.label8.Text = "Precio Venta:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(380, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Precio Compra:";
            // 
            // txtPrecioVenta
            // 
            this.txtPrecioVenta.Location = new System.Drawing.Point(504, 44);
            this.txtPrecioVenta.Name = "txtPrecioVenta";
            this.txtPrecioVenta.Size = new System.Drawing.Size(106, 20);
            this.txtPrecioVenta.TabIndex = 34;
            // 
            // txtPrecioCompra
            // 
            this.txtPrecioCompra.Location = new System.Drawing.Point(383, 45);
            this.txtPrecioCompra.Name = "txtPrecioCompra";
            this.txtPrecioCompra.Size = new System.Drawing.Size(108, 20);
            this.txtPrecioCompra.TabIndex = 33;
            // 
            // txtProducto
            // 
            this.txtProducto.Location = new System.Drawing.Point(209, 44);
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.Size = new System.Drawing.Size(160, 20);
            this.txtProducto.TabIndex = 32;
            // 
            // txtIdProducto
            // 
            this.txtIdProducto.Location = new System.Drawing.Point(125, 18);
            this.txtIdProducto.Name = "txtIdProducto";
            this.txtIdProducto.Size = new System.Drawing.Size(28, 20);
            this.txtIdProducto.TabIndex = 31;
            this.txtIdProducto.Text = "0";
            this.txtIdProducto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnBuscarProducto
            // 
            this.btnBuscarProducto.BackColor = System.Drawing.Color.White;
            this.btnBuscarProducto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscarProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarProducto.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBuscarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarProducto.ForeColor = System.Drawing.Color.White;
            this.btnBuscarProducto.IconChar = FontAwesome.Sharp.IconChar.Searchengin;
            this.btnBuscarProducto.IconColor = System.Drawing.Color.Black;
            this.btnBuscarProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarProducto.IconSize = 20;
            this.btnBuscarProducto.Location = new System.Drawing.Point(159, 43);
            this.btnBuscarProducto.Name = "btnBuscarProducto";
            this.btnBuscarProducto.Size = new System.Drawing.Size(40, 20);
            this.btnBuscarProducto.TabIndex = 31;
            this.btnBuscarProducto.UseVisualStyleBackColor = false;
            this.btnBuscarProducto.Click += new System.EventHandler(this.btnBuscarProducto_Click);
            // 
            // txtCodProducto
            // 
            this.txtCodProducto.Location = new System.Drawing.Point(26, 44);
            this.txtCodProducto.Name = "txtCodProducto";
            this.txtCodProducto.Size = new System.Drawing.Size(127, 20);
            this.txtCodProducto.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(23, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Código Producto:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(206, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Producto:";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdProducto,
            this.Producto,
            this.PrecioCompra,
            this.PrecioVenta,
            this.Cantidad,
            this.subTotal,
            this.btnEliminar});
            this.dgvData.Location = new System.Drawing.Point(63, 227);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(763, 285);
            this.dgvData.TabIndex = 29;
            // 
            // IdProducto
            // 
            this.IdProducto.HeaderText = "IdProducto";
            this.IdProducto.Name = "IdProducto";
            this.IdProducto.ReadOnly = true;
            this.IdProducto.Visible = false;
            // 
            // Producto
            // 
            this.Producto.HeaderText = "Producto";
            this.Producto.Name = "Producto";
            this.Producto.ReadOnly = true;
            this.Producto.Width = 150;
            // 
            // PrecioCompra
            // 
            this.PrecioCompra.HeaderText = "Precio Compra";
            this.PrecioCompra.Name = "PrecioCompra";
            this.PrecioCompra.ReadOnly = true;
            this.PrecioCompra.Width = 180;
            // 
            // PrecioVenta
            // 
            this.PrecioVenta.HeaderText = "Precio Venta";
            this.PrecioVenta.Name = "PrecioVenta";
            this.PrecioVenta.ReadOnly = true;
            this.PrecioVenta.Visible = false;
            this.PrecioVenta.Width = 150;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // subTotal
            // 
            this.subTotal.HeaderText = "Sub Total";
            this.subTotal.Name = "subTotal";
            this.subTotal.ReadOnly = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.HeaderText = "";
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.ReadOnly = true;
            this.btnEliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnEliminar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnEliminar.Width = 50;
            // 
            // txtTotalPagar
            // 
            this.txtTotalPagar.Location = new System.Drawing.Point(865, 447);
            this.txtTotalPagar.Name = "txtTotalPagar";
            this.txtTotalPagar.Size = new System.Drawing.Size(98, 20);
            this.txtTotalPagar.TabIndex = 39;
            this.txtTotalPagar.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(862, 431);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 13);
            this.label12.TabIndex = 39;
            this.label12.Text = "Total a Pagar:";
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.IconChar = FontAwesome.Sharp.IconChar.Paste;
            this.btnRegistrar.IconColor = System.Drawing.Color.SteelBlue;
            this.btnRegistrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRegistrar.IconSize = 36;
            this.btnRegistrar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistrar.Location = new System.Drawing.Point(865, 473);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(98, 39);
            this.btnRegistrar.TabIndex = 40;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistrar.UseVisualStyleBackColor = true;
            // 
            // btnAgreagar
            // 
            this.btnAgreagar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgreagar.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAgreagar.IconColor = System.Drawing.Color.ForestGreen;
            this.btnAgreagar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAgreagar.Location = new System.Drawing.Point(865, 141);
            this.btnAgreagar.Name = "btnAgreagar";
            this.btnAgreagar.Size = new System.Drawing.Size(98, 80);
            this.btnAgreagar.TabIndex = 30;
            this.btnAgreagar.Text = "Agregar";
            this.btnAgreagar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAgreagar.UseVisualStyleBackColor = true;
            // 
            // frmCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 535);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtTotalPagar);
            this.Controls.Add(this.btnAgreagar);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Name = "frmCompras";
            this.Text = "frmCompras";
            this.Load += new System.EventHandler(this.frmCompras_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.ComboBox cboTipoDocumento;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDocProveedor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNombreProveedor;
        private System.Windows.Forms.Label label4;
        private FontAwesome.Sharp.IconButton btnBuscarProveedor;
        private System.Windows.Forms.TextBox txtIdProveedor;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPrecioVenta;
        private System.Windows.Forms.TextBox txtPrecioCompra;
        private System.Windows.Forms.TextBox txtProducto;
        private System.Windows.Forms.TextBox txtIdProducto;
        private FontAwesome.Sharp.IconButton btnBuscarProducto;
        private System.Windows.Forms.TextBox txtCodProducto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn subTotal;
        private System.Windows.Forms.DataGridViewButtonColumn btnEliminar;
        private FontAwesome.Sharp.IconButton btnAgreagar;
        private System.Windows.Forms.TextBox txtTotalPagar;
        private System.Windows.Forms.Label label12;
        private FontAwesome.Sharp.IconButton btnRegistrar;
    }
}