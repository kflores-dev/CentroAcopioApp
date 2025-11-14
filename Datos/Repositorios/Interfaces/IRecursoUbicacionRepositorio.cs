using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IRecursoUbicacionRepositorio : IRepositorioBase<RecursoUbicacionDto>
    {
        IEnumerable<RecursoUbicacionDto> ObtenerPorRecurso(int recursoId);
        IEnumerable<RecursoUbicacionDto> ObtenerPorUbicacion(int ubicacionId);
    }
}