using System;
using System.Collections.Generic;
using System.Linq;
using CentroAcopioApp.Datos.Repositorios.Implementaciones;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Negocio.Seguridad;
using CentroAcopioApp.Negocio.Servicios;

namespace CentroAcopioApp.Presentacion.Formularios.GestionRecursos
{
    public partial class FormRecurso : BaseForm
    {
        private readonly RecursoServicio _servicioRecurso;
        private readonly TipoRecursoServicio _servicioTipoRecurso;

        public FormRecurso()
        {
            InitializeComponent();

            _servicioRecurso = new RecursoServicio(new RecursoRepositorio());
            _servicioTipoRecurso = new TipoRecursoServicio(new TipoRecursoRepositorio());
        }

        private void FormRecurso_Load(object sender, EventArgs e)
        {
            Ejecutar(() =>
            {
                CargarTipos();
                CargarRecursos();
            });
        }

        private void AplicarPermisos()
        {
            if (SesionActual.Instancia.EsOperador())
            {
                btnEliminar.Enabled = false; // No puede eliminar
                btnGuardar.Enabled = false; // Deshabilitado por defecto
                btnNuevoRegistro.Enabled = true;

                // Campos inicialmente bloqueados (si hay selección)
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
            txtNombre.Enabled = habilitar;
            txtUnidadMedida.Enabled = habilitar;
            txtVigencia.Enabled = habilitar;
            cbTipoRecurso.Enabled = habilitar;
        }

        private void dgvRecursos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRecursos.CurrentRow?.DataBoundItem is RecursoDto dto)
            {
                CargarCampos(dto);

                if (SesionActual.Instancia.EsOperador())
                {
                    // Solo ver, no editar
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

        private void CargarCampos(RecursoDto dto)
        {
            txtId.Text = dto.Id.ToString();
            txtNombre.Text = dto.Nombre;
            txtUnidadMedida.Text = dto.UnidadMedida;
            txtVigencia.Text = dto.Vigencia.ToString();
            cbTipoRecurso.SelectedValue = dto.TipoId;
        }

        // --------------------------------------------------------------------
        // CARGA DE DATOS
        // --------------------------------------------------------------------
        private void CargarRecursos()
        {
            Ejecutar(() =>
            {
                var lista = _servicioRecurso.ObtenerTodo().ToList();
                dgvRecursos.DataSource = lista;

                // Configurar columnas visibles y sus encabezados
                var columnasVisibles = new Dictionary<string, string>
                {
                    { "Id", "ID" },
                    { "Nombre", "Nombre" },
                    { "UnidadMedida", "Unidad" },
                    { "Vigencia", "Vigencia" },
                    { "TipoNombre", "Tipo" } // Renombrando a "Tipo"
                };

                // Columnas que NO deben verse
                var columnasOcultas = new List<string>
                {
                    "TipoId"
                };

                ConfigurarColumnas(dgvRecursos, columnasVisibles, columnasOcultas);
            });
        }

        private void CargarTipos()
        {
            Ejecutar(() =>
            {
                var tipos = _servicioTipoRecurso.ObtenerTodo().ToList();

                var listaBuscar = tipos.Prepend(new TipoRecursoDto { Id = 0, Nombre = "Seleccione" }).ToList();
                var listaEditar = tipos.Prepend(new TipoRecursoDto { Id = 0, Nombre = "Seleccione" }).ToList();

                CargarCombo(cbBuscarCategoria, listaBuscar, "Nombre", "Id", seleccionarPrimero: true);
                CargarCombo(cbTipoRecurso, listaEditar, "Nombre", "Id", seleccionarPrimero: true);
            });
        }

        // --------------------------------------------------------------------
        // BUSCAR
        // --------------------------------------------------------------------
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Ejecutar(() =>
            {
                string nombre = txtBuscarNombre.Text.Trim();
                int tipoId = (int)(cbBuscarCategoria.SelectedValue ?? 0);

                List<RecursoDto> resultados;

                if (!string.IsNullOrEmpty(nombre))
                    resultados = _servicioRecurso.BuscarPorNombre(nombre).ToList();

                else if (tipoId > 0)
                    resultados = _servicioRecurso.BuscarPorTipo(tipoId).ToList();

                else
                    resultados = _servicioRecurso.ObtenerTodo().ToList();

                dgvRecursos.DataSource = resultados;

                if (resultados.Count == 0)
                    MostrarInfo("No se encontraron resultados.");
            });
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Clear();
            cbBuscarCategoria.SelectedIndex = 0;
        }

        // --------------------------------------------------------------------
        // CRUD
        // --------------------------------------------------------------------
        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            dgvRecursos.ClearSelection();
            LimpiarControles(this);

            if (SesionActual.Instancia.EsOperador())
            {
                // Puede crear, así que habilitar
                SetCamposEdicion(true);
                btnGuardar.Enabled = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles(this);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (SesionActual.Instancia.EsOperador() && !string.IsNullOrEmpty(txtId.Text))
            {
                MostrarError("No tiene permiso para actualizar recursos.");
                return;
            }

            Ejecutar(() =>
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MostrarError("Ingrese el nombre del recurso.");
                    return;
                }

                if (cbTipoRecurso.SelectedIndex <= 0)
                {
                    MostrarError("Seleccione un tipo de recurso.");
                    return;
                }

                var dto = new RecursoDto
                {
                    Id = string.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text),
                    Nombre = txtNombre.Text.Trim(),
                    TipoId = (int)cbTipoRecurso.SelectedValue,
                    UnidadMedida = txtUnidadMedida.Text.Trim(),
                    Vigencia = string.IsNullOrWhiteSpace(txtVigencia.Text) ? 'A' : txtVigencia.Text.Trim()[0]
                };

                bool exito;

                if (dto.Id == 0)
                {
                    exito = _servicioRecurso.Crear(dto) > 0;
                    if (exito) MostrarInfo("Recurso agregado correctamente.");
                }
                else
                {
                    exito = _servicioRecurso.Actualizar(dto);
                    if (exito) MostrarInfo("Recurso actualizado correctamente.");
                }

                if (exito)
                {
                    CargarRecursos();
                    dgvRecursos.ClearSelection();
                    LimpiarControles(this);
                }
                else
                {
                    MostrarError("No se pudo guardar el recurso.");
                }
            });
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (SesionActual.Instancia.EsOperador())
            {
                MostrarError("No tiene permiso para eliminar recursos.");
                return;
            }

            Ejecutar(() =>
            {
                if (dgvRecursos.CurrentRow == null)
                {
                    MostrarError("Seleccione un recurso para eliminar.");
                    return;
                }

                int id = Convert.ToInt32(dgvRecursos.CurrentRow.Cells["Id"].Value);

                if (!Confirmar("¿Está seguro que desea eliminar este recurso?"))
                    return;

                bool exito = _servicioRecurso.Eliminar(id);

                if (exito)
                {
                    MostrarInfo("Recurso eliminado correctamente.");
                    CargarRecursos();
                    LimpiarControles(this);
                }
                else
                {
                    MostrarError("No se pudo eliminar el recurso.");
                }
            });
        }
    }
}