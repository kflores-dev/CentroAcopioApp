using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Validaciones
{
    public class ProveedorValidador
    {
        public static void Validar(ProveedorDto dto)
        {
            if (dto == null)
                throw new ExcepcionValidacion("El objeto ProveedorDto no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ExcepcionValidacion("El nombre del proveedor es obligatorio.");
            if (dto.Nombre.Length > 100)
                throw new ExcepcionValidacion("El nombre del proveedor no puede superar los 100 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Contacto))
                throw new ExcepcionValidacion("El contacto es obligatorio.");
            if (dto.Contacto.Length > 100)
                throw new ExcepcionValidacion("El contacto no puede superar los 100 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Telefono))
                throw new ExcepcionValidacion("El teléfono es obligatorio.");
            if (dto.Telefono.Length > 30)
                throw new ExcepcionValidacion("El teléfono no puede superar los 30 caracteres.");

            if (!string.IsNullOrWhiteSpace(dto.Correo))
            {
                if (dto.Correo.Length > 100)
                    throw new ExcepcionValidacion("El correo electrónico no puede superar los 100 caracteres.");
                if (!dto.Correo.Contains("@") || !dto.Correo.Contains("."))
                    throw new ExcepcionValidacion("El correo electrónico no tiene un formato válido.");
            }

            if (string.IsNullOrWhiteSpace(dto.Direccion))
                throw new ExcepcionValidacion("La dirección es obligatoria.");
            if (dto.Direccion.Length > 100)
                throw new ExcepcionValidacion("La dirección no puede superar los 100 caracteres.");

            if (dto.Vigencia != 'A' && dto.Vigencia != 'I')
                throw new ExcepcionValidacion("La vigencia debe ser 'A' (activa) o 'I' (inactiva).");
        }
    }
}