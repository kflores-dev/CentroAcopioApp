using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Validaciones
{
    public class RecursoUbicacionValidador
    {
        public static void Validar(RecursoUbicacionDto dto)
        {
            if (dto == null)
                throw new ExcepcionValidacion("El objeto RecursoUbicacionDto no puede ser nulo.");

            if (dto.RecursoId <= 0)
                throw new ExcepcionValidacion("Debe especificarse un recurso válido.");

            if (dto.UbicacionId <= 0)
                throw new ExcepcionValidacion("Debe especificarse una ubicación válida.");

            if (dto.Cantidad <= 0)
                throw new ExcepcionValidacion("La cantidad debe ser mayor que cero.");

            if (dto.Vigencia != 'A' && dto.Vigencia != 'I')
                throw new ExcepcionValidacion("El campo Vigencia solo puede ser 'A' (Activo) o 'I' (Inactivo).");
        }
    }
}