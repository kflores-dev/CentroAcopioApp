using System.Collections.Generic;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;
using CentroAcopioApp.Negocio.Seguridad;
using CentroAcopioApp.Negocio.Transaccion;
using CentroAcopioApp.Negocio.Validaciones;
using CentroAcopioApp.Utilidades;

namespace CentroAcopioApp.Negocio.Servicios
{
    public class TipoRecursoServicio
    {
        private readonly ITipoRecursoRepositorio _repositorio;

        public TipoRecursoServicio(ITipoRecursoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<TipoRecursoDto> ObtenerTodo()
        {
            var lista = _repositorio.ObtenerTodo();
            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public TipoRecursoDto ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

            var dto = _repositorio.ObtenerPorId(id);

            dto = FiltroRolHelper.FiltrarEntidadPorRol(dto);

            if (dto == null)
                throw new ExcepcionServicio("No se encontró el tipo de recurso especificado.");

            return dto;
        }

        public IEnumerable<TipoRecursoDto> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ExcepcionValidacion("Debe proporcionar un nombre para la búsqueda.");

            var lista = _repositorio.ObtenerPorNombre(nombre);

            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public int Crear(TipoRecursoDto dto)
        {
            TipoRecursoValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var id = _repositorio.Insertar(dto);

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Crear",
                        entidad: "TipoRecurso",
                        entidadId: id,
                        descripcion: $"Se creó el tipo de recurso: {dto.Nombre}."
                    );

                    return id;
                });
            }
        }

        public bool Actualizar(TipoRecursoDto dto)
        {
            TipoRecursoValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var actualizado = _repositorio.Actualizar(dto);
                    if (!actualizado)
                        throw new ExcepcionServicio("No se pudo actualizar el tipo de recurso.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Actualizar",
                        entidad: "TipoRecurso",
                        entidadId: dto.Id,
                        descripcion: $"Se actualizó el tipo de recurso: {dto.Nombre}."
                    );

                    return actualizado;
                });
            }
        }

        public bool Eliminar(int id)
        {
            if (id <= 0)
                throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var eliminado = _repositorio.Eliminar(id);
                    if (!eliminado)
                        throw new ExcepcionServicio("No se encontró el tipo de recurso para eliminar.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Eliminar",
                        entidad: "TipoRecurso",
                        entidadId: id,
                        descripcion: $"Se eliminó el tipo de recurso con ID {id}."
                    );

                    return eliminado;
                });
            }
        }
    }
}