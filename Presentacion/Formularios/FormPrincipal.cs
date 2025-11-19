using System;
using System.Windows.Forms;
using CentroAcopioApp.Negocio.Seguridad;
using CentroAcopioApp.Presentacion.Formularios.GestionDonaciones;
using CentroAcopioApp.Presentacion.Formularios.GestionRecursos;

namespace CentroAcopioApp.Presentacion.Formularios
{
    public partial class FormPrincipal : BaseForm
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void CargarDatosUsuario()
        {
            lbUsuario.Text = SesionActual.Instancia.Nombre;
            if (SesionActual.Instancia.EsAdmin())
                lbRol.Text = "Administrador";
            else lbRol.Text = "Operador";
        }

        private void btnGestionRecursos_Click(object sender, EventArgs e)
        {
            FormMenuGestionRecursos formito = new FormMenuGestionRecursos();
            formito.ShowDialog();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            CargarDatosUsuario();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGestionDonaciones_Click(object sender, EventArgs e)
        {
            FormMenuGestionDonaciones formito = new FormMenuGestionDonaciones();
            formito.ShowDialog();
        }
    }
}