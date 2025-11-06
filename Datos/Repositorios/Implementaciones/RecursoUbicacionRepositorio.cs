using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Datos.Repositorios.Implementaciones
{
    public class RecursoUbicacionRepositorio
    {
        public IEnumerable<RecursoUbicacionDto> ObtenerTodo()
        {
            var lista = new List<RecursoUbicacionDto>();
            var consulta = @"
                SELECT ru.id, ru.recurso_id, r.nombre AS recurso_nombre,
                       ru.ubicacion_id, u.nombre AS ubicacion_nombre,
                       ru.cantidad, ru.vigencia
                FROM recurso_ubicacion ru
                INNER JOIN recurso r ON ru.recurso_id = r.id
                INNER JOIN ubicacion u ON ru.ubicacion_id = u.id";

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
                throw new ExcepcionAccesoDatos("Error al obtener las relaciones recurso-ubicación.", ex);
            }
        }

        public RecursoUbicacionDto ObtenerPorId(int id)
        {
            RecursoUbicacionDto dto = null;
            var consulta = @"
                SELECT ru.id, ru.recurso_id, r.nombre AS recurso_nombre,
                       ru.ubicacion_id, u.nombre AS ubicacion_nombre,
                       ru.cantidad, ru.vigencia
                FROM recurso_ubicacion ru
                INNER JOIN recurso r ON ru.recurso_id = r.id
                INNER JOIN ubicacion u ON ru.ubicacion_id = u.id
                WHERE ru.id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al obtener la relación recurso-ubicación.", ex);
            }
        }

        public IEnumerable<RecursoUbicacionDto> ObtenerPorRecurso(int recursoId)
        {
            var lista = new List<RecursoUbicacionDto>();
            var consulta = @"
                SELECT ru.id, ru.recurso_id, r.nombre AS recurso_nombre,
                       ru.ubicacion_id, u.nombre AS ubicacion_nombre,
                       ru.cantidad, ru.vigencia
                FROM recurso_ubicacion ru
                INNER JOIN recurso r ON ru.recurso_id = r.id
                INNER JOIN ubicacion u ON ru.ubicacion_id = u.id
                WHERE ru.recurso_id = @RecursoId";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@RecursoId", recursoId);
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
                throw new ExcepcionAccesoDatos("Error al obtener ubicaciones por recurso.", ex);
            }
        }

        public IEnumerable<RecursoUbicacionDto> ObtenerPorUbicacion(int ubicacionId)
        {
            var lista = new List<RecursoUbicacionDto>();
            var consulta = @"
                SELECT ru.id, ru.recurso_id, r.nombre AS recurso_nombre,
                       ru.ubicacion_id, u.nombre AS ubicacion_nombre,
                       ru.cantidad, ru.vigencia
                FROM recurso_ubicacion ru
                INNER JOIN recurso r ON ru.recurso_id = r.id
                INNER JOIN ubicacion u ON ru.ubicacion_id = u.id
                WHERE ru.ubicacion_id = @UbicacionId";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@UbicacionId", ubicacionId);
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
                throw new ExcepcionAccesoDatos("Error al obtener recursos por ubicación.", ex);
            }
        }

        public int Insertar(RecursoUbicacionDto dto)
        {
            var consulta = @"
                INSERT INTO recurso_ubicacion (recurso_id, ubicacion_id, cantidad, vigencia)
                VALUES (@RecursoId, @UbicacionId, @Cantidad, @Vigencia);
                SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@RecursoId", dto.RecursoId);
                    cmd.Parameters.AddWithValue("@UbicacionId", dto.UbicacionId);
                    cmd.Parameters.AddWithValue("@Cantidad", dto.Cantidad);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al insertar la relación recurso-ubicación.", ex);
            }
        }

        public bool Actualizar(RecursoUbicacionDto dto)
        {
            var consulta = @"
                UPDATE recurso_ubicacion
                SET recurso_id = @RecursoId,
                    ubicacion_id = @UbicacionId,
                    cantidad = @Cantidad,
                    vigencia = @Vigencia
                WHERE id = @Id";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Id", dto.Id);
                    cmd.Parameters.AddWithValue("@RecursoId", dto.RecursoId);
                    cmd.Parameters.AddWithValue("@UbicacionId", dto.UbicacionId);
                    cmd.Parameters.AddWithValue("@Cantidad", dto.Cantidad);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al actualizar la relación recurso-ubicación.", ex);
            }
        }

        public bool Eliminar(int id)
        {
            var consulta = @"UPDATE recurso_ubicacion SET vigencia = 'I' WHERE id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al eliminar la relación recurso-ubicación.", ex);
            }
        }

        private RecursoUbicacionDto MapearDto(SqlDataReader reader)
        {
            return new RecursoUbicacionDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                RecursoId = reader.GetInt32(reader.GetOrdinal("recurso_id")),
                RecursoNombre = reader.GetString(reader.GetOrdinal("recurso_nombre")),
                UbicacionId = reader.GetInt32(reader.GetOrdinal("ubicacion_id")),
                UbicacionNombre = reader.GetString(reader.GetOrdinal("ubicacion_nombre")),
                Cantidad = reader.GetDecimal(reader.GetOrdinal("cantidad")),
                Vigencia = reader.GetString(reader.GetOrdinal("vigencia"))[0]
            };
        }
    }
}