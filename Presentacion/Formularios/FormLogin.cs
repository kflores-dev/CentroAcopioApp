using System;
using System.Windows.Forms;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.Excepciones;
using CentroAcopioApp.Negocio.Servicios;

namespace CentroAcopioApp.Presentacion.Formularios
{
    public partial class FormLogin : BaseForm
    {
        private readonly AutenticacionServicio _authServicio;

        public FormLogin(IUsuarioRepositorio usuarioRepositorio)
        {
            InitializeComponent();
            _authServicio = new AutenticacionServicio(usuarioRepositorio);
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            string username = txtNombreUsuario.Text.Trim();
            string password = txtPassword.Text;

            try
            {
                // Intentar login
                _authServicio.Login(username, password);

                // Abrir el menú como ventana modal
                using (FormPrincipal menuForm = new FormPrincipal())
                {
                    // Ocultar el login mientras el menú está abierto
                    this.Hide();

                    // Mostrar modal
                    menuForm.ShowDialog();

                    // Al cerrar el menú, cerrar sesión automáticamente
                    _authServicio.Logout();
                }

                // Mostrar nuevamente el login para poder iniciar sesión otra vez
                txtPassword.Clear();
                this.Show();
                txtNombreUsuario.Focus();
            }
            catch (ExcepcionServicio ex)
            {
                MessageBox.Show(ex.Message, "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}