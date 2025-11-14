using System;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Seguridad
{
    public sealed class SesionActual
    {
        private static readonly Lazy<SesionActual> _instancia =
            new Lazy<SesionActual>(() => new SesionActual());

        private SesionActual()
        {
        }

        public static SesionActual Instancia => _instancia.Value;

        public int UsuarioId { get; private set; }
        public string Nombre { get; private set; }
        public RolUsuario Rol { get; private set; }
        public bool Activa { get; private set; }

        public void IniciarSesion(UsuarioDto usuario)
        {
            UsuarioId = usuario.Id;
            Nombre = usuario.Nombre;
            Rol = MapearRol(usuario.Rol);
            Activa = true;
        }

        public void CerrarSesion()
        {
            UsuarioId = 0;
            Nombre = null;
            Rol = 0;
            Activa = false;
        }

        private RolUsuario MapearRol(string rol)
        {
            if (string.IsNullOrWhiteSpace(rol))
                throw new ExcepcionValidacion("Rol no puede ser vacío.");

            switch (rol.ToUpper())
            {
                case "ADMIN":
                    return RolUsuario.Administrador;

                case "OPERADOR":
                    return RolUsuario.Operador;

                default:
                    throw new ExcepcionValidacion("Rol no reconocido.");
            }
        }

        public bool EsAdmin() => Rol == RolUsuario.Administrador;
        public bool EsOperador() => Rol == RolUsuario.Operador;
    }
}