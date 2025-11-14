using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IDetalleSolicitudRepositorio : IRepositorioBase<DetalleSolicitudDto>
    {
        IEnumerable<DetalleSolicitudDto> ObtenerPorSolicitud(int solicitudId);
        IEnumerable<DetalleSolicitudDto> ObtenerPorRecurso(int recursoId);
    }
}