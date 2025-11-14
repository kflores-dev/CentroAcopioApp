using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Validaciones
{
    public class DetalleDonacionValidador
    {
        public static void Validar(DetalleDonacionDto dto)
        {
            if (dto == null)
                throw new ExcepcionValidacion("El objeto DetalleDonacionDto no puede ser nulo.");

            if (dto.DonacionId <= 0)
                throw new ExcepcionValidacion("La donación es obligatoria y debe ser válida.");

            if (dto.RecursoId <= 0)
                throw new ExcepcionValidacion("El recurso es obligatorio y debe ser válido.");

            if (dto.UbicacionId <= 0)
                throw new ExcepcionValidacion("La ubicación es obligatoria y debe ser válida.");

            if (dto.CantidadDonada <= 0)
                throw new ExcepcionValidacion("La cantidad donada debe ser mayor que cero.");

            if (dto.Vigencia != 'A' && dto.Vigencia != 'I')
                throw new ExcepcionValidacion("La vigencia debe ser 'A' (activa) o 'I' (inactiva).");
        }
    }
}