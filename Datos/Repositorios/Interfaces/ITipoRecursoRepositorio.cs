using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface ITipoRecursoRepositorio : IRepositorioBase<TipoRecursoDto>
    {
        IEnumerable<TipoRecursoDto> ObtenerPorNombre(string nombre);
    }
}