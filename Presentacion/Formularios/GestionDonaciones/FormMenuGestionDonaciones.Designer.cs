using System.ComponentModel;

namespace CentroAcopioApp.Presentacion.Formularios.GestionDonaciones
{
    partial class FormMenuGestionDonaciones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.btnDetalleDonacion = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnDonaciones = new System.Windows.Forms.Button();
            this.btnProveedor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDetalleDonacion
            // 
            this.btnDetalleDonacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetalleDonacion.Location = new System.Drawing.Point(120, 209);
            this.btnDetalleDonacion.Name = "btnDetalleDonacion";
            this.btnDetalleDonacion.Size = new System.Drawing.Size(252, 61);
            this.btnDetalleDonacion.TabIndex = 10;
            this.btnDetalleDonacion.Text = "Detalles de las donaciones";
            this.btnDetalleDonacion.UseVisualStyleBackColor = true;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(120, 379);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(252, 61);
            this.btnCerrar.TabIndex = 9;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // btnDonaciones
            // 
            this.btnDonaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDonaciones.Location = new System.Drawing.Point(120, 131);
            this.btnDonaciones.Name = "btnDonaciones";
            this.btnDonaciones.Size = new System.Drawing.Size(252, 61);
            this.btnDonaciones.TabIndex = 8;
            this.btnDonaciones.Text = "Donaciones";
            this.btnDonaciones.UseVisualStyleBackColor = true;
            // 
            // btnProveedor
            // 
            this.btnProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProveedor.Location = new System.Drawing.Point(120, 55);
            this.btnProveedor.Name = "btnProveedor";
            this.btnProveedor.Size = new System.Drawing.Size(252, 61);
            this.btnProveedor.TabIndex = 7;
            this.btnProveedor.Text = "Proveedores";
            this.btnProveedor.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(115, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "Gestion de donaciones";
            // 
            // FormMenuGestionDonaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 457);
            this.Controls.Add(this.btnDetalleDonacion);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnDonaciones);
            this.Controls.Add(this.btnProveedor);
            this.Controls.Add(this.label1);
            this.Name = "FormMenuGestionDonaciones";
            this.Text = "FormMenuGestionDonaciones";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnDetalleDonacion;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnDonaciones;
        private System.Windows.Forms.Button btnProveedor;
        private System.Windows.Forms.Label label1;

        #endregion
    }
}