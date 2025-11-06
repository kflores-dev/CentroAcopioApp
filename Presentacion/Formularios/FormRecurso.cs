using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CentroAcopioApp.Datos.Repositorios.Implementaciones;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Negocio.Servicios;

namespace CentroAcopioApp.Presentacion.Formularios
{
    public partial class FormRecurso : Form
    {
        private readonly RecursoServicio _servicioRecurso;
        private readonly TipoRecursoServicio _servicioTipoRecurso;

        public FormRecurso()
        {
            InitializeComponent();
            var repoRecurso = new RecursoRepositorio();
            _servicioRecurso = new RecursoServicio(repoRecurso);
            var repoTipoRecurso = new TipoRecursoRepositorio();
            _servicioTipoRecurso = new TipoRecursoServicio(repoTipoRecurso);
        }

        private void FormRecurso_Load(object sender, EventArgs e)
        {
            CargarRecursos();
            CargarTipos();
        }

        private void dgvRecursos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRecursos.CurrentRow != null && dgvRecursos.CurrentRow.DataBoundItem is RecursoDto dto)
                CargarCampos(dto);
        }

        private void CargarCampos(RecursoDto dto)
        {
            if (dto == null) return;

            txtId.Text = dto.Id.ToString();
            txtNombre.Text = dto.Nombre;
            txtUnidadMedida.Text = dto.UnidadMedida;
            txtVigencia.Text = dto.Vigencia.ToString();

            cbTipoRecurso.SelectedValue = dto.TipoId;
        }

        private void CargarRecursos()
        {
            try
            {
                var lista = _servicioRecurso.ObtenerTodo();
                dgvRecursos.DataSource = null;
                dgvRecursos.DataSource = lista.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void CargarTipos()
        {
            var tipos = _servicioTipoRecurso.ObtenerTodo().ToList();

            var tiposBuscar = new List<TipoRecursoDto>(tipos);
            var tiposEditar = new List<TipoRecursoDto>(tipos);

            tiposBuscar.Insert(0, new TipoRecursoDto { Id = 0, Nombre = "Seleccione" });
            tiposEditar.Insert(0, new TipoRecursoDto { Id = 0, Nombre = "Seleccione" });

            cbBuscarCategoria.DataSource = tiposBuscar;
            cbBuscarCategoria.DisplayMember = "Nombre";
            cbBuscarCategoria.ValueMember = "Id";
            cbBuscarCategoria.SelectedIndex = 0;

            cbTipoRecurso.DataSource = tiposEditar;
            cbTipoRecurso.DisplayMember = "Nombre";
            cbTipoRecurso.ValueMember = "Id";
            cbTipoRecurso.SelectedIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombre = txtBuscarNombre.Text.Trim();
            int tipoId = (int)(cbBuscarCategoria.SelectedValue ?? 0);

            List<RecursoDto> resultados = new List<RecursoDto>();

            if (!string.IsNullOrEmpty(nombre))
            {
                // Buscar por nombre
                resultados = _servicioRecurso.BuscarPorNombre(nombre).ToList();
            }
            else if (tipoId > 0)
            {
                // Buscar por categoría
                resultados = _servicioRecurso.BuscarPorTipo(tipoId).ToList();
            }
            else
            {
                // Si no hay filtros, mostrar todos
                resultados = _servicioRecurso.ObtenerTodo().ToList();
            }

            dgvRecursos.DataSource = resultados;

            if (resultados.Count == 0)
                MessageBox.Show("No se encontraron resultados.");
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            cbTipoRecurso.SelectedIndex = 0;
            txtUnidadMedida.Clear();
            txtVigencia.Clear();
        }

        private void LimpiarSeleccion()
        {
            dgvRecursos.ClearSelection();
            LimpiarCampos();
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Clear();
            cbBuscarCategoria.SelectedIndex = 0;
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
                MessageBox.Show("Ingrese el nombre del recurso.");
                return;
            }

            if (cbTipoRecurso.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un tipo de recurso.");
                return;
            }

            // Construir el DTO
            var dto = new RecursoDto
            {
                Id = string.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text),
                Nombre = txtNombre.Text.Trim(),
                TipoId = (int)(cbTipoRecurso.SelectedValue),
                UnidadMedida = txtUnidadMedida.Text.Trim(),
                Vigencia = txtVigencia.Text.Trim()[0]
            };

            bool exito;

            // Decidir si crear o actualizar
            if (dto.Id == 0)
            {
                exito = _servicioRecurso.Crear(dto) > 0;
                if (exito)
                    MessageBox.Show("Recurso agregado correctamente.");
            }
            else
            {
                exito = _servicioRecurso.Actualizar(dto);
                if (exito)
                    MessageBox.Show("Recurso actualizado correctamente.");
            }

            if (exito)
            {
                CargarRecursos(); // refresca el DataGridView
                LimpiarSeleccion(); // limpia los controles
            }
            else
            {
                MessageBox.Show("No se pudo guardar el recurso.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvRecursos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un recurso para eliminar.");
                return;
            }

            int id = Convert.ToInt32(dgvRecursos.CurrentRow.Cells["Id"].Value);

            var confirmar = MessageBox.Show(
                "¿Está seguro que desea eliminar este recurso?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmar == DialogResult.Yes)
            {
                bool exito = _servicioRecurso.Eliminar(id);

                if (exito)
                {
                    MessageBox.Show("Recurso eliminado correctamente.");
                    CargarRecursos(); 
                    LimpiarCampos(); 
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el recurso.");
                }
            }
        }
    }
}