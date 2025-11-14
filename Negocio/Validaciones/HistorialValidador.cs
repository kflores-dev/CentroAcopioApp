using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Validaciones
{
    public class HistorialValidador
    {
        public static void Validar(HistorialDto dto)
        {
            if (dto == null)
                throw new ExcepcionValidacion("El objeto HistorialDto no puede ser nulo.");

            if (dto.UsuarioId <= 0)
                throw new ExcepcionValidacion("El usuario es obligatorio y debe ser válido.");

            if (string.IsNullOrWhiteSpace(dto.Accion))
                throw new ExcepcionValidacion("La acción es obligatoria.");
            if (dto.Accion.Length > 50)
                throw new ExcepcionValidacion("La acción no puede superar los 50 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Entidad))
                throw new ExcepcionValidacion("La entidad es obligatoria.");
            if (dto.Entidad.Length > 50)
                throw new ExcepcionValidacion("La entidad no puede superar los 50 caracteres.");

            if (dto.EntidadId <= 0)
                throw new ExcepcionValidacion("El identificador de la entidad debe ser válido.");

            if (dto.FechaHora == default)
                throw new ExcepcionValidacion("La fecha y hora son obligatorias.");

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                throw new ExcepcionValidacion("La descripción es obligatoria.");
            if (dto.Descripcion.Length > 255)
                throw new ExcepcionValidacion("La descripción no puede superar los 255 caracteres.");

            if (dto.Vigencia != 'A' && dto.Vigencia != 'I')
                throw new ExcepcionValidacion("La vigencia debe ser 'A' (activa) o 'I' (inactiva).");
        }
    }
}