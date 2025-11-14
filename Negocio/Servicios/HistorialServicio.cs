using System;
using System.Collections.Generic;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;
using CentroAcopioApp.Negocio.Validaciones;

namespace CentroAcopioApp.Negocio.Servicios
{
    public class HistorialServicio
    {
        private static IHistorialRepositorio _repositorioEstatico;

        public HistorialServicio(IHistorialRepositorio repositorio)
        {
            _repositorioEstatico = repositorio;
        }

        public IEnumerable<HistorialDto> ObtenerTodo()
        {
            return _repositorioEstatico.ObtenerTodo();
        }

        public HistorialDto ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

            var dto = _repositorioEstatico.ObtenerPorId(id);
            if (dto == null)
                throw new ExcepcionServicio("No se encontró el registro en el historial.");

            return dto;
        }

        public IEnumerable<HistorialDto> ObtenerPorUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un ID de usuario válido.");

            return _repositorioEstatico.ObtenerPorUsuario(usuarioId);
        }

        public IEnumerable<HistorialDto> ObtenerPorEntidad(string entidad, int entidadId)
        {
            if (string.IsNullOrWhiteSpace(entidad))
                throw new ExcepcionValidacion("Debe proporcionar una entidad válida.");

            if (entidadId <= 0)
                throw new ExcepcionValidacion("Debe proporcionar un ID de entidad válido.");

            return _repositorioEstatico.ObtenerPorEntidad(entidad, entidadId);
        }

        public static int Registrar(
            int usuarioId,
            string accion,
            string entidad,
            int entidadId,
            string descripcion = null)
        {
            var dto = new HistorialDto
            {
                UsuarioId = usuarioId,
                Accion = accion,
                Entidad = entidad,
                EntidadId = entidadId,
                FechaHora = DateTime.Now,
                Descripcion = descripcion,
                Vigencia = 'A'
            };

            HistorialValidador.Validar(dto);

            if (_repositorioEstatico == null)
                throw new InvalidOperationException("HistorialServicio no ha sido inicializado.");

            return _repositorioEstatico.Insertar(dto);
        }
    }
}