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
    public class DetalleSolicitudServicio
    {
        private readonly IDetalleSolicitudRepositorio _repositorio;

        public DetalleSolicitudServicio(IDetalleSolicitudRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<DetalleSolicitudDto> ObtenerTodo()
        {
            var lista = _repositorio.ObtenerTodo();
            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public DetalleSolicitudDto ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

            var dto = _repositorio.ObtenerPorId(id);

            dto = FiltroRolHelper.FiltrarEntidadPorRol(dto);

            if (dto == null)
                throw new ExcepcionServicio("No se encontró el detalle de solicitud especificado.");

            return dto;
        }

        public IEnumerable<DetalleSolicitudDto> BuscarPorSolicitud(int solicitudId)
        {
            if (solicitudId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un ID de solicitud válido.");

            var lista = _repositorio.ObtenerPorSolicitud(solicitudId);

            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public IEnumerable<DetalleSolicitudDto> BuscarPorRecurso(int recursoId)
        {
            if (recursoId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un ID de recurso válido.");

            var lista = _repositorio.ObtenerPorRecurso(recursoId);

            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public int Crear(DetalleSolicitudDto dto)
        {
            DetalleSolicitudValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var id = _repositorio.Insertar(dto);

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Crear",
                        entidad: "DetalleSolicitud",
                        entidadId: id,
                        descripcion:
                        $"Se creó el detalle de la solicitud {dto.SolicitudNombre} para " +
                        $"el recurso {dto.RecursoNombre} (Cantidad solicitada: {dto.CantidadSolicitada})."
                    );

                    return id;
                });
            }
        }

        public bool Actualizar(DetalleSolicitudDto dto)
        {
            DetalleSolicitudValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var actualizado = _repositorio.Actualizar(dto);
                    if (!actualizado)
                        throw new ExcepcionServicio("No se pudo actualizar el detalle de solicitud.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Actualizar",
                        entidad: "DetalleSolicitud",
                        entidadId: dto.Id,
                        descripcion:
                        $"Se actualizó el detalle de la solicitud {dto.SolicitudNombre} para " +
                        $"el recurso {dto.RecursoNombre} (Cantidad solicitada: {dto.CantidadSolicitada}, " +
                        $"Entregada: {dto.CantidadEntregada})."
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
                        throw new ExcepcionServicio("No se encontró el detalle de solicitud para eliminar.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Eliminar",
                        entidad: "DetalleSolicitud",
                        entidadId: id,
                        descripcion: $"Se eliminó el detalle de solicitud con ID {id}."
                    );

                    return eliminado;
                });
            }
        }
    }
}