using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Datos.Repositorios.Implementaciones
{
    public class HistorialRepositorio : IHistorialRepositorio
    {
        public IEnumerable<HistorialDto> ObtenerTodo()
        {
            var lista = new List<HistorialDto>();
            var consulta = @"
                SELECT h.id, h.usuario_id, u.nombre AS usuario_nombre,
                       h.accion, h.entidad, h.entidad_id,
                       h.fecha_hora, h.descripcion, h.vigencia
                FROM historial h
                INNER JOIN usuario u ON h.usuario_id = u.id";

            try
            {
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
                throw new ExcepcionAccesoDatos("Error al obtener los registros del historial.", ex);
            }
        }

        public HistorialDto ObtenerPorId(int id)
        {
            HistorialDto dto = null;
            var consulta = @"
                SELECT h.id, h.usuario_id, u.nombre AS usuario_nombre,
                       h.accion, h.entidad, h.entidad_id,
                       h.fecha_hora, h.descripcion, h.vigencia
                FROM historial h
                INNER JOIN usuario u ON h.usuario_id = u.id
                WHERE h.id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al obtener el registro del historial.", ex);
            }
        }

        public IEnumerable<HistorialDto> ObtenerPorUsuario(int usuarioId)
        {
            var lista = new List<HistorialDto>();
            var consulta = @"
                SELECT h.id, h.usuario_id, u.nombre AS usuario_nombre,
                       h.accion, h.entidad, h.entidad_id,
                       h.fecha_hora, h.descripcion, h.vigencia
                FROM historial h
                INNER JOIN usuario u ON h.usuario_id = u.id
                WHERE h.usuario_id = @UsuarioId";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
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
                throw new ExcepcionAccesoDatos("Error al obtener el historial por usuario.", ex);
            }
        }

        public IEnumerable<HistorialDto> ObtenerPorEntidad(string entidad, int entidadId)
        {
            var lista = new List<HistorialDto>();
            var consulta = @"
                SELECT h.id, h.usuario_id, u.nombre AS usuario_nombre,
                       h.accion, h.entidad, h.entidad_id,
                       h.fecha_hora, h.descripcion, h.vigencia
                FROM historial h
                INNER JOIN usuario u ON h.usuario_id = u.id
                WHERE h.entidad = @Entidad AND h.entidad_id = @EntidadId";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Entidad", entidad);
                    cmd.Parameters.AddWithValue("@EntidadId", entidadId);
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
                throw new ExcepcionAccesoDatos("Error al obtener el historial por entidad.", ex);
            }
        }

        public int Insertar(HistorialDto dto)
        {
            var consulta = @"
                INSERT INTO historial (usuario_id, accion, entidad, entidad_id, fecha_hora, descripcion, vigencia)
                VALUES (@UsuarioId, @Accion, @Entidad, @EntidadId, @FechaHora, @Descripcion, @Vigencia);
                SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", dto.UsuarioId);
                    cmd.Parameters.AddWithValue("@Accion", dto.Accion);
                    cmd.Parameters.AddWithValue("@Entidad", dto.Entidad);
                    cmd.Parameters.AddWithValue("@EntidadId", dto.EntidadId);
                    cmd.Parameters.AddWithValue("@FechaHora", dto.FechaHora);
                    cmd.Parameters.AddWithValue("@Descripcion", (object)dto.Descripcion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al insertar el registro en el historial.", ex);
            }
        }

        private HistorialDto MapearDto(SqlDataReader reader)
        {
            return new HistorialDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                UsuarioId = reader.GetInt32(reader.GetOrdinal("usuario_id")),
                UsuarioNombre = reader.GetString(reader.GetOrdinal("usuario_nombre")),
                Accion = reader.GetString(reader.GetOrdinal("accion")),
                Entidad = reader.GetString(reader.GetOrdinal("entidad")),
                EntidadId = reader.GetInt32(reader.GetOrdinal("entidad_id")),
                FechaHora = reader.GetDateTime(reader.GetOrdinal("fecha_hora")),
                Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("descripcion")),
                Vigencia = reader.GetString(reader.GetOrdinal("vigencia"))[0]
            };
        }
    }
}