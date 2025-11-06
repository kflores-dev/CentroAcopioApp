using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Datos.Repositorios.Implementaciones
{
   public class RecursoRepositorio : IRecursoRepositorio
    {
        public IEnumerable<RecursoDto> ObtenerTodo()
        {
            var lista = new List<RecursoDto>();
            var consulta = @"
                SELECT r.id, r.nombre, r.tipo_id, tr.nombre AS tipo_nombre, 
                       r.unidad_medida, r.vigencia
                FROM recurso r
                INNER JOIN tipo_recurso tr ON r.tipo_id = tr.id";

            try
            {
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
                throw new ExcepcionAccesoDatos("Error al obtener los recursos.", ex);
            }
        }

        public RecursoDto ObtenerPorId(int id)
        {
            RecursoDto dto = null;
            var consulta = @"
                SELECT r.id, r.nombre, r.tipo_id, tr.nombre AS tipo_nombre,
                       r.unidad_medida, r.vigencia
                FROM recurso r
                INNER JOIN tipo_recurso tr ON r.tipo_id = tr.id
                WHERE r.id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al obtener el recurso.", ex);
            }
        }

        public IEnumerable<RecursoDto> ObtenerPorNombre(string nombre)
        {
            var lista = new List<RecursoDto>();
            var consulta = @"
                SELECT r.id, r.nombre, r.tipo_id, tr.nombre AS tipo_nombre,
                       r.unidad_medida, r.vigencia
                FROM recurso r
                INNER JOIN tipo_recurso tr ON r.tipo_id = tr.id
                WHERE r.nombre LIKE @Nombre";

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
                throw new ExcepcionAccesoDatos("Error al buscar los recursos por nombre.", ex);
            }
        }

        public IEnumerable<RecursoDto> ObtenerPorTipo(int tipoId)
        {
            var lista = new List<RecursoDto>();
            var consulta = @"
                SELECT r.id, r.nombre, r.tipo_id, tr.nombre AS tipo_nombre,
                       r.unidad_medida, r.vigencia
                FROM recurso r
                INNER JOIN tipo_recurso tr ON r.tipo_id = tr.id
                WHERE r.tipo_id = @TipoId";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@TipoId", tipoId);
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
                throw new ExcepcionAccesoDatos("Error al buscar los recursos por tipo.", ex);
            }
        }

        public int Insertar(RecursoDto dto)
        {
            var consulta = @"
                INSERT INTO recurso (nombre, tipo_id, unidad_medida, vigencia)
                VALUES (@Nombre, @TipoId, @UnidadMedida, @Vigencia);
                SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Nombre", dto.Nombre);
                    cmd.Parameters.AddWithValue("@TipoId", dto.TipoId);
                    cmd.Parameters.AddWithValue("@UnidadMedida", dto.UnidadMedida);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al insertar el recurso.", ex);
            }
        }

        public bool Actualizar(RecursoDto dto)
        {
            var consulta = @"
                UPDATE recurso
                SET nombre = @Nombre,
                    tipo_id = @TipoId,
                    unidad_medida = @UnidadMedida,
                    vigencia = @Vigencia
                WHERE id = @Id";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Id", dto.Id);
                    cmd.Parameters.AddWithValue("@Nombre", dto.Nombre);
                    cmd.Parameters.AddWithValue("@TipoId", dto.TipoId);
                    cmd.Parameters.AddWithValue("@UnidadMedida", dto.UnidadMedida);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al actualizar el recurso.", ex);
            }
        }

        public bool Eliminar(int id)
        {
            var consulta = @"UPDATE recurso SET vigencia = 'I' WHERE id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al eliminar el recurso.", ex);
            }
        }

        private RecursoDto MapearDto(SqlDataReader reader)
        {
            return new RecursoDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                TipoId = reader.GetInt32(reader.GetOrdinal("tipo_id")),
                TipoNombre = reader.GetString(reader.GetOrdinal("tipo_nombre")),
                UnidadMedida = reader.GetString(reader.GetOrdinal("unidad_medida")),
                Vigencia = reader.GetString(reader.GetOrdinal("vigencia"))[0]
            };
        }
    }
}