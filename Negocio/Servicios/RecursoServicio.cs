using System.Collections.Generic;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;
using CentroAcopioApp.Negocio.Transaccion;
using CentroAcopioApp.Negocio.Validaciones;

namespace CentroAcopioApp.Negocio.Servicios
{
    public class RecursoServicio
    {
        private readonly IRecursoRepositorio _repositorio;

        public RecursoServicio(IRecursoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<RecursoDto> ObtenerTodo()
        {
            return _repositorio.ObtenerTodo();
        }

        public RecursoDto ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

            var dto = _repositorio.ObtenerPorId(id);
            if (dto == null)
                throw new ExcepcionServicio("No se encontró el recurso especificado.");

            return dto;
        }

        public IEnumerable<RecursoDto> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ExcepcionValidacion("Debe proporcionar un nombre para la búsqueda.");

            return _repositorio.ObtenerPorNombre(nombre);
        }

        public IEnumerable<RecursoDto> BuscarPorTipo(int tipoId)
        {
            if (tipoId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un tipo de recurso válido.");

            return _repositorio.ObtenerPorTipo(tipoId);
        }

        public int Crear(RecursoDto dto)
        {
            RecursoValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var id = _repositorio.Insertar(dto);
                    return id;
                });
            }
        }

        public bool Actualizar(RecursoDto dto)
        {
            RecursoValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var actualizado = _repositorio.Actualizar(dto);
                    if (!actualizado)
                        throw new ExcepcionServicio("No se pudo actualizar el recurso.");
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
                        throw new ExcepcionServicio("No se encontró el recurso para eliminar.");
                    return eliminado;
                });
            }
        }
    }
}