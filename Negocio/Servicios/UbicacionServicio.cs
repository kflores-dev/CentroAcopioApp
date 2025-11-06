using System.Collections.Generic;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;
using CentroAcopioApp.Negocio.Transaccion;
using CentroAcopioApp.Negocio.Validaciones;

namespace CentroAcopioApp.Negocio.Servicios
{
    public class UbicacionServicio
    {
        private readonly IUbicacionRepositorio _repositorio;

        public UbicacionServicio(IUbicacionRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<UbicacionDto> ObtenerTodo()
        {
            return _repositorio.ObtenerTodo();
        }

        public UbicacionDto ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

            var dto = _repositorio.ObtenerPorId(id);
            if (dto == null)
                throw new ExcepcionServicio("No se encontró la ubicación especificada.");

            return dto;
        }

        public IEnumerable<UbicacionDto> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ExcepcionValidacion("Debe proporcionar un nombre para la búsqueda.");

            return _repositorio.ObtenerPorNombre(nombre);
        }

        public int Crear(UbicacionDto dto)
        {
            UbicacionValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var id = _repositorio.Insertar(dto);
                    return id;
                });
            }
        }

        public bool Actualizar(UbicacionDto dto)
        {
            UbicacionValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var actualizado = _repositorio.Actualizar(dto);
                    if (!actualizado)
                        throw new ExcepcionServicio("No se pudo actualizar la ubicación.");
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
                        throw new ExcepcionServicio("No se encontró la ubicación para eliminar.");
                    return eliminado;
                });
            }
        }
    }
}