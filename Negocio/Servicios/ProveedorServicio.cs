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
    public class ProveedorServicio
    {
        private readonly IProveedorRepositorio _repositorio;

        public ProveedorServicio(IProveedorRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<ProveedorDto> ObtenerTodo()
        {
            var lista = _repositorio.ObtenerTodo();

            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public ProveedorDto ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

            var dto = _repositorio.ObtenerPorId(id);

            dto = FiltroRolHelper.FiltrarEntidadPorRol(dto);

            if (dto == null)
                throw new ExcepcionServicio("No se encontró el proveedor especificado.");

            return dto;
        }

        public IEnumerable<ProveedorDto> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ExcepcionValidacion("Debe proporcionar un nombre para la búsqueda.");

            return FiltroRolHelper.FiltrarPorRol(_repositorio.ObtenerPorNombre(nombre));
        }

        public int Crear(ProveedorDto dto)
        {
            ProveedorValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var id = _repositorio.Insertar(dto);

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Crear",
                        entidad: "Proveedor",
                        entidadId: id,
                        descripcion: $"Se creó un proveedor: {dto.Nombre}."
                    );

                    return id;
                });
            }
        }

        public bool Actualizar(ProveedorDto dto)
        {
            ProveedorValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var actualizado = _repositorio.Actualizar(dto);
                    if (!actualizado)
                        throw new ExcepcionServicio("No se pudo actualizar el proveedor.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Actualizar",
                        entidad: "Proveedor",
                        entidadId: dto.Id,
                        descripcion: $"Se actualizó el proveedor: {dto.Nombre}."
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
                        throw new ExcepcionServicio("No se encontró el proveedor para eliminar.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Eliminar",
                        entidad: "Proveedor",
                        entidadId: id,
                        descripcion: $"Se eliminó el proveedor con ID {id}."
                    );

                    return eliminado;
                });
            }
        }
    }
}