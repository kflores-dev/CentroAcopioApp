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
    public class RecursoUbicacionServicio
    {
        private readonly IRecursoUbicacionRepositorio _repositorio;

        public RecursoUbicacionServicio(IRecursoUbicacionRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<RecursoUbicacionDto> ObtenerTodo()
        {
            var lista = _repositorio.ObtenerTodo();
            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public RecursoUbicacionDto ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

            var dto = _repositorio.ObtenerPorId(id);

            dto = FiltroRolHelper.FiltrarEntidadPorRol(dto);

            if (dto == null)
                throw new ExcepcionServicio("No se encontró el registro especificado de recurso en ubicación.");

            return dto;
        }

        public IEnumerable<RecursoUbicacionDto> BuscarPorRecurso(int recursoId)
        {
            if (recursoId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un ID de recurso válido.");

            var lista = _repositorio.ObtenerPorRecurso(recursoId);

            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public IEnumerable<RecursoUbicacionDto> BuscarPorUbicacion(int ubicacionId)
        {
            if (ubicacionId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un ID de ubicación válido.");

            var lista = _repositorio.ObtenerPorUbicacion(ubicacionId);

            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public int Crear(RecursoUbicacionDto dto)
        {
            RecursoUbicacionValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var id = _repositorio.Insertar(dto);

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Crear",
                        entidad: "RecursoUbicacion",
                        entidadId: id,
                        descripcion:
                        $"Se creó la asignación del recurso {dto.RecursoId} en la ubicación {dto.UbicacionId}."
                    );

                    return id;
                });
            }
        }

        public bool Actualizar(RecursoUbicacionDto dto)
        {
            RecursoUbicacionValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var actualizado = _repositorio.Actualizar(dto);
                    if (!actualizado)
                        throw new ExcepcionServicio("No se pudo actualizar el registro de recurso en ubicación.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Actualizar",
                        entidad: "RecursoUbicacion",
                        entidadId: dto.Id,
                        descripcion:
                        $"Se actualizó la asignación del recurso {dto.RecursoId} en la ubicación {dto.UbicacionId}."
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
                        throw new ExcepcionServicio(
                            "No se encontró el registro de recurso en ubicación para eliminar.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Eliminar",
                        entidad: "RecursoUbicacion",
                        entidadId: id,
                        descripcion: $"Se eliminó la asignación de recurso en ubicación con ID {id}."
                    );

                    return eliminado;
                });
            }
        }
    }
}