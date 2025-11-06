using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Datos.Repositorios.Implementaciones
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public IEnumerable<UsuarioDto> ObtenerTodo()
        {
            try
            {
                var lista = new List<UsuarioDto>();
                var consulta = "SELECT id, nombre, username, password_hash, rol, email, vigencia FROM usuario";

                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    conn.Open();
                    using (var lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                            lista.Add(MapearDto(lector));
                    }
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al obtener los usuarios.", ex);
            }
        }

        public UsuarioDto ObtenerPorId(int id)
        {
            UsuarioDto dto = null;
            var consulta = @"SELECT id, nombre, username, password_hash, rol, email, vigencia 
                             FROM usuario WHERE id = @Id";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    using (var lector = cmd.ExecuteReader())
                    {
                        if (lector.Read())
                            dto = MapearDto(lector);
                    }
                }

                return dto;
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al obtener el usuario.", ex);
            }
        }

        public IEnumerable<UsuarioDto> ObtenerPorNombre(string nombre)
        {
            var lista = new List<UsuarioDto>();
            var consulta = @"SELECT id, nombre, username, password_hash, rol, email, vigencia 
                           FROM usuario WHERE nombre LIKE @Nombre";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");
                    conn.Open();
                    using (var lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                            lista.Add(MapearDto(lector));
                    }
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al buscar usuarios por nombre.", ex);
            }
        }

        public UsuarioDto ObtenerPorUsername(string username)
        {
            UsuarioDto dto = null;
            var consulta = @"SELECT id, nombre, username, password_hash, rol, email, vigencia 
                             FROM usuario WHERE username = @Username";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    using (var lector = cmd.ExecuteReader())
                    {
                        if (lector.Read())
                            dto = MapearDto(lector);
                    }
                }

                return dto;
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al obtener el usuario por username.", ex);
            }
        }

        public int Insertar(UsuarioDto dto)
        {
            var consulta = @"INSERT INTO usuario (nombre, username, password_hash, rol, email, vigencia)
                             VALUES (@Nombre, @Username, @PasswordHash, @Rol, @Email, @Vigencia);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Nombre", dto.Nombre);
                    cmd.Parameters.AddWithValue("@Username", dto.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", dto.PasswordHash);
                    cmd.Parameters.AddWithValue("@Rol", dto.Rol);
                    cmd.Parameters.AddWithValue("@Email", (object)dto.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al insertar el usuario.", ex);
            }
        }

        public bool Actualizar(UsuarioDto dto)
        {
            var consulta = @"UPDATE usuario
                             SET nombre = @Nombre,
                                 username = @Username,
                                 password_hash = @PasswordHash,
                                 rol = @Rol,
                                 email = @Email,
                                 vigencia = @Vigencia
                             WHERE id = @Id";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Id", dto.Id);
                    cmd.Parameters.AddWithValue("@Nombre", dto.Nombre);
                    cmd.Parameters.AddWithValue("@Username", dto.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", dto.PasswordHash);
                    cmd.Parameters.AddWithValue("@Rol", dto.Rol);
                    cmd.Parameters.AddWithValue("@Email", (object)dto.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al actualizar el usuario.", ex);
            }
        }

        public bool Eliminar(int id)
        {
            var consulta = @"UPDATE usuario SET vigencia = 'I' WHERE id = @Id";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    var filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al eliminar el usuario.", ex);
            }
        }

        private UsuarioDto MapearDto(SqlDataReader reader)
        {
            return new UsuarioDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                Username = reader.GetString(reader.GetOrdinal("username")),
                PasswordHash = reader.GetString(reader.GetOrdinal("password_hash")),
                Rol = reader.GetString(reader.GetOrdinal("rol")),
                Email = reader.IsDBNull(reader.GetOrdinal("email"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("email")),
                Vigencia = reader.GetString(reader.GetOrdinal("vigencia"))[0]
            };
        }
    }
}