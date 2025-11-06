using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Datos.Repositorios.Implementaciones
{
    public class TipoRecursoRepositorio : ITipoRecursoRepositorio
    {
        public IEnumerable<TipoRecursoDto> ObtenerTodo()
        {
            try
            {
                var lista = new List<TipoRecursoDto>();
                var consulta = "SELECT id, nombre, vigencia FROM tipo_recurso";

                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    conn.Open();
                    using (var lector = cmd.ExecuteReader())
                    {
                        while (lector.Read()) lista.Add(MapearDto(lector));
                    }
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al obtener los tipos de recurso.", ex);
            }
        }

        public TipoRecursoDto ObtenerPorId(int id)
        {
            TipoRecursoDto dto = null;
            var consulta = "SELECT id, nombre, vigencia FROM tipo_recurso WHERE id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al obtener el tipo de recurso.", ex);
            }
        }

        public IEnumerable<TipoRecursoDto> ObtenerPorNombre(string nombre)
        {
            var lista = new List<TipoRecursoDto>();
            var consulta = "SELECT id, nombre, vigencia FROM tipo_recurso WHERE nombre LIKE @Nombre";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");
                    conn.Open();
                    using (var lector = cmd.ExecuteReader())
                    {
                        while (lector.Read()) lista.Add(MapearDto(lector));
                    }
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al obtener los tipos de recurso.", ex);
            }
        }

        public int Insertar(TipoRecursoDto dto)
        {
            var consulta = @"INSERT INTO tipo_recurso (nombre, vigencia)
                        VALUES (@Nombre);
                        SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Nombre", dto.Nombre);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al insertar el tipo de recurso.", ex);
            }
        }

        public bool Actualizar(TipoRecursoDto dto)
        {
            var consulta = @"UPDATE tipo_recurso
                        SET nombre = @Nombre,
                            vigencia = @Vigencia
                        WHERE id = @Id";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Id", dto.Id);
                    cmd.Parameters.AddWithValue("@Nombre", dto.Nombre);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al actualizar el tipo de recurso.", ex);
            }
        }

        public bool Eliminar(int id)
        {
            var consulta = @"UPDATE tipo_recurso SET vigencia = 'I' WHERE id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al eliminar el tipo de recurso.", ex);
            }
        }


        private TipoRecursoDto MapearDto(SqlDataReader reader)
        {
            return new TipoRecursoDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                Vigencia = reader.GetString(reader.GetOrdinal("vigencia"))[0]
            };
        }
    }
}