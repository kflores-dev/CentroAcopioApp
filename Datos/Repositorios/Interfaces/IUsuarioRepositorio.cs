using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        IEnumerable<UsuarioDto> ObtenerTodo();
        UsuarioDto ObtenerPorId(int id);
        IEnumerable<UsuarioDto> ObtenerPorNombre(string nombre);
        UsuarioDto ObtenerPorUsername(string username);
        int Insertar(UsuarioDto dto);
        bool Actualizar(UsuarioDto dto);
        bool Eliminar(int id);
    }
}