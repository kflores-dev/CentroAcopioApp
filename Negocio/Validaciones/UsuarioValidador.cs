using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Validaciones
{
    public class UsuarioValidador
    {
        public static void Validar(UsuarioDto dto)
        {
            if (dto == null)
                throw new ExcepcionValidacion("El objeto UsuarioDto no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ExcepcionValidacion("El nombre del usuario es obligatorio.");

            if (dto.Nombre.Length > 100)
                throw new ExcepcionValidacion("El nombre no puede superar los 100 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Username))
                throw new ExcepcionValidacion("El nombre de usuario es obligatorio.");

            if (dto.Username.Length > 50)
                throw new ExcepcionValidacion("El nombre de usuario no puede superar los 50 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.PasswordHash))
                throw new ExcepcionValidacion("La contraseña es obligatoria.");

            if (dto.PasswordHash.Length > 255)
                throw new ExcepcionValidacion("La contraseña no puede superar los 255 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Rol))
                throw new ExcepcionValidacion("El rol del usuario es obligatorio.");

            if (dto.Rol.Length > 50)
                throw new ExcepcionValidacion("El rol no puede superar los 50 caracteres.");

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                if (dto.Email.Length > 100)
                    throw new ExcepcionValidacion("El correo electrónico no puede superar los 100 caracteres.");

                if (!dto.Email.Contains("@") || !dto.Email.Contains("."))
                    throw new ExcepcionValidacion("El correo electrónico no tiene un formato válido.");
            }

            if (dto.Vigencia != 'A' && dto.Vigencia != 'I')
                throw new ExcepcionValidacion("La vigencia debe ser 'A' (activa) o 'I' (inactiva).");
        }
    }
}