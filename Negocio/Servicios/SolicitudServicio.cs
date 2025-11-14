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
    public class SolicitudServicio
    {
        private readonly ISolicitudRepositorio _repositorio;

        public SolicitudServicio(ISolicitudRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<SolicitudDto> ObtenerTodo()
        {
            var lista = _repositorio.ObtenerTodo();
            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public SolicitudDto ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

            var dto = _repositorio.ObtenerPorId(id);

            dto = FiltroRolHelper.FiltrarEntidadPorRol(dto);

            if (dto == null)
                throw new ExcepcionServicio("No se encontró la solicitud especificada.");

            return dto;
        }

        public IEnumerable<SolicitudDto> BuscarPorEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new ExcepcionValidacion("Debe proporcionar un estado válido.");

            var lista = _repositorio.ObtenerPorEstado(estado);

            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public IEnumerable<SolicitudDto> BuscarPorPrioridad(string prioridad)
        {
            if (string.IsNullOrWhiteSpace(prioridad))
                throw new ExcepcionValidacion("Debe proporcionar una prioridad válida.");

            var lista = _repositorio.ObtenerPorPrioridad(prioridad);

            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public int Crear(SolicitudDto dto)
        {
            SolicitudValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var id = _repositorio.Insertar(dto);

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Crear",
                        entidad: "Solicitud",
                        entidadId: id,
                        descripcion: $"Se creó una nueva solicitud con prioridad {dto.Prioridad} y estado {dto.Estado}."
                    );

                    return id;
                });
            }
        }

        public bool Actualizar(SolicitudDto dto)
        {
            SolicitudValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var actualizado = _repositorio.Actualizar(dto);
                    if (!actualizado)
                        throw new ExcepcionServicio("No se pudo actualizar la solicitud.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Actualizar",
                        entidad: "Solicitud",
                        entidadId: dto.Id,
                        descripcion: $"Se actualizó la solicitud con ID {dto.Id}."
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
                        throw new ExcepcionServicio("No se encontró la solicitud para eliminar.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Eliminar",
                        entidad: "Solicitud",
                        entidadId: id,
                        descripcion: $"Se eliminó la solicitud con ID {id}."
                    );

                    return eliminado;
                });
            }
        }
    }
}