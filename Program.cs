using System;
using System.Windows.Forms;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.Datos.Repositorios.Implementaciones;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;
using CentroAcopioApp.Negocio.Servicios;
using CentroAcopioApp.Presentacion.Formularios;

namespace CentroAcopioApp
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                using (var conn = DbConexion.CrearConexion())
                {
                    conn.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de conexión: " + ex.Message);
            }

            var historialRepo = new HistorialRepositorio();
            var historialSrv = new HistorialServicio(historialRepo);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin(new UsuarioRepositorio()));
        }
    }
}