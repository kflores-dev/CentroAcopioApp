using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IRecursoRepositorio : IRepositorioBase<RecursoDto>
    {
        IEnumerable<RecursoDto> ObtenerPorNombre(string nombre);
        IEnumerable<RecursoDto> ObtenerPorTipo(int tipoId);
    }
}