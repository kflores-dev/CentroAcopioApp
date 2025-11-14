using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Validaciones
{
    public class DetalleSolicitudValidador
    {
        public static void Validar(DetalleSolicitudDto dto)
        {
            if (dto == null)
                throw new ExcepcionValidacion("El objeto DetalleSolicitudDto no puede ser nulo.");

            if (dto.SolicitudId <= 0)
                throw new ExcepcionValidacion("La solicitud es obligatoria y debe ser válida.");

            if (dto.RecursoId <= 0)
                throw new ExcepcionValidacion("El recurso es obligatorio y debe ser válido.");

            if (dto.CantidadSolicitada <= 0)
                throw new ExcepcionValidacion("La cantidad solicitada debe ser mayor que cero.");

            if (dto.CantidadEntregada < 0)
                throw new ExcepcionValidacion("La cantidad entregada no puede ser negativa.");

            if (dto.Vigencia != 'A' && dto.Vigencia != 'I')
                throw new ExcepcionValidacion("La vigencia debe ser 'A' (activa) o 'I' (inactiva).");
        }
    }
}