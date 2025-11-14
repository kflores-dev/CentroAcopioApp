using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IDetalleDonacionRepositorio : IRepositorioBase<DetalleDonacionDto>
    {
        IEnumerable<DetalleDonacionDto> ObtenerPorDonacion(int donacionId);
        IEnumerable<DetalleDonacionDto> ObtenerPorRecurso(int recursoId);
        IEnumerable<DetalleDonacionDto> ObtenerPorUbicacion(int ubicacionId);
    }
}