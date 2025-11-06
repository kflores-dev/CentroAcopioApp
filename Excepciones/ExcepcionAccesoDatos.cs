using System;

namespace CentroAcopioApp.Excepciones
{
    public class ExcepcionAccesoDatos : Exception
    {
        public ExcepcionAccesoDatos()
        {
        }

        public ExcepcionAccesoDatos(string message)
            : base(message)
        {
        }

        public ExcepcionAccesoDatos(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}