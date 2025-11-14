using System.Collections.Generic;
using System.Linq;
using CentroAcopioApp.Excepciones;
using CentroAcopioApp.Negocio.Seguridad;

namespace CentroAcopioApp.Utilidades
{
    public class FiltroRolHelper
    {
        public static IEnumerable<T> FiltrarPorRol<T>(IEnumerable<T> lista)
        {
            var sesion = SesionActual.Instancia;

            if (sesion.EsAdmin())
                return lista;

            // Operador: solo vigencia = "A"
            return lista.Where(x => ObtenerVigencia(x) == "A");
        }

        public static T FiltrarEntidadPorRol<T>(T entidad)
        {
            var sesion = SesionActual.Instancia;

            if (entidad == null)
                return default;

            var vigencia = ObtenerVigencia(entidad);

            if (sesion.EsOperador() && vigencia != "A")
                return default;

            return entidad;
        }

        private static string ObtenerVigencia<T>(T entidad)
        {
            var prop = typeof(T).GetProperty("Vigencia");
            if (prop == null)
                throw new ExcepcionServicio($"El tipo {typeof(T).Name} no contiene la propiedad 'Vigencia'.");

            return prop.GetValue(entidad)?.ToString();
        }
    }
}