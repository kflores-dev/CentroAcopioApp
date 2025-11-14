using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Seguridad
{
    public static class Autorizacion
    {
        public static void RequerirAdmin()
        {
            if (!SesionActual.Instancia.EsAdmin())
                throw new ExcepcionServicio("No tiene permisos para realizar esta operación.");
        }

        public static void RequerirRol(params RolUsuario[] roles)
        {
            var actual = SesionActual.Instancia.Rol;

            foreach (var r in roles)
            {
                if (r == actual)
                    return; // autorizado
            }

            throw new ExcepcionServicio("No tiene permisos para realizar esta operación.");
        }
    }
}