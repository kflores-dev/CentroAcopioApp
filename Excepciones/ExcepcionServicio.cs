using System;

namespace CentroAcopioApp.Excepciones
{
    public class ExcepcionServicio : Exception
    {
        public ExcepcionServicio()
        {
        }

        public ExcepcionServicio(string message)
            : base(message)
        {
        }

        public ExcepcionServicio(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}