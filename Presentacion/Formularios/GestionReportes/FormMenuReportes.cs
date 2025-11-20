using System;
using System.Windows.Forms;

namespace CentroAcopioApp.Presentacion.Formularios.GestionReportes
{
    public partial class FormMenuReportes : BaseForm
    {
        public FormMenuReportes()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}