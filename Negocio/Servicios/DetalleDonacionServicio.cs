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
    public class DetalleDonacionServicio
    {
        private readonly IDetalleDonacionRepositorio _repositorio;

        public DetalleDonacionServicio(IDetalleDonacionRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<DetalleDonacionDto> ObtenerTodo()
        {
            var lista = _repositorio.ObtenerTodo();

            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public DetalleDonacionDto ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

            var dto = _repositorio.ObtenerPorId(id);

            dto = FiltroRolHelper.FiltrarEntidadPorRol(dto);

            if (dto == null)
                throw new ExcepcionServicio("No se encontró el detalle de donación especificado.");

            return dto;
        }

        public IEnumerable<DetalleDonacionDto> BuscarPorDonacion(int donacionId)
        {
            if (donacionId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un ID de donación válido.");

            var lista = _repositorio.ObtenerPorDonacion(donacionId);

            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public IEnumerable<DetalleDonacionDto> BuscarPorRecurso(int recursoId)
        {
            if (recursoId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un ID de recurso válido.");

            var lista = _repositorio.ObtenerPorRecurso(recursoId);

            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public IEnumerable<DetalleDonacionDto> BuscarPorUbicacion(int ubicacionId)
        {
            if (ubicacionId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un ID de ubicación válido.");

            var lista = _repositorio.ObtenerPorUbicacion(ubicacionId);

            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public int Crear(DetalleDonacionDto dto)
        {
            DetalleDonacionValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var id = _repositorio.Insertar(dto);

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Crear",
                        entidad: "DetalleDonacion",
                        entidadId: id,
                        descripcion: $"Se creó el detalle de donación para el recurso {dto.RecursoNombre} " +
                                     $"en la ubicación {dto.UbicacionNombre} (Cantidad: {dto.CantidadDonada})."
                    );

                    return id;
                });
            }
        }

        public bool Actualizar(DetalleDonacionDto dto)
        {
            DetalleDonacionValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var actualizado = _repositorio.Actualizar(dto);
                    if (!actualizado)
                        throw new ExcepcionServicio("No se pudo actualizar el detalle de donación.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Actualizar",
                        entidad: "DetalleDonacion",
                        entidadId: dto.Id,
                        descripcion: $"Se actualizó el detalle de donación del recurso {dto.RecursoNombre} " +
                                     $"en la ubicación {dto.UbicacionNombre} (Cantidad: {dto.CantidadDonada})."
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
                        throw new ExcepcionServicio("No se encontró el detalle de donación para eliminar.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Eliminar",
                        entidad: "DetalleDonacion",
                        entidadId: id,
                        descripcion: $"Se eliminó el detalle de donación con ID {id}."
                    );

                    return eliminado;
                });
            }
        }
    }
}