using System;
using System.Windows.Forms;
using CentroAcopioApp.Negocio.Seguridad;
using CentroAcopioApp.Presentacion.Formularios.GestionDonaciones;
using CentroAcopioApp.Presentacion.Formularios.GestionRecursos;
using CentroAcopioApp.Presentacion.Formularios.GestionReportes;
using CentroAcopioApp.Presentacion.Formularios.GestionSolicitudes;
using CentroAcopioApp.Presentacion.Formularios.GestionUbicacion;
using CentroAcopioApp.Presentacion.Formularios.GestionUsuario;

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

        private void CargarPermisos()
        {
            if (!SesionActual.Instancia.EsOperador()) return;
            btnUsuarios.Visible = false;
            btnHistorial.Visible = false;
            lbHistorial.Visible = false;
            lbUsuarios.Visible = false;
        }

        private void btnGestionRecursos_Click(object sender, EventArgs e)
        {
            FormMenuGestionRecursos formito = new FormMenuGestionRecursos();
            formito.ShowDialog();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            CargarDatosUsuario();
            CargarPermisos();
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

        private void btnUbicaciones_Click(object sender, EventArgs e)
        {
            FormUbicacion f = new FormUbicacion();
            f.ShowDialog();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FormUsuario f = new FormUsuario();
            f.ShowDialog();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            FormHistorial f = new FormHistorial();
            f.ShowDialog();
        }

        private void btnGestionSolicitudes_Click(object sender, EventArgs e)
        {
            FormMenuGestionSolicitudes f = new FormMenuGestionSolicitudes();
            f.ShowDialog();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            FormMenuReportes f = new FormMenuReportes();
            f.ShowDialog();
        }

        private void btnCreditos_Click(object sender, EventArgs e)
        {
            FormCreditos f = new FormCreditos();
            f.ShowDialog();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}