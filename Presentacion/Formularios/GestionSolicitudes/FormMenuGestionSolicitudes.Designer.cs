namespace CentroAcopioApp.Presentacion.Formularios.GestionSolicitudes
{
    partial class FormMenuGestionSolicitudes
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
            this.btnAtencionSolicitudes = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnDeatalleSolicitudes = new System.Windows.Forms.Button();
            this.btnSolicitudes = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAsignarRecursosSolicitud = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAtencionSolicitudes
            // 
            this.btnAtencionSolicitudes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtencionSolicitudes.Location = new System.Drawing.Point(112, 290);
            this.btnAtencionSolicitudes.Name = "btnAtencionSolicitudes";
            this.btnAtencionSolicitudes.Size = new System.Drawing.Size(252, 61);
            this.btnAtencionSolicitudes.TabIndex = 20;
            this.btnAtencionSolicitudes.Text = "Atencion a las solicitudes";
            this.btnAtencionSolicitudes.UseVisualStyleBackColor = true;
            this.btnAtencionSolicitudes.Click += new System.EventHandler(this.btnAtencionSolicitudes_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(112, 383);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(252, 61);
            this.btnCerrar.TabIndex = 19;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnDeatalleSolicitudes
            // 
            this.btnDeatalleSolicitudes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeatalleSolicitudes.Location = new System.Drawing.Point(112, 135);
            this.btnDeatalleSolicitudes.Name = "btnDeatalleSolicitudes";
            this.btnDeatalleSolicitudes.Size = new System.Drawing.Size(252, 61);
            this.btnDeatalleSolicitudes.TabIndex = 18;
            this.btnDeatalleSolicitudes.Text = "Detalle de solicitudes";
            this.btnDeatalleSolicitudes.UseVisualStyleBackColor = true;
            this.btnDeatalleSolicitudes.Click += new System.EventHandler(this.btnDeatalleSolicitudes_Click);
            // 
            // btnSolicitudes
            // 
            this.btnSolicitudes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolicitudes.Location = new System.Drawing.Point(112, 59);
            this.btnSolicitudes.Name = "btnSolicitudes";
            this.btnSolicitudes.Size = new System.Drawing.Size(252, 61);
            this.btnSolicitudes.TabIndex = 17;
            this.btnSolicitudes.Text = "Solicitudes";
            this.btnSolicitudes.UseVisualStyleBackColor = true;
            this.btnSolicitudes.Click += new System.EventHandler(this.btnSolicitudes_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(107, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 29);
            this.label1.TabIndex = 16;
            this.label1.Text = "Gestion de solicitudes";
            // 
            // btnAsignarRecursosSolicitud
            // 
            this.btnAsignarRecursosSolicitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarRecursosSolicitud.Location = new System.Drawing.Point(112, 213);
            this.btnAsignarRecursosSolicitud.Name = "btnAsignarRecursosSolicitud";
            this.btnAsignarRecursosSolicitud.Size = new System.Drawing.Size(252, 61);
            this.btnAsignarRecursosSolicitud.TabIndex = 21;
            this.btnAsignarRecursosSolicitud.Text = "Asignar recursos a solicitudes";
            this.btnAsignarRecursosSolicitud.UseVisualStyleBackColor = true;
            this.btnAsignarRecursosSolicitud.Click += new System.EventHandler(this.btnAsignarRecursosSolicitud_Click);
            // 
            // FormMenuGestionSolicitudes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 457);
            this.Controls.Add(this.btnAsignarRecursosSolicitud);
            this.Controls.Add(this.btnAtencionSolicitudes);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnDeatalleSolicitudes);
            this.Controls.Add(this.btnSolicitudes);
            this.Controls.Add(this.label1);
            this.Name = "FormMenuGestionSolicitudes";
            this.Text = "FormMenuGestionSolicitudes";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnAsignarRecursosSolicitud;

        #endregion

        private System.Windows.Forms.Button btnAtencionSolicitudes;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnDeatalleSolicitudes;
        private System.Windows.Forms.Button btnSolicitudes;
        private System.Windows.Forms.Label label1;
    }
}