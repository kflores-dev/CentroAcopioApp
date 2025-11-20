using System;
using System.Windows.Forms;

namespace CentroAcopioApp.Presentacion.Formularios.GestionSolicitudes
{
    public partial class FormMenuGestionSolicitudes : BaseForm
    {
        public FormMenuGestionSolicitudes()
        {
            InitializeComponent();
        }

        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            FormSolicitudes f = new FormSolicitudes();
            f.ShowDialog();
        }

        private void btnDeatalleSolicitudes_Click(object sender, EventArgs e)
        {
            FormDetalleSolicitud f = new FormDetalleSolicitud();
            f.ShowDialog();
        }

        private void btnAsignarRecursosSolicitud_Click(object sender, EventArgs e)
        {
            FormAsignarRecursosSolicitud f = new FormAsignarRecursosSolicitud();
            f.ShowDialog();
        }

        private void btnAtencionSolicitudes_Click(object sender, EventArgs e)
        {
            FormAtencionSolicitudes f = new FormAtencionSolicitudes();
            f.ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}