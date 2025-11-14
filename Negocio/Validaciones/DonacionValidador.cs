using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Validaciones
{
    public class DonacionValidador
    {
        public static void Validar(DonacionDto dto)
        {
            if (dto == null)
                throw new ExcepcionValidacion("El objeto DonacionDto no puede ser nulo.");

            if (dto.ProveedorId <= 0)
                throw new ExcepcionValidacion("El proveedor es obligatorio y debe ser válido.");

            if (dto.Fecha == default)
                throw new ExcepcionValidacion("La fecha de donación es obligatoria.");

            if (!string.IsNullOrWhiteSpace(dto.Observaciones) && dto.Observaciones.Length > 1000)
                throw new ExcepcionValidacion("Las observaciones no pueden superar los 1000 caracteres.");

            if (dto.Vigencia != 'A' && dto.Vigencia != 'I')
                throw new ExcepcionValidacion("La vigencia debe ser 'A' (activa) o 'I' (inactiva).");
        }
    }
}