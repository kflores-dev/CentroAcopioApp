using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IRecursoRepositorio
    {
        IEnumerable<RecursoDto> ObtenerTodo();
        RecursoDto ObtenerPorId(int id);
        IEnumerable<RecursoDto> ObtenerPorNombre(string nombre);
        IEnumerable<RecursoDto> ObtenerPorTipo(int tipoId);
        int Insertar(RecursoDto dto);
        bool Actualizar(RecursoDto dto);
        bool Eliminar(int id);
    }
}