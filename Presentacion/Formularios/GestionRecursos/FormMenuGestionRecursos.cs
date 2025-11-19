using System;
using System.Windows.Forms;

namespace CentroAcopioApp.Presentacion.Formularios.GestionRecursos
{
    public partial class FormMenuGestionRecursos : Form
    {
        public FormMenuGestionRecursos()
        {
            InitializeComponent();
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            FormMovimientosRecursos movimientosRecursos = new FormMovimientosRecursos();
            movimientosRecursos.ShowDialog();
        }

        private void btnRecurso_Click(object sender, EventArgs e)
        {
            FormRecurso recurso = new FormRecurso();
            recurso.ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTiposRecursos_Click(object sender, EventArgs e)
        {
            FormTipoRecurso tipo = new FormTipoRecurso();
            tipo.ShowDialog();
        }

        private void btnUbicacionRecursos_Click(object sender, EventArgs e)
        {
            FormRecursoUbicacion formito = new FormRecursoUbicacion();
            formito.ShowDialog();
        }
    }
}