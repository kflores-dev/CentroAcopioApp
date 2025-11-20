using System;
using System.Windows.Forms;

namespace CentroAcopioApp.Presentacion.Formularios.GestionDonaciones
{
    public partial class FormMenuGestionDonaciones : BaseForm
    {
        public FormMenuGestionDonaciones()
        {
            InitializeComponent();
        }

        private void btnAsignarRecursosDonacion_Click(object sender, EventArgs e)
        {
            FormAsignarRecursosDonacion f = new FormAsignarRecursosDonacion();
            f.ShowDialog();
        }

        private void btnProveedor_Click(object sender, EventArgs e)
        {
            FormProveedor f = new FormProveedor();
            f.ShowDialog();
        }

        private void btnDonaciones_Click(object sender, EventArgs e)
        {
            FormDonacion f = new FormDonacion();
            f.ShowDialog();
        }

        private void btnDetalleDonacion_Click(object sender, EventArgs e)
        {
            FormDetalleDonacion f = new FormDetalleDonacion();
            f.ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}