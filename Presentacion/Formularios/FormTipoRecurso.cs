using System;
using System.Linq;
using System.Windows.Forms;
using CentroAcopioApp.Datos.Repositorios.Implementaciones;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Negocio.Servicios;

namespace CentroAcopioApp.Presentacion.Formularios
{
    public partial class FormTipoRecurso : Form
    {
        private readonly TipoRecursoServicio _servicio;

        public FormTipoRecurso()
        {
            InitializeComponent();
            var repo = new TipoRecursoRepositorio();
            _servicio = new TipoRecursoServicio(repo);
        }

        private void FormTipoRecurso_Load(object sender, EventArgs e)
        {
            CargarTiposRecursos();
        }

        private void CargarTiposRecursos()
        {
            try
            {
                var lista = _servicio.ObtenerTodo();
                dgvTipoRecurso.DataSource = null;
                dgvTipoRecurso.DataSource = lista.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre es obligatorio.");
                    return;
                }

                // Ver si hay una fila seleccionada en el DataGridView
                int? idSeleccionado = null;
                if (dgvTipoRecurso.CurrentRow != null &&
                    dgvTipoRecurso.CurrentRow.DataBoundItem is TipoRecursoDto seleccionado)
                    idSeleccionado = seleccionado.Id;

                // Crear DTO con los datos del formulario
                var dto = new TipoRecursoDto
                {
                    Id = idSeleccionado ?? 0,
                    Nombre = txtNombre.Text.Trim(),
                    Vigencia = 'A'
                };

                if (idSeleccionado == null || idSeleccionado == 0)
                {
                    // Insertar
                    var nuevoId = _servicio.Crear(dto);
                    MessageBox.Show("Registro insertado con ID: " + nuevoId);
                }
                else
                {
                    // Actualizar
                    var ok = _servicio.Actualizar(dto);
                    MessageBox.Show(ok ? "Registro actualizado correctamente." : "No se pudo actualizar el registro.");
                }

                CargarTiposRecursos();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            dgvTipoRecurso.ClearSelection();
        }

        private void dgvTipoRecurso_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTipoRecurso.CurrentRow != null && dgvTipoRecurso.CurrentRow.DataBoundItem is TipoRecursoDto dto)
                txtNombre.Text = dto.Nombre;
        }
    }
}