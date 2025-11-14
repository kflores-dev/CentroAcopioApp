using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IUbicacionRepositorio : IRepositorioBase<UbicacionDto>
    {
        IEnumerable<UbicacionDto> ObtenerPorNombre(string nombre);
    }
}