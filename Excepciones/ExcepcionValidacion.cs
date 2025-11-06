using System;

namespace CentroAcopioApp.Excepciones
{
    public class ExcepcionValidacion : Exception
    {
        public ExcepcionValidacion()
        {
        }

        public ExcepcionValidacion(string mensaje)
            : base(mensaje)
        {
        }

        public ExcepcionValidacion(string mensaje, Exception excepcionInterna)
            : base(mensaje, excepcionInterna)
        {
        }
    }
}