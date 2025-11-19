using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CentroAcopioApp.Datos.Repositorios.Implementaciones;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Negocio.Servicios;

namespace CentroAcopioApp.Presentacion.Formularios.GestionRecursos
{
    public partial class FormMovimientosRecursos : BaseForm
    {
        private readonly RecursoServicio _servicioRecurso;
        private readonly RecursoUbicacionServicio _servicioRecursoUbicacion;
        private readonly UbicacionServicio _servicioUbicacion;
        private readonly TipoRecursoServicio _servicioTipoRecurso;

        public FormMovimientosRecursos()
        {
            InitializeComponent();

            _servicioRecurso = new RecursoServicio(new RecursoRepositorio());
            _servicioRecursoUbicacion = new RecursoUbicacionServicio(new RecursoUbicacionRepositorio());
            _servicioUbicacion = new UbicacionServicio(new UbicacionRepositorio());
            _servicioTipoRecurso = new TipoRecursoServicio(new TipoRecursoRepositorio());
        }

        private void FormMovimientosRecursos_Load(object sender, EventArgs e)
        {
            Ejecutar(() =>
            {
                CargarTiposRecurso();
                CargarUbicaciones();
                CargarRecursos();
            });
        }

        // --------------------------------------------------------------------
        // CARGA INICIAL
        // --------------------------------------------------------------------
        private void CargarTiposRecurso()
        {
            Ejecutar(() =>
            {
                var tipos = _servicioTipoRecurso.ObtenerTodo().ToList();
                var listaCombo = tipos.Prepend(new TipoRecursoDto { Id = 0, Nombre = "Seleccione" }).ToList();
                CargarCombo(cbTipoRecurso, listaCombo, "Nombre", "Id", true);
            });
        }

        private void CargarUbicaciones()
        {
            Ejecutar(() =>
            {
                var ubicaciones = _servicioUbicacion.ObtenerTodo().ToList();
                var listaCombo = ubicaciones.Prepend(new UbicacionDto { Id = 0, Nombre = "Seleccione" }).ToList();
                CargarCombo(cbBuscarUbicacion, listaCombo, "Nombre", "Id", true);
            });
        }

        private void CargarRecursos()
        {
            Ejecutar(() =>
            {
                var recursos = _servicioRecurso.ObtenerTodo().ToList();
                var datos = recursos.Select(r => new
                {
                    r.Id,
                    r.Nombre,
                    TipoNombre = r.TipoNombre,
                    CantidadTotal = _servicioRecursoUbicacion.ObtenerTodo()
                        .Where(ru => ru.RecursoId == r.Id)
                        .Sum(ru => ru.Cantidad),
                    r.UnidadMedida
                }).ToList();

                dgvRecursos.DataSource = datos;

                var columnasVisibles = new Dictionary<string, string>
                {
                    { "Id", "ID" },
                    { "Nombre", "Recurso" },
                    { "TipoNombre", "Tipo" },
                    { "CantidadTotal", "Cantidad Total" },
                    { "UnidadMedida", "Unidad" }
                };
                ConfigurarColumnas(dgvRecursos, columnasVisibles, ocultar: null);
            });
        }

        // --------------------------------------------------------------------
        // BUSCAR RECURSOS
        // --------------------------------------------------------------------
        private void btnBuscarRecursos_Click(object sender, EventArgs e)
        {
            Ejecutar(() =>
            {
                string nombre = txtBuscarNombreRecurso.Text.Trim();
                int tipoId = (int)(cbTipoRecurso.SelectedValue ?? 0);

                var recursos = _servicioRecurso.ObtenerTodo().ToList();

                if (!string.IsNullOrEmpty(nombre))
                {
                    recursos = recursos
                        .Where(r => r.Nombre != null &&
                                    r.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToList();
                }

                if (tipoId > 0)
                    recursos = recursos.Where(r => r.TipoId == tipoId).ToList();

                var datos = recursos.Select(r => new
                {
                    r.Id,
                    r.Nombre,
                    TipoNombre = r.TipoNombre,
                    CantidadTotal = _servicioRecursoUbicacion.ObtenerTodo()
                        .Where(ru => ru.RecursoId == r.Id)
                        .Sum(ru => ru.Cantidad),
                    r.UnidadMedida
                }).ToList();

                dgvRecursos.DataSource = datos;
            });
        }

        private void btnLimpiarBuscarRecursos_Click(object sender, EventArgs e)
        {
            txtBuscarNombreRecurso.Clear();
            cbTipoRecurso.SelectedIndex = 0;
            CargarRecursos();
        }

        // --------------------------------------------------------------------
        // BUSCAR POR UBICACIÓN
        // --------------------------------------------------------------------
        private void btnBuscarUbicacionRecurso_Click(object sender, EventArgs e)
        {
            Ejecutar(() =>
            {
                int ubicacionId = (int)(cbBuscarUbicacion.SelectedValue ?? 0);

                if (ubicacionId <= 0)
                {
                    CargarRecursos();
                    return;
                }

                var recursosIds = _servicioRecursoUbicacion.ObtenerTodo()
                    .Where(ru => ru.UbicacionId == ubicacionId)
                    .Select(ru => ru.RecursoId)
                    .Distinct()
                    .ToList();

                var recursos = _servicioRecurso.ObtenerTodo()
                    .Where(r => recursosIds.Contains(r.Id))
                    .Select(r => new
                    {
                        r.Id,
                        r.Nombre,
                        TipoNombre = r.TipoNombre,
                        CantidadTotal = _servicioRecursoUbicacion.ObtenerTodo()
                            .Where(ru => ru.RecursoId == r.Id)
                            .Sum(ru => ru.Cantidad),
                        r.UnidadMedida
                    }).ToList();

                dgvRecursos.DataSource = recursos;
            });
        }

        private void btnLimpiarUbicacion_Click(object sender, EventArgs e)
        {
            cbBuscarUbicacion.SelectedIndex = 0;
            CargarRecursos();
            dgvUbicacionRecurso.DataSource = null;
            txtRecursoSeleccionado.Clear();
            txtCantidadTotal.Clear();
            txtUnidadMedida.Clear();
        }

        // --------------------------------------------------------------------
        // SELECCIÓN DE RECURSO
        // --------------------------------------------------------------------
        private void dgvRecursos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRecursos.CurrentRow?.DataBoundItem == null) return;

            dynamic dto = dgvRecursos.CurrentRow.DataBoundItem;
            int recursoId = dto.Id;

            txtRecursoSeleccionado.Text = dto.Nombre;
            txtCantidadTotal.Text = dto.CantidadTotal.ToString();
            txtUnidadMedida.Text = dto.UnidadMedida;

            // Cargar ubicaciones del recurso seleccionado usando DTO definido
            var ubicaciones = _servicioRecursoUbicacion.BuscarPorRecurso(recursoId)
                .Select(ru => new RecursoUbicacionVistaDto
                {
                    Id = ru.Id,
                    UbicacionId = ru.UbicacionId,
                    UbicacionNombre = _servicioUbicacion.ObtenerPorId(ru.UbicacionId)?.Nombre ?? "Desconocido",
                    Cantidad = ru.Cantidad
                }).ToList();

            dgvUbicacionRecurso.DataSource = ubicaciones;

            var columnasVisibles = new Dictionary<string, string>
            {
                { "Id", "ID" },
                { "UbicacionNombre", "Ubicación" },
                { "Cantidad", "Cantidad Disponible" }
            };
            ConfigurarColumnas(dgvUbicacionRecurso, columnasVisibles, ocultar: null);
        }

        // --------------------------------------------------------------------
        // MÉTODOS AUXILIARES
        // --------------------------------------------------------------------
        private void Ejecutar(Action accion)
        {
            try
            {
                accion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCombo(ComboBox combo, object lista, string displayMember, string valueMember,
            bool seleccionarPrimero = false)
        {
            combo.DataSource = null;
            combo.DisplayMember = displayMember;
            combo.ValueMember = valueMember;
            combo.DataSource = lista;
            if (seleccionarPrimero && combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }

        private void ConfigurarColumnas(DataGridView dgv, Dictionary<string, string> columnasVisibles,
            List<string> ocultar)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (columnasVisibles.ContainsKey(col.Name))
                    col.HeaderText = columnasVisibles[col.Name];
                if (ocultar != null && ocultar.Contains(col.Name))
                    col.Visible = false;
            }
        }

        // --------------------------------------------------------------------
// SELECCIÓN DE UBICACIÓN DE UN RECURSO
// --------------------------------------------------------------------
        private void dgvUbicacionRecurso_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUbicacionRecurso.CurrentRow?.DataBoundItem is RecursoUbicacionVistaDto dto)
            {
                // Campos X
                txtUbicacionX.Text = dto.UbicacionNombre;
                txtCantidadX.Text = dto.Cantidad.ToString();

                if (dgvRecursos.CurrentRow == null) return;
                int recursoId = (int)dgvRecursos.CurrentRow.Cells["Id"].Value;

                // Combo Y: todas menos la X
                var ubicacionesY = _servicioUbicacion.ObtenerTodo()
                    .Where(u => u.Id != dto.UbicacionId)
                    .ToList();

                CargarCombo(cbUbicacionY, ubicacionesY, "Nombre", "Id", seleccionarPrimero: true);

                if (cbUbicacionY.SelectedIndex > 0)
                    ActualizarCantidadY(recursoId, (int)cbUbicacionY.SelectedValue);
                else
                    txtCantidadY.Clear();
            }
        }

        private void cbUbicacionY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUbicacionY.SelectedIndex <= 0)
            {
                txtCantidadY.Clear();
                return;
            }

            if (dgvRecursos.CurrentRow == null) return;
            int recursoId = (int)dgvRecursos.CurrentRow.Cells["Id"].Value;
            int ubicacionYId = (int)cbUbicacionY.SelectedValue;

            ActualizarCantidadY(recursoId, ubicacionYId);
        }

        private void ActualizarCantidadY(int recursoId, int ubicacionId)
        {
            var existente = _servicioRecursoUbicacion.BuscarPorRecurso(recursoId)
                .FirstOrDefault(r => r.UbicacionId == ubicacionId);

            txtCantidadY.Text = existente?.Cantidad.ToString() ?? "0";
        }

// --------------------------------------------------------------------
// BOTÓN CAMBIAR
// --------------------------------------------------------------------
        private void btnCambiar_Click(object sender, EventArgs e)
        {
            if (dgvRecursos.CurrentRow == null || dgvUbicacionRecurso.CurrentRow == null)
            {
                MostrarError("Seleccione un recurso y una ubicación origen.");
                return;
            }

            int recursoId = (int)dgvRecursos.CurrentRow.Cells["Id"].Value;
            int ubicacionOrigenId =
                (int)((RecursoUbicacionVistaDto)dgvUbicacionRecurso.CurrentRow.DataBoundItem).UbicacionId;
            int ubicacionDestinoId = (int)cbUbicacionY.SelectedValue;

            if (!decimal.TryParse(txtCantidadCambiar.Text.Trim(), out decimal cantidadCambiar) || cantidadCambiar <= 0)
            {
                MostrarError("Ingrese una cantidad válida para mover.");
                return;
            }

            try
            {
                _servicioRecursoUbicacion.TraspasarRecurso(recursoId, ubicacionOrigenId, ubicacionDestinoId,
                    cantidadCambiar);
                MostrarInfo("Movimiento realizado correctamente.");

                // Refrescar grids y campos
                dgvRecursos_SelectionChanged(null, null);
                dgvUbicacionRecurso_SelectionChanged(null, null);
                txtCantidadCambiar.Clear();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }
    }
}