using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IProveedorRepositorio : IRepositorioBase<ProveedorDto>
    {
        IEnumerable<ProveedorDto> ObtenerPorNombre(string nombre);
    }
}