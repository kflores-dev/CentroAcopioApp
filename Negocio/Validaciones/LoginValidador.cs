using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Validaciones
{
    public class LoginValidador
    {
        public static void Validar(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ExcepcionValidacion("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(password))
                throw new ExcepcionValidacion("La contraseña es obligatoria.");

            if (username.Length > 50)
                throw new ExcepcionValidacion("El nombre de usuario no puede superar 50 caracteres.");
        }
    }
}