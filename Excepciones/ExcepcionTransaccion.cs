using System;

namespace CentroAcopioApp.Excepciones
{
    public class ExcepcionTransaccion : Exception
    {
        public ExcepcionTransaccion()
        {
        }

        public ExcepcionTransaccion(string mensaje)
            : base(mensaje)
        {
        }

        public ExcepcionTransaccion(string mensaje, Exception excepcionInterna)
            : base(mensaje, excepcionInterna)
        {
        }
    }
}