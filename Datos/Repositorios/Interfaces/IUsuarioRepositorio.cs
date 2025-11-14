using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioBase<UsuarioDto>
    {
        IEnumerable<UsuarioDto> ObtenerPorNombre(string nombre);
        UsuarioDto ObtenerPorUsername(string username);
    }
}