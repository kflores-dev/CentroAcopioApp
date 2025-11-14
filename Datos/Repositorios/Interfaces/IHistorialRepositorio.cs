using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IHistorialRepositorio
    {
        IEnumerable<HistorialDto> ObtenerTodo();
        HistorialDto ObtenerPorId(int id);
        IEnumerable<HistorialDto> ObtenerPorUsuario(int usuarioId);
        IEnumerable<HistorialDto> ObtenerPorEntidad(string entidad, int entidadId);
        int Insertar(HistorialDto dto);
    }
}