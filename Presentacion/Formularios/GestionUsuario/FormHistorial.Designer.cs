namespace CentroAcopioApp.Presentacion.Formularios.GestionUsuario
{
    partial class FormHistorial
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dtpBuscarFecha = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBuscarUsuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtVigencia = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDetalleDescripcion = new System.Windows.Forms.TextBox();
            this.txtDetalleFecha = new System.Windows.Forms.TextBox();
            this.txtEntidadId = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDetalleEntidad = new System.Windows.Forms.TextBox();
            this.txtDetalleAccion = new System.Windows.Forms.TextBox();
            this.txtDetalleUsuario = new System.Windows.Forms.TextBox();
            this.txtDetalleId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(623, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Historial";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.dtpBuscarFecha);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtBuscarUsuario);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(59, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1241, 91);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar:";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(1038, 33);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(174, 44);
            this.btnLimpiar.TabIndex = 10;
            this.btnLimpiar.Text = "Limpiar busqueda";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(928, 33);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(91, 44);
            this.btnBuscar.TabIndex = 9;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // dtpBuscarFecha
            // 
            this.dtpBuscarFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBuscarFecha.Location = new System.Drawing.Point(498, 42);
            this.dtpBuscarFecha.Name = "dtpBuscarFecha";
            this.dtpBuscarFecha.Size = new System.Drawing.Size(130, 29);
            this.dtpBuscarFecha.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(423, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 24);
            this.label5.TabIndex = 7;
            this.label5.Text = "Fecha:";
            // 
            // txtBuscarUsuario
            // 
            this.txtBuscarUsuario.Location = new System.Drawing.Point(111, 43);
            this.txtBuscarUsuario.Name = "txtBuscarUsuario";
            this.txtBuscarUsuario.Size = new System.Drawing.Size(244, 29);
            this.txtBuscarUsuario.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Usuario:";
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Location = new System.Drawing.Point(59, 133);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.Size = new System.Drawing.Size(1241, 288);
            this.dgvHistorial.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtVigencia);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtDetalleDescripcion);
            this.groupBox2.Controls.Add(this.txtDetalleFecha);
            this.groupBox2.Controls.Add(this.txtEntidadId);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtDetalleEntidad);
            this.groupBox2.Controls.Add(this.txtDetalleAccion);
            this.groupBox2.Controls.Add(this.txtDetalleUsuario);
            this.groupBox2.Controls.Add(this.txtDetalleId);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(59, 440);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1241, 233);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles:";
            // 
            // txtVigencia
            // 
            this.txtVigencia.Location = new System.Drawing.Point(931, 49);
            this.txtVigencia.Name = "txtVigencia";
            this.txtVigencia.ReadOnly = true;
            this.txtVigencia.Size = new System.Drawing.Size(143, 29);
            this.txtVigencia.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(836, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 24);
            this.label11.TabIndex = 14;
            this.label11.Text = "Vigencia:";
            // 
            // txtDetalleDescripcion
            // 
            this.txtDetalleDescripcion.Location = new System.Drawing.Point(957, 104);
            this.txtDetalleDescripcion.Multiline = true;
            this.txtDetalleDescripcion.Name = "txtDetalleDescripcion";
            this.txtDetalleDescripcion.ReadOnly = true;
            this.txtDetalleDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDetalleDescripcion.Size = new System.Drawing.Size(255, 29);
            this.txtDetalleDescripcion.TabIndex = 13;
            // 
            // txtDetalleFecha
            // 
            this.txtDetalleFecha.Location = new System.Drawing.Point(507, 135);
            this.txtDetalleFecha.Name = "txtDetalleFecha";
            this.txtDetalleFecha.ReadOnly = true;
            this.txtDetalleFecha.Size = new System.Drawing.Size(173, 29);
            this.txtDetalleFecha.TabIndex = 12;
            // 
            // txtEntidadId
            // 
            this.txtEntidadId.Location = new System.Drawing.Point(539, 90);
            this.txtEntidadId.Name = "txtEntidadId";
            this.txtEntidadId.ReadOnly = true;
            this.txtEntidadId.Size = new System.Drawing.Size(141, 29);
            this.txtEntidadId.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(836, 107);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 24);
            this.label10.TabIndex = 10;
            this.label10.Text = "Descripción:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(432, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 24);
            this.label9.TabIndex = 9;
            this.label9.Text = "Fecha:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(432, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 24);
            this.label8.TabIndex = 8;
            this.label8.Text = "Entidad ID:";
            // 
            // txtDetalleEntidad
            // 
            this.txtDetalleEntidad.Location = new System.Drawing.Point(517, 52);
            this.txtDetalleEntidad.Name = "txtDetalleEntidad";
            this.txtDetalleEntidad.ReadOnly = true;
            this.txtDetalleEntidad.Size = new System.Drawing.Size(251, 29);
            this.txtDetalleEntidad.TabIndex = 7;
            // 
            // txtDetalleAccion
            // 
            this.txtDetalleAccion.Location = new System.Drawing.Point(111, 135);
            this.txtDetalleAccion.Name = "txtDetalleAccion";
            this.txtDetalleAccion.ReadOnly = true;
            this.txtDetalleAccion.Size = new System.Drawing.Size(251, 29);
            this.txtDetalleAccion.TabIndex = 6;
            // 
            // txtDetalleUsuario
            // 
            this.txtDetalleUsuario.Location = new System.Drawing.Point(111, 95);
            this.txtDetalleUsuario.Name = "txtDetalleUsuario";
            this.txtDetalleUsuario.ReadOnly = true;
            this.txtDetalleUsuario.Size = new System.Drawing.Size(251, 29);
            this.txtDetalleUsuario.TabIndex = 5;
            // 
            // txtDetalleId
            // 
            this.txtDetalleId.Location = new System.Drawing.Point(64, 47);
            this.txtDetalleId.Name = "txtDetalleId";
            this.txtDetalleId.ReadOnly = true;
            this.txtDetalleId.Size = new System.Drawing.Size(160, 29);
            this.txtDetalleId.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(432, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 24);
            this.label7.TabIndex = 3;
            this.label7.Text = "Entidad:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "Acción:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "Usuario:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "ID:";
            // 
            // frmHistorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 654);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgvHistorial);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "frmHistorial";
            this.Text = "Historial";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBuscarUsuario;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpBuscarFecha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDetalleEntidad;
        private System.Windows.Forms.TextBox txtDetalleAccion;
        private System.Windows.Forms.TextBox txtDetalleUsuario;
        private System.Windows.Forms.TextBox txtDetalleId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEntidadId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDetalleDescripcion;
        private System.Windows.Forms.TextBox txtDetalleFecha;
        private System.Windows.Forms.TextBox txtVigencia;
    }
}