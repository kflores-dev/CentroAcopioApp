using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Validaciones
{
    public class UbicacionValidador
    {
        public static void Validar(UbicacionDto dto)
        {
            if (dto == null)
                throw new ExcepcionValidacion("El objeto UbicacionDto no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ExcepcionValidacion("El nombre de la ubicación es obligatorio.");

            if (dto.Nombre.Length > 100)
                throw new ExcepcionValidacion("El nombre no puede superar los 100 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Direccion))
                throw new ExcepcionValidacion("La dirección de la ubicación es obligatoria.");

            if (dto.Direccion.Length > 100)
                throw new ExcepcionValidacion("La dirección no puede superar los 100 caracteres.");

            if (dto.Vigencia != 'A' && dto.Vigencia != 'I')
                throw new ExcepcionValidacion("La vigencia debe ser 'A' (activa) o 'I' (inactiva).");
        }
    }
}