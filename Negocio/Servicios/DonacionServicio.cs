using System;
using System.Collections.Generic;
using CentroAcopioApp.Datos.Repositorios.Implementaciones;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;
using CentroAcopioApp.Negocio.Seguridad;
using CentroAcopioApp.Negocio.Transaccion;
using CentroAcopioApp.Negocio.Validaciones;
using CentroAcopioApp.Utilidades;

namespace CentroAcopioApp.Negocio.Servicios
{
    public class DonacionServicio
    {
        private readonly DonacionRepositorio _repositorio;

        public DonacionServicio(DonacionRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<DonacionDto> ObtenerTodo()
        {
            var lista = _repositorio.ObtenerTodo();
            return FiltroRolHelper.FiltrarPorRol(lista); 
        }

        public DonacionDto ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

            var dto = _repositorio.ObtenerPorId(id);

            dto = FiltroRolHelper.FiltrarEntidadPorRol(dto);

            if (dto == null)
                throw new ExcepcionServicio("No se encontró la donación especificada.");

            return dto;
        }

        public IEnumerable<DonacionDto> BuscarPorProveedor(int proveedorId)
        {
            if (proveedorId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un ID de proveedor válido.");

            var lista = _repositorio.ObtenerPorProveedor(proveedorId);
            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public IEnumerable<DonacionDto> BuscarPorFecha(DateTime fecha)
        {
            var lista = _repositorio.ObtenerPorFecha(fecha);
            return FiltroRolHelper.FiltrarPorRol(lista);
        }

        public int Crear(DonacionDto dto)
        {
            DonacionValidador.Validar(dto); 

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var id = _repositorio.Insertar(dto);

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Crear",
                        entidad: "Donacion",
                        entidadId: id,
                        descripcion:
                        $"Se creó una donación del proveedor {dto.ProveedorNombre} con fecha {dto.Fecha:yyyy-MM-dd}."
                    );

                    return id;
                });
            }
        }

        public bool Actualizar(DonacionDto dto)
        {
            DonacionValidador.Validar(dto);

            using (var tx = new TransaccionManager())
            {
                return tx.EjecutarResultado(() =>
                {
                    var actualizado = _repositorio.Actualizar(dto);
                    if (!actualizado)
                        throw new ExcepcionServicio("No se pudo actualizar la donación.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Actualizar",
                        entidad: "Donacion",
                        entidadId: dto.Id,
                        descripcion:
                        $"Se actualizó la donación del proveedor {dto.ProveedorNombre} con fecha {dto.Fecha:yyyy-MM-dd}."
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
                        throw new ExcepcionServicio("No se encontró la donación para eliminar.");

                    HistorialServicio.Registrar(
                        usuarioId: SesionActual.Instancia.UsuarioId,
                        accion: "Eliminar",
                        entidad: "Donacion",
                        entidadId: id,
                        descripcion: $"Se eliminó la donación con ID {id}."
                    );

                    return eliminado;
                });
            }
        }
    }
}