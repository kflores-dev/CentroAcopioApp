using System;
using System.Security.Cryptography;
using System.Text;

namespace CentroAcopioApp.Negocio.Seguridad
{
    public class PasswordHasher
    {
        public static string GenerarHash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(password));

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerificarHash(string passwordIngresada, string hashAlmacenado)
        {
            if (string.IsNullOrWhiteSpace(passwordIngresada))
                return false;

            if (string.IsNullOrWhiteSpace(hashAlmacenado))
                return false;

            var hashIngresado = GenerarHash(passwordIngresada);

            return hashIngresado == hashAlmacenado;
        }
    }
}