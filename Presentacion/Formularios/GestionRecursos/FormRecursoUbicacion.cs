using System;
using System.Collections.Generic;
using System.Linq;
using CentroAcopioApp.Datos.Repositorios.Implementaciones;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Negocio.Seguridad;
using CentroAcopioApp.Negocio.Servicios;

namespace CentroAcopioApp.Presentacion.Formularios.GestionRecursos
{
    public partial class FormRecursoUbicacion : BaseForm
    {
        private readonly RecursoUbicacionServicio _servicio;
        private readonly RecursoServicio _servicioRecurso;
        private readonly UbicacionServicio _servicioUbicacion;
        private readonly TipoRecursoServicio _servicioTipoRecurso;

        public FormRecursoUbicacion()
        {
            InitializeComponent();

            _servicio = new RecursoUbicacionServicio(new RecursoUbicacionRepositorio());
            _servicioRecurso = new RecursoServicio(new RecursoRepositorio());
            _servicioUbicacion = new UbicacionServicio(new UbicacionRepositorio());
            _servicioTipoRecurso = new TipoRecursoServicio(new TipoRecursoRepositorio());
        }

        private void FormRecursoUbicacion_Load(object sender, EventArgs e)
        {
            Ejecutar(() =>
            {
                CargarCategorias();
                CargarUbicaciones();
                CargarRecursoUbicaciones();
                AplicarPermisos();
            });
        }

        // --------------------------------------------------------------------
        // PERMISOS
        // --------------------------------------------------------------------
        private void AplicarPermisos()
        {
            if (SesionActual.Instancia.EsOperador())
            {
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = false;
                btnNuevoRegistro.Enabled = false;
                SetCamposEdicion(false);
            }
            else if (SesionActual.Instancia.EsAdmin())
            {
                btnEliminar.Enabled = true;
                btnGuardar.Enabled = true;
                btnNuevoRegistro.Enabled = true;
                SetCamposEdicion(true);
            }
        }

        private void SetCamposEdicion(bool habilitar)
        {
            txtIdRecurso.ReadOnly = !habilitar;
            txtCantidad.ReadOnly = !habilitar;
            txtVigencia.ReadOnly = !habilitar;
            cmbUbicacion.Enabled = habilitar;

            txtNombreRecurso.ReadOnly = true; // siempre solo lectura
        }

        // --------------------------------------------------------------------
        // CARGA DE DATOS
        // --------------------------------------------------------------------
        private void CargarCategorias()
        {
            Ejecutar(() =>
            {
                var categorias = _servicioTipoRecurso.ObtenerTodo().ToList();
                var listaCombo = categorias.Prepend(new TipoRecursoDto { Id = 0, Nombre = "Seleccione" }).ToList();
                CargarCombo(cbBuscarCategoria, listaCombo, "Nombre", "Id", true);
            });
        }

        private void CargarUbicaciones()
        {
            Ejecutar(() =>
            {
                var lista = _servicioUbicacion.ObtenerTodo().ToList();
                var listaCombo = lista.Prepend(new UbicacionDto { Id = 0, Nombre = "Seleccione" }).ToList();
                CargarCombo(cmbUbicacion, listaCombo, "Nombre", "Id", true);
            });
        }

        private void CargarRecursoUbicaciones()
        {
            Ejecutar(() =>
            {
                var lista = _servicio.ObtenerTodo().ToList();

                dgvRecursoUbicacion.DataSource = lista;

                var columnasVisibles = new Dictionary<string, string>
                {
                    { "Id", "ID" },
                    { "RecursoNombre", "Recurso" },
                    { "UbicacionNombre", "Ubicación" },
                    { "Cantidad", "Cantidad" },
                    { "Vigencia", "Vigencia" }
                };

                var columnasOcultas = new List<string>
                {
                    "RecursoId",
                    "UbicacionId"
                };

                ConfigurarColumnas(dgvRecursoUbicacion, columnasVisibles, columnasOcultas);
            });
        }

        // --------------------------------------------------------------------
        // SELECCIÓN EN EL DATAGRID
        // --------------------------------------------------------------------
        private void dgvRecursoUbicacion_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRecursoUbicacion.CurrentRow?.DataBoundItem is RecursoUbicacionDto dto)
            {
                txtId.Text = dto.Id.ToString();
                txtIdRecurso.Text = dto.RecursoId.ToString();
                txtNombreRecurso.Text = dto.RecursoNombre;
                cmbUbicacion.SelectedValue = dto.UbicacionId;
                txtCantidad.Text = dto.Cantidad.ToString();
                txtVigencia.Text = dto.Vigencia.ToString();

                if (SesionActual.Instancia.EsOperador())
                {
                    SetCamposEdicion(false);
                    btnGuardar.Enabled = false;
                }
                else if (SesionActual.Instancia.EsAdmin())
                {
                    SetCamposEdicion(true);
                    btnGuardar.Enabled = true;
                }
            }
        }

        // --------------------------------------------------------------------
        // BUSCAR
        // --------------------------------------------------------------------
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Ejecutar(() =>
            {
                string nombre = txtBuscarNombre.Text.Trim();
                int categoriaId = (int)(cbBuscarCategoria.SelectedValue ?? 0);
                int ubicacionId = (int)(cmbUbicacion.SelectedValue ?? 0);

                var resultados = _servicio.ObtenerTodo().ToList();

                if (!string.IsNullOrEmpty(nombre))
                {
                    var recursos = _servicioRecurso.BuscarPorNombre(nombre).Select(r => r.Id).ToList();
                    resultados = resultados.Where(r => recursos.Contains(r.RecursoId)).ToList();
                }

                if (categoriaId > 0)
                {
                    var recursos = _servicioRecurso.BuscarPorTipo(categoriaId).Select(r => r.Id).ToList();
                    resultados = resultados.Where(r => recursos.Contains(r.RecursoId)).ToList();
                }

                if (ubicacionId > 0)
                    resultados = resultados.Where(r => r.UbicacionId == ubicacionId).ToList();

                dgvRecursoUbicacion.DataSource = resultados;

                if (resultados.Count == 0)
                    MostrarInfo("No se encontraron resultados.");
            });
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Clear();
            cbBuscarCategoria.SelectedIndex = 0;
            cmbUbicacion.SelectedIndex = 0;
        }

        // --------------------------------------------------------------------
        // CRUD
        // --------------------------------------------------------------------
        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            dgvRecursoUbicacion.ClearSelection();
            LimpiarControles(this);
            SetCamposEdicion(SesionActual.Instancia.EsAdmin());
            btnGuardar.Enabled = SesionActual.Instancia.EsAdmin();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles(this);
            SetCamposEdicion(SesionActual.Instancia.EsAdmin());
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (SesionActual.Instancia.EsOperador())
            {
                MostrarError("No tiene permisos para crear o actualizar registros.");
                return;
            }

            Ejecutar(() =>
            {
                if (!int.TryParse(txtIdRecurso.Text.Trim(), out int recursoId) || recursoId <= 0)
                {
                    MostrarError("Ingrese un ID de recurso válido.");
                    return;
                }

                if (cmbUbicacion.SelectedIndex <= 0)
                {
                    MostrarError("Seleccione una ubicación.");
                    return;
                }

                if (!decimal.TryParse(txtCantidad.Text.Trim(), out decimal cantidad))
                {
                    MostrarError("Ingrese una cantidad válida.");
                    return;
                }

                var vigencia = string.IsNullOrWhiteSpace(txtVigencia.Text) ? 'A' : txtVigencia.Text.Trim()[0];

                bool exito = false;

                // EDICIÓN DE UN REGISTRO EXISTENTE
                if (!string.IsNullOrEmpty(txtId.Text))
                {
                    int id = int.Parse(txtId.Text);
                    var dto = new RecursoUbicacionDto
                    {
                        Id = id,
                        RecursoId = recursoId,
                        UbicacionId = (int)cmbUbicacion.SelectedValue,
                        Cantidad = cantidad,
                        Vigencia = vigencia
                    };

                    exito = _servicio.Actualizar(dto);
                    if (exito) MostrarInfo("Registro actualizado correctamente.");
                }
                else
                {
                    // CREAR NUEVO: aplicar lógica upsert
                    var existente = _servicio.ObtenerTodo()
                        .FirstOrDefault(r =>
                            r.RecursoId == recursoId && r.UbicacionId == (int)cmbUbicacion.SelectedValue);

                    if (existente != null)
                    {
                        existente.Cantidad += cantidad;
                        existente.Vigencia = vigencia;
                        exito = _servicio.Actualizar(existente);
                        if (exito) MostrarInfo("Cantidad actualizada correctamente.");
                    }
                    else
                    {
                        var dto = new RecursoUbicacionDto
                        {
                            RecursoId = recursoId,
                            UbicacionId = (int)cmbUbicacion.SelectedValue,
                            Cantidad = cantidad,
                            Vigencia = vigencia
                        };

                        exito = _servicio.Crear(dto) > 0;
                        if (exito) MostrarInfo("Registro agregado correctamente.");
                    }
                }

                if (exito)
                {
                    CargarRecursoUbicaciones();
                    dgvRecursoUbicacion.ClearSelection();
                    LimpiarControles(this);
                }
                else
                {
                    MostrarError("No se pudo guardar el registro.");
                }
            });
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (SesionActual.Instancia.EsOperador())
            {
                MostrarError("No tiene permisos para eliminar registros.");
                return;
            }

            Ejecutar(() =>
            {
                if (dgvRecursoUbicacion.CurrentRow == null)
                {
                    MostrarError("Seleccione un registro para eliminar.");
                    return;
                }

                int id = Convert.ToInt32(dgvRecursoUbicacion.CurrentRow.Cells["Id"].Value);

                if (!Confirmar("¿Está seguro que desea eliminar este registro?"))
                    return;

                bool exito = _servicio.Eliminar(id);

                if (exito)
                {
                    MostrarInfo("Registro eliminado correctamente.");
                    CargarRecursoUbicaciones();
                    LimpiarControles(this);
                }
                else
                {
                    MostrarError("No se pudo eliminar el registro.");
                }
            });
        }
    }
}