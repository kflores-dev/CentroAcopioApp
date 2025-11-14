using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Datos.Repositorios.Implementaciones
{
    public class DetalleSolicitudRepositorio : IDetalleSolicitudRepositorio
    {
        public IEnumerable<DetalleSolicitudDto> ObtenerTodo()
        {
            var lista = new List<DetalleSolicitudDto>();
            var consulta = @"
            SELECT ds.id, ds.solicitud_id, s.nombre AS solicitud_nombre,
                   ds.recurso_id, r.nombre AS recurso_nombre,
                   ds.cantidad_solicitada, ds.cantidad_entregada, ds.vigencia
            FROM detalle_solicitud ds
            INNER JOIN solicitud s ON ds.solicitud_id = s.id
            INNER JOIN recurso r ON ds.recurso_id = r.id";

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
                throw new ExcepcionAccesoDatos("Error al obtener los detalles de solicitud.", ex);
            }
        }

        public DetalleSolicitudDto ObtenerPorId(int id)
        {
            DetalleSolicitudDto dto = null;
            var consulta = @"
            SELECT ds.id, ds.solicitud_id, s.nombre AS solicitud_nombre,
                   ds.recurso_id, r.nombre AS recurso_nombre,
                   ds.cantidad_solicitada, ds.cantidad_entregada, ds.vigencia
            FROM detalle_solicitud ds
            INNER JOIN solicitud s ON ds.solicitud_id = s.id
            INNER JOIN recurso r ON ds.recurso_id = r.id
            WHERE ds.id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al obtener el detalle de solicitud.", ex);
            }
        }

        public IEnumerable<DetalleSolicitudDto> ObtenerPorSolicitud(int solicitudId)
        {
            var lista = new List<DetalleSolicitudDto>();
            var consulta = @"
            SELECT ds.id, ds.solicitud_id, s.nombre AS solicitud_nombre,
                   ds.recurso_id, r.nombre AS recurso_nombre,
                   ds.cantidad_solicitada, ds.cantidad_entregada, ds.vigencia
            FROM detalle_solicitud ds
            INNER JOIN solicitud s ON ds.solicitud_id = s.id
            INNER JOIN recurso r ON ds.recurso_id = r.id
            WHERE ds.solicitud_id = @SolicitudId";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@SolicitudId", solicitudId);
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
                throw new ExcepcionAccesoDatos("Error al obtener los detalles por solicitud.", ex);
            }
        }

        public IEnumerable<DetalleSolicitudDto> ObtenerPorRecurso(int recursoId)
        {
            var lista = new List<DetalleSolicitudDto>();
            var consulta = @"
            SELECT ds.id, ds.solicitud_id, s.nombre AS solicitud_nombre,
                   ds.recurso_id, r.nombre AS recurso_nombre,
                   ds.cantidad_solicitada, ds.cantidad_entregada, ds.vigencia
            FROM detalle_solicitud ds
            INNER JOIN solicitud s ON ds.solicitud_id = s.id
            INNER JOIN recurso r ON ds.recurso_id = r.id
            WHERE ds.recurso_id = @RecursoId";

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
                throw new ExcepcionAccesoDatos("Error al obtener los detalles por recurso.", ex);
            }
        }

        public int Insertar(DetalleSolicitudDto dto)
        {
            var consulta = @"
            INSERT INTO detalle_solicitud (solicitud_id, recurso_id, cantidad_solicitada, cantidad_entregada, vigencia)
            VALUES (@SolicitudId, @RecursoId, @CantidadSolicitada, @CantidadEntregada, @Vigencia);
            SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@SolicitudId", dto.SolicitudId);
                    cmd.Parameters.AddWithValue("@RecursoId", dto.RecursoId);
                    cmd.Parameters.AddWithValue("@CantidadSolicitada", dto.CantidadSolicitada);
                    cmd.Parameters.AddWithValue("@CantidadEntregada", dto.CantidadEntregada);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al insertar el detalle de solicitud.", ex);
            }
        }

        public bool Actualizar(DetalleSolicitudDto dto)
        {
            var consulta = @"
            UPDATE detalle_solicitud
            SET solicitud_id = @SolicitudId,
                recurso_id = @RecursoId,
                cantidad_solicitada = @CantidadSolicitada,
                cantidad_entregada = @CantidadEntregada,
                vigencia = @Vigencia
            WHERE id = @Id";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Id", dto.Id);
                    cmd.Parameters.AddWithValue("@SolicitudId", dto.SolicitudId);
                    cmd.Parameters.AddWithValue("@RecursoId", dto.RecursoId);
                    cmd.Parameters.AddWithValue("@CantidadSolicitada", dto.CantidadSolicitada);
                    cmd.Parameters.AddWithValue("@CantidadEntregada", dto.CantidadEntregada);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al actualizar el detalle de solicitud.", ex);
            }
        }

        public bool Eliminar(int id)
        {
            var consulta = @"UPDATE detalle_solicitud SET vigencia = 'I' WHERE id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al eliminar el detalle de solicitud.", ex);
            }
        }

        private DetalleSolicitudDto MapearDto(SqlDataReader reader)
        {
            return new DetalleSolicitudDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                SolicitudId = reader.GetInt32(reader.GetOrdinal("solicitud_id")),
                SolicitudNombre = reader.GetString(reader.GetOrdinal("solicitud_nombre")),
                RecursoId = reader.GetInt32(reader.GetOrdinal("recurso_id")),
                RecursoNombre = reader.GetString(reader.GetOrdinal("recurso_nombre")),
                CantidadSolicitada = reader.GetDecimal(reader.GetOrdinal("cantidad_solicitada")),
                CantidadEntregada = reader.GetDecimal(reader.GetOrdinal("cantidad_entregada")),
                Vigencia = reader.GetString(reader.GetOrdinal("vigencia"))[0]
            };
        }
    }
}