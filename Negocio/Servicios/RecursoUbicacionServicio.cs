using System.Collections.Generic;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;
using CentroAcopioApp.Negocio.Transaccion;
using CentroAcopioApp.Negocio.Validaciones;

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
            return _repositorio.ObtenerTodo();
        }

        public RecursoUbicacionDto ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

            var dto = _repositorio.ObtenerPorId(id);
            if (dto == null)
                throw new ExcepcionServicio("No se encontró el registro especificado de recurso en ubicación.");

            return dto;
        }

        public IEnumerable<RecursoUbicacionDto> BuscarPorRecurso(int recursoId)
        {
            if (recursoId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un ID de recurso válido.");

            return _repositorio.ObtenerPorRecurso(recursoId);
        }

        public IEnumerable<RecursoUbicacionDto> BuscarPorUbicacion(int ubicacionId)
        {
            if (ubicacionId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un ID de ubicación válido.");

            return _repositorio.ObtenerPorUbicacion(ubicacionId);
        }

        public int Crear(RecursoUbicacionDto dto)
        {
            RecursoUbicacionValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var id = _repositorio.Insertar(dto);
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
                    return eliminado;
                });
            }
        }
    }
}