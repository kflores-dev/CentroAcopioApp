namespace CentroAcopioApp.Presentacion.Formularios.GestionReportes
{
    partial class FormMenuReportes
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAtencionSolicitudes = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnGenerarReportesDonaciones = new System.Windows.Forms.Button();
            this.btnGenerarReportesInventario = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAtencionSolicitudes
            // 
            this.btnAtencionSolicitudes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtencionSolicitudes.Location = new System.Drawing.Point(112, 213);
            this.btnAtencionSolicitudes.Name = "btnAtencionSolicitudes";
            this.btnAtencionSolicitudes.Size = new System.Drawing.Size(252, 61);
            this.btnAtencionSolicitudes.TabIndex = 25;
            this.btnAtencionSolicitudes.Text = "Generar reportes de solicitudes";
            this.btnAtencionSolicitudes.UseVisualStyleBackColor = true;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(112, 294);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(252, 61);
            this.btnCerrar.TabIndex = 24;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnGenerarReportesDonaciones
            // 
            this.btnGenerarReportesDonaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarReportesDonaciones.Location = new System.Drawing.Point(112, 135);
            this.btnGenerarReportesDonaciones.Name = "btnGenerarReportesDonaciones";
            this.btnGenerarReportesDonaciones.Size = new System.Drawing.Size(252, 61);
            this.btnGenerarReportesDonaciones.TabIndex = 23;
            this.btnGenerarReportesDonaciones.Text = "Generar reportes de donaciones";
            this.btnGenerarReportesDonaciones.UseVisualStyleBackColor = true;
            // 
            // btnGenerarReportesInventario
            // 
            this.btnGenerarReportesInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarReportesInventario.Location = new System.Drawing.Point(112, 59);
            this.btnGenerarReportesInventario.Name = "btnGenerarReportesInventario";
            this.btnGenerarReportesInventario.Size = new System.Drawing.Size(252, 61);
            this.btnGenerarReportesInventario.TabIndex = 22;
            this.btnGenerarReportesInventario.Text = "Generar reportes de inventario";
            this.btnGenerarReportesInventario.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(119, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 29);
            this.label1.TabIndex = 21;
            this.label1.Text = "Gestion de reportes";
            // 
            // FormMenuReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 374);
            this.Controls.Add(this.btnAtencionSolicitudes);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnGenerarReportesDonaciones);
            this.Controls.Add(this.btnGenerarReportesInventario);
            this.Controls.Add(this.label1);
            this.Name = "FormMenuReportes";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnAtencionSolicitudes;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnGenerarReportesDonaciones;
        private System.Windows.Forms.Button btnGenerarReportesInventario;
        private System.Windows.Forms.Label label1;
    }
}

