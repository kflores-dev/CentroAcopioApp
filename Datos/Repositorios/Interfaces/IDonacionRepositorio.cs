using System;
using System.Collections.Generic;
using CentroAcopioApp.DTO;

namespace CentroAcopioApp.Datos.Repositorios.Interfaces
{
    public interface IDonacionRepositorio: IRepositorioBase<DonacionDto>
    {
        IEnumerable<DonacionDto> ObtenerPorProveedor(int proveedorId);
        IEnumerable<DonacionDto> ObtenerPorFecha(DateTime fecha);
    }
}