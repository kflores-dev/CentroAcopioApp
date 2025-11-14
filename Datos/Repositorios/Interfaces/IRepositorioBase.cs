using System.Collections.Generic;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IRepositorioBase<T>
    {
        IEnumerable<T> ObtenerTodo();
        T ObtenerPorId(int id);
        int Insertar(T dto);
        bool Actualizar(T dto);
        bool Eliminar(int id);
    }
}