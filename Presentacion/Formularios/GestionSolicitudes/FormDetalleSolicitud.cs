using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CentroAcopioApp.Datos.Repositorios.Implementaciones;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Negocio.Servicios;

//using CentroAcopioApp.DTOs;

namespace CentroAcopioApp.Presentacion.Formularios.GestionSolicitudes
{
    public partial class FormDetalleSolicitud : BaseForm
    {
        private readonly DetalleSolicitudServicio _servicioDetalleSolicitud;
        private readonly SolicitudServicio _servicioSolicitud;

        public FormDetalleSolicitud()
        {
            InitializeComponent();
            var repoDetalleSolicitud = new DetalleSolicitudRepositorio();
            _servicioDetalleSolicitud = new DetalleSolicitudServicio(repoDetalleSolicitud);

            var repoSolicitud = new SolicitudRepositorio();
            _servicioSolicitud = new SolicitudServicio(repoSolicitud);

        }

        private void FormDetalleSolicitud_Load(object sender, EventArgs e)
        {
            CargarDetallesSolicitud();
            CargarSolicitantes();
        }

        private void dgvDetalleSolicitud_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDetalleSolicitud.CurrentRow != null && dgvDetalleSolicitud.CurrentRow.DataBoundItem is DetalleSolicitudDto dto)
                CargarCampos(dto);
        }

        private void CargarCampos(DetalleSolicitudDto dto)
        {
            if (dto == null) return;

            txtIdDetalleSolicitud.Text = dto.Id.ToString();
            txtCantidadSolicitada.Text = dto.CantidadSolicitada.ToString();
            txtCantidadEntregada.Text = dto.CantidadEntregada.ToString();
            txtVigencia.Text = dto.Vigencia.ToString();

            // Seleccionar el solicitante y recurso en los combobox
            cbSolicitante.SelectedValue = dto.SolicitudId;
            cbRecurso.SelectedValue = dto.RecursoId;
        }

        private void CargarDetallesSolicitud()
        {
            try
            {
                var lista = _servicioDetalleSolicitud.ObtenerTodo();
                dgvDetalleSolicitud.DataSource = null;
                dgvDetalleSolicitud.DataSource = lista.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void CargarSolicitantes()
        {
            var solicitantes = _servicioSolicitud.ObtenerTodo().ToList();

            var solicitantesBuscar = new List<SolicitudDto>(solicitantes);
            var solicitantesEditar = new List<SolicitudDto>(solicitantes);

            solicitantesBuscar.Insert(0, new SolicitudDto { Id = 0, Nombre = "Seleccione" });
            solicitantesEditar.Insert(0, new SolicitudDto { Id = 0, Nombre = "Seleccione" });

            cbBuscarSolicitante.DataSource = solicitantesBuscar;
            cbBuscarSolicitante.DisplayMember = "Nombre";
            cbBuscarSolicitante.ValueMember = "Id";
            cbBuscarSolicitante.SelectedIndex = 0;

            cbSolicitante.DataSource = solicitantesEditar;
            cbSolicitante.DisplayMember = "Nombre";
            cbSolicitante.ValueMember = "Id";
            cbSolicitante.SelectedIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string solicitante = cbBuscarSolicitante.Text.Trim();
            string recurso = cbBuscarRecurso.Text.Trim();

            List<DetalleSolicitudDto> resultados = new List<DetalleSolicitudDto>();

            if (!string.IsNullOrEmpty(solicitante) && solicitante != "Seleccione")
            {
                // Buscar por solicitante
               // resultados = _servicioDetalleSolicitud.BuscarPorSolicitante(solicitante).ToList();
            }
            else if (!string.IsNullOrEmpty(recurso) && recurso != "Seleccione")
            {
                // Buscar por recurso
               // resultados = _servicioDetalleSolicitud.BuscarPorRecurso(recurso).ToList();
            }
            else
            {
                // Si no hay filtros, mostrar todos
                resultados = _servicioDetalleSolicitud.ObtenerTodo().ToList();
            }

            dgvDetalleSolicitud.DataSource = resultados;

            if (resultados.Count == 0)
                MessageBox.Show("No se encontraron resultados.");
        }

        private void LimpiarCampos()
        {
            txtIdDetalleSolicitud.Clear();
            txtCantidadSolicitada.Clear();
            txtCantidadEntregada.Clear();
            txtVigencia.Clear();
            cbSolicitante.SelectedIndex = 0;
            cbRecurso.SelectedIndex = 0;
        }

        private void LimpiarSeleccion()
        {
            dgvDetalleSolicitud.ClearSelection();
            LimpiarCampos();
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            cbBuscarSolicitante.SelectedIndex = 0;
            cbBuscarRecurso.SelectedIndex = 0;
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
            if (cbSolicitante.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un solicitante.");
                return;
            }

            if (cbRecurso.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un recurso.");
                return;
            }

            if (!decimal.TryParse(txtCantidadSolicitada.Text, out decimal cantidadSolicitada) || cantidadSolicitada < 0)
            {
                MessageBox.Show("Ingrese una cantidad solicitada válida.");
                return;
            }

            if (!decimal.TryParse(txtCantidadEntregada.Text, out decimal cantidadEntregada) || cantidadEntregada < 0)
            {
                MessageBox.Show("Ingrese una cantidad entregada válida.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtVigencia.Text) || (txtVigencia.Text != "A" && txtVigencia.Text != "I"))
            {
                MessageBox.Show("La vigencia debe ser 'A' (Activo) o 'I' (Inactivo).");
                return;
            }

            // Construir el DTO
            var dto = new DetalleSolicitudDto
            {
                Id = string.IsNullOrEmpty(txtIdDetalleSolicitud.Text) ? 0 : int.Parse(txtIdDetalleSolicitud.Text),
                SolicitudId = (int)cbSolicitante.SelectedValue,
                RecursoId = (int)cbRecurso.SelectedValue,
                CantidadSolicitada = cantidadSolicitada,
                CantidadEntregada = cantidadEntregada,
                Vigencia = txtVigencia.Text.Trim()[0]
            };

            bool exito;

            // Decidir si crear o actualizar
            if (dto.Id == 0)
            {
                exito = _servicioDetalleSolicitud.Crear(dto) > 0;
                if (exito)
                    MessageBox.Show("Detalle de solicitud agregado correctamente.");
            }
            else
            {
                exito = _servicioDetalleSolicitud.Actualizar(dto);
                if (exito)
                    MessageBox.Show("Detalle de solicitud actualizado correctamente.");
            }

            if (exito)
            {
                CargarDetallesSolicitud();
                LimpiarSeleccion();
            }
            else
            {
                MessageBox.Show("No se pudo guardar el detalle de solicitud.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleSolicitud.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un detalle de solicitud para eliminar.");
                return;
            }

            int id = Convert.ToInt32(dgvDetalleSolicitud.CurrentRow.Cells["Id"].Value);

            var confirmar = MessageBox.Show(
                "¿Está seguro que desea eliminar este detalle de solicitud?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmar == DialogResult.Yes)
            {
                bool exito = _servicioDetalleSolicitud.Eliminar(id);

                if (exito)
                {
                    MessageBox.Show("Detalle de solicitud eliminado correctamente.");
                    CargarDetallesSolicitud();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el detalle de solicitud.");
                }
            }
        }
    }
}