using System;
using System.Data;
using System.Data.SqlClient;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Transaccion
{
    public class TransaccionManager : IDisposable
    {
        private bool _confirmada;

        public TransaccionManager()
        {
            Conexion = DbConexion.CrearConexion();
            Conexion.Open();
            Transaccion = Conexion.BeginTransaction();
            _confirmada = false;
        }

        public SqlConnection Conexion { get; }

        public SqlTransaction Transaccion { get; }


        public void Dispose()
        {
            if (!_confirmada)
                try
                {
                    Transaccion?.Rollback();
                }
                catch
                {
                }

            Transaccion?.Dispose();

            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
        }

        public void Ejecutar(Action accion)
        {
            try
            {
                accion(); // Ejecuta la lógica de negocio
                Transaccion.Commit();
                _confirmada = true;
            }
            catch (Exception ex)
            {
                try
                {
                    Transaccion.Rollback();
                }
                catch
                {
                }

                throw new ExcepcionTransaccion("Error durante la transacción.", ex);
            }
        }

        public T EjecutarResultado<T>(Func<T> funcion)
        {
            try
            {
                var resultado = funcion();
                Transaccion.Commit();
                _confirmada = true;
                return resultado;
            }
            catch (Exception ex)
            {
                try
                {
                    Transaccion.Rollback();
                }
                catch
                {
                }

                throw new ExcepcionTransaccion("Error durante la transacción.", ex);
            }
        }
    }
}