using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface ITipoRecursoRepositorio
    {
        IEnumerable<TipoRecursoDto> ObtenerTodo();
        TipoRecursoDto ObtenerPorId(int id);
        IEnumerable<TipoRecursoDto> ObtenerPorNombre(string name);
        int Insertar(TipoRecursoDto dto);
        bool Actualizar(TipoRecursoDto dto);
        bool Eliminar(int id);
    }
}