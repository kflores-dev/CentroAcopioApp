using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface ISolicitudRepositorio: IRepositorioBase<SolicitudDto>
    {
        IEnumerable<SolicitudDto> ObtenerPorEstado(string estado);
        IEnumerable<SolicitudDto> ObtenerPorPrioridad(string prioridad);
    }
}