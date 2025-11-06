using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CentroAcopioApp.Datos.Conexion
{
    public class DbConexion
    {
        private static readonly string _stringConexion =
            ConfigurationManager.ConnectionStrings["CentroAcopioDB"].ConnectionString;

        public static SqlConnection CrearConexion()
        {
            var connection = new SqlConnection(_stringConexion);
            return connection;
        }

        public static SqlCommand CrearComando(SqlConnection conn, string query, CommandType type = CommandType.Text)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = query;
            cmd.CommandType = type;
            return cmd;
        }
    }
}