using System;
using System.Collections.Generic;
using System.Linq;
using CentroAcopioApp.Datos.Repositorios.Implementaciones;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Negocio.Seguridad;
using CentroAcopioApp.Negocio.Servicios;

namespace CentroAcopioApp.Presentacion.Formularios.GestionRecursos
{
    public partial class FormTipoRecurso : BaseForm
    {
        private readonly TipoRecursoServicio _servicio;

        public FormTipoRecurso()
        {
            InitializeComponent();
            _servicio = new TipoRecursoServicio(new TipoRecursoRepositorio());
        }

        private void FormTipoRecurso_Load(object sender, EventArgs e)
        {
            Ejecutar(() =>
            {
                CargarTipos();
                AplicarPermisos();
            });
        }

        // ============================================================
        // PERMISOS POR ROL
        // ============================================================
        private void AplicarPermisos()
        {
            if (SesionActual.Instancia.EsOperador())
            {
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = false;
                btnNuevoRegistro.Enabled = true;

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
            txtVigencia.Enabled = habilitar;
            txtId.Enabled = false; // Siempre readonly
        }

        // ============================================================
        // CARGA DE DATOS
        // ============================================================
        private void CargarTipos()
        {
            Ejecutar(() =>
            {
                var lista = _servicio.ObtenerTodo().ToList();
                dgvTipoRecurso.DataSource = lista;

                var columnasVisibles = new Dictionary<string, string>
                {
                    { "Id", "ID" },
                    { "Nombre", "Nombre" },
                    { "Vigencia", "Vigencia" }
                };

                var columnasOcultas = new List<string>();

                ConfigurarColumnas(dgvTipoRecurso, columnasVisibles, columnasOcultas);
            });
        }

        // ============================================================
        // SELECCIÓN DEL DGV
        // ============================================================
        private void dgvTipoRecurso_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTipoRecurso.CurrentRow?.DataBoundItem is TipoRecursoDto dto)
            {
                CargarCampos(dto);

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

        private void CargarCampos(TipoRecursoDto dto)
        {
            txtId.Text = dto.Id.ToString();
            txtNombre.Text = dto.Nombre;
            txtVigencia.Text = dto.Vigencia.ToString();
        }

        // ============================================================
        // BUSCAR
        // ============================================================
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Ejecutar(() =>
            {
                string nombre = txtBuscarNombre.Text.Trim();
                List<TipoRecursoDto> resultados;

                if (!string.IsNullOrEmpty(nombre))
                    resultados = _servicio.BuscarPorNombre(nombre).ToList();
                else
                    resultados = _servicio.ObtenerTodo().ToList();

                dgvTipoRecurso.DataSource = resultados;

                if (resultados.Count == 0)
                    MostrarInfo("No se encontraron resultados.");
            });
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Clear();
        }

        // ============================================================
        // NUEVO REGISTRO
        // ============================================================
        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            dgvTipoRecurso.ClearSelection();
            LimpiarControles(this);

            if (SesionActual.Instancia.EsOperador())
            {
                SetCamposEdicion(true);
                btnGuardar.Enabled = true;
            }
        }

        private void btnLimpio_Click(object sender, EventArgs e)
        {
            LimpiarControles(this);
        }

        // ============================================================
        // GUARDAR
        // ============================================================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (SesionActual.Instancia.EsOperador() && !string.IsNullOrEmpty(txtId.Text))
            {
                MostrarError("No tiene permiso para actualizar tipos de recurso.");
                return;
            }

            Ejecutar(() =>
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MostrarError("Ingrese un nombre.");
                    return;
                }

                var dto = new TipoRecursoDto
                {
                    Id = string.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text),
                    Nombre = txtNombre.Text.Trim(),
                    Vigencia = string.IsNullOrWhiteSpace(txtVigencia.Text) ? 'A' : txtVigencia.Text.Trim()[0]
                };

                bool exito;

                if (dto.Id == 0)
                {
                    exito = _servicio.Crear(dto) > 0;
                    if (exito) MostrarInfo("Tipo de recurso creado correctamente.");
                }
                else
                {
                    exito = _servicio.Actualizar(dto);
                    if (exito) MostrarInfo("Tipo de recurso actualizado correctamente.");
                }

                if (exito)
                {
                    CargarTipos();
                    dgvTipoRecurso.ClearSelection();
                    LimpiarControles(this);
                }
                else
                {
                    MostrarError("No se pudo guardar.");
                }
            });
        }

        // ============================================================
        // ELIMINAR
        // ============================================================
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (SesionActual.Instancia.EsOperador())
            {
                MostrarError("No tiene permiso para eliminar tipos de recurso.");
                return;
            }

            Ejecutar(() =>
            {
                if (dgvTipoRecurso.CurrentRow == null)
                {
                    MostrarError("Seleccione un registro para eliminar.");
                    return;
                }

                int id = Convert.ToInt32(dgvTipoRecurso.CurrentRow.Cells["Id"].Value);

                if (!Confirmar("¿Está seguro que desea eliminar este tipo de recurso?"))
                    return;

                bool exito = _servicio.Eliminar(id);

                if (exito)
                {
                    MostrarInfo("Tipo de recurso eliminado correctamente.");
                    CargarTipos();
                    LimpiarControles(this);
                }
                else
                {
                    MostrarError("No se pudo eliminar.");
                }
            });
        }
    }
}