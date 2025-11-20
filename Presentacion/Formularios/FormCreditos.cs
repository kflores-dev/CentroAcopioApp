using System;
using System.Windows.Forms;

namespace CentroAcopioApp.Presentacion.Formularios
{
    public partial class FormCreditos : BaseForm
    {
        public FormCreditos()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}