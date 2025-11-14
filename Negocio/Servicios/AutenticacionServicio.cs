using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;
using CentroAcopioApp.Negocio.Seguridad;
using CentroAcopioApp.Negocio.Validaciones;

namespace CentroAcopioApp.Negocio.Servicios
{
    public class AutenticacionServicio
    {
        private readonly IUsuarioRepositorio _repositorio;

        public AutenticacionServicio(IUsuarioRepositorio repo)
        {
            _repositorio = repo;
        }

        public UsuarioDto Login(string username, string password)
        {
            LoginValidador.Validar(username, password);

            var usuario = _repositorio.ObtenerPorUsername(username);
            if (usuario == null)
                throw new ExcepcionServicio("Usuario o contraseña inválida.");

            if (usuario.Vigencia != 'A')
                throw new ExcepcionServicio("El usuario está inactivo.");

            if (!PasswordHasher.VerificarHash(password, usuario.PasswordHash))
                throw new ExcepcionServicio("Usuario o contraseña inválida.");

            SesionActual.Instancia.IniciarSesion(usuario);

            HistorialServicio.Registrar(
                usuarioId: usuario.Id,
                accion: "Login",
                entidad: "Usuario",
                entidadId: usuario.Id,
                descripcion: $"El usuario {usuario.Nombre} inició sesión."
            );

            return usuario;
        }

        public void Logout()
        {
            if (SesionActual.Instancia.Activa)
            {
                var usuarioId = SesionActual.Instancia.UsuarioId;

                SesionActual.Instancia.CerrarSesion();

                HistorialServicio.Registrar(
                    usuarioId: usuarioId,
                    accion: "Logout",
                    entidad: "Usuario",
                    entidadId: usuarioId,
                    descripcion: "El usuario cerró sesión."
                );
            }
        }

        public bool TieneRol(params RolUsuario[] roles)
        {
            if (!SesionActual.Instancia.Activa)
                return false;

            foreach (var r in roles)
            {
                if (r == SesionActual.Instancia.Rol)
                    return true;
            }

            return false;
        }
    }
}