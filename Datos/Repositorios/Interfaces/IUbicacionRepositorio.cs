using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IUbicacionRepositorio
    {
        IEnumerable<UbicacionDto> ObtenerTodo();
        UbicacionDto ObtenerPorId(int id);
        IEnumerable<UbicacionDto> ObtenerPorNombre(string nombre);
        int Insertar(UbicacionDto dto);
        bool Actualizar(UbicacionDto dto);
        bool Eliminar(int id);
    }
}