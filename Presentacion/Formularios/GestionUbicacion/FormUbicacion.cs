using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CentroAcopioApp.Datos.Repositorios.Implementaciones;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Negocio.Servicios;

namespace CentroAcopioApp.Presentacion.Formularios.GestionUbicacion
{
    public partial class FormUbicacion : BaseForm
    {
        private readonly UbicacionServicio _servicioUbicacion;

        public FormUbicacion()
        {
            InitializeComponent();
            var repoUbicacion = new UbicacionRepositorio();
            _servicioUbicacion = new UbicacionServicio(repoUbicacion);
        }

        private void FormUbicacion_Load(object sender, EventArgs e)
        {
            CargarUbicaciones();
        }

        private void dgvUbicaciones_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUbicaciones.CurrentRow != null && dgvUbicaciones.CurrentRow.DataBoundItem is UbicacionDto dto)
                CargarCampos(dto);
        }

        private void CargarCampos(UbicacionDto dto)
        {
            if (dto == null) return;

            txtId.Text = dto.Id.ToString();
            txtNombre.Text = dto.Nombre;
            txtDireccion.Text = dto.Direccion;
            txtVigencia.Text = dto.Vigencia.ToString();
        }

        private void CargarUbicaciones()
        {
            try
            {
                var lista = _servicioUbicacion.ObtenerTodo();
                dgvUbicaciones.DataSource = null;
                dgvUbicaciones.DataSource = lista.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombre = txtBuscarNombre.Text.Trim();

            List<UbicacionDto> resultados = new List<UbicacionDto>();

            if (!string.IsNullOrEmpty(nombre))
            {
                // Buscar por nombre
                resultados = _servicioUbicacion.BuscarPorNombre(nombre).ToList();
            }
            else
            {
                // Si no hay filtros, mostrar todos
                resultados = _servicioUbicacion.ObtenerTodo().ToList();
            }

            dgvUbicaciones.DataSource = resultados;

            if (resultados.Count == 0)
                MessageBox.Show("No se encontraron resultados.");
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtVigencia.Clear();
        }

        private void LimpiarSeleccion()
        {
            dgvUbicaciones.ClearSelection();
            LimpiarCampos();
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Clear();
        }

        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            LimpiarSeleccion();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre de la ubicación.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Ingrese la dirección.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtVigencia.Text) ||
                (txtVigencia.Text.Trim()[0] != 'A' && txtVigencia.Text.Trim()[0] != 'I'))
            {
                MessageBox.Show("La vigencia debe ser 'A' (Activo) o 'I' (Inactivo).");
                return;
            }

            // Construir el DTO
            var dto = new UbicacionDto
            {
                Id = string.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text),
                Nombre = txtNombre.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Vigencia = txtVigencia.Text.Trim()[0]
            };

            bool exito;

            // Decidir si crear o actualizar
            if (dto.Id == 0)
            {
                exito = _servicioUbicacion.Crear(dto) > 0;
                if (exito)
                    MessageBox.Show("Ubicación agregada correctamente.");
            }
            else
            {
                exito = _servicioUbicacion.Actualizar(dto);
                if (exito)
                    MessageBox.Show("Ubicación actualizada correctamente.");
            }

            if (exito)
            {
                CargarUbicaciones();
                LimpiarSeleccion();
            }
            else
            {
                MessageBox.Show("No se pudo guardar la ubicación.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUbicaciones.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una ubicación para eliminar.");
                return;
            }

            int id = Convert.ToInt32(dgvUbicaciones.CurrentRow.Cells["Id"].Value);

            var confirmar = MessageBox.Show(
                "¿Está seguro que desea eliminar esta ubicación?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmar == DialogResult.Yes)
            {
                bool exito = _servicioUbicacion.Eliminar(id);

                if (exito)
                {
                    MessageBox.Show("Ubicación eliminada correctamente.");
                    CargarUbicaciones();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la ubicación.");
                }
            }
        }
    }
}