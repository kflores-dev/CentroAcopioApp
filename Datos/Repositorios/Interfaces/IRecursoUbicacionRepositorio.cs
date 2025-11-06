using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IRecursoUbicacionRepositorio
    {
        IEnumerable<RecursoUbicacionDto> ObtenerTodo();
        RecursoUbicacionDto ObtenerPorId(int id);
        IEnumerable<RecursoUbicacionDto> ObtenerPorRecurso(int recursoId);
        IEnumerable<RecursoUbicacionDto> ObtenerPorUbicacion(int ubicacionId);
        int Insertar(RecursoUbicacionDto dto);
        bool Actualizar(RecursoUbicacionDto dto);
        bool Eliminar(int id);
    }
}