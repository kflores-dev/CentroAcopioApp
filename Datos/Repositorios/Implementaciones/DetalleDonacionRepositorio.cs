using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Datos.Repositorios.Implementaciones
{
    public class DetalleDonacionRepositorio : IDetalleDonacionRepositorio
    {
        public IEnumerable<DetalleDonacionDto> ObtenerTodo()
        {
            var lista = new List<DetalleDonacionDto>();
            var consulta = @"
            SELECT dd.id, dd.donacion_id,
                   dd.recurso_id, r.nombre AS recurso_nombre,
                   dd.cantidad_donada,
                   dd.ubicacion_id, u.nombre AS ubicacion_nombre,
                   dd.vigencia
            FROM detalle_donacion dd
            INNER JOIN recurso r ON dd.recurso_id = r.id
            INNER JOIN ubicacion u ON dd.ubicacion_id = u.id";

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
                throw new ExcepcionAccesoDatos("Error al obtener los detalles de donación.", ex);
            }
        }

        public DetalleDonacionDto ObtenerPorId(int id)
        {
            DetalleDonacionDto dto = null;
            var consulta = @"
            SELECT dd.id, dd.donacion_id,
                   dd.recurso_id, r.nombre AS recurso_nombre,
                   dd.cantidad_donada,
                   dd.ubicacion_id, u.nombre AS ubicacion_nombre,
                   dd.vigencia
            FROM detalle_donacion dd
            INNER JOIN recurso r ON dd.recurso_id = r.id
            INNER JOIN ubicacion u ON dd.ubicacion_id = u.id
            WHERE dd.id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al obtener el detalle de donación.", ex);
            }
        }

        public IEnumerable<DetalleDonacionDto> ObtenerPorDonacion(int donacionId)
        {
            var lista = new List<DetalleDonacionDto>();
            var consulta = @"
            SELECT dd.id, dd.donacion_id,
                   dd.recurso_id, r.nombre AS recurso_nombre,
                   dd.cantidad_donada,
                   dd.ubicacion_id, u.nombre AS ubicacion_nombre,
                   dd.vigencia
          
            INNER JOIN recurso r ON dd.recurso_id = r.id
            INNER JOIN ubicacion u ON dd.ubicacion_id = u.id
            WHERE dd.donacion_id = @DonacionId";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@DonacionId", donacionId);
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
                throw new ExcepcionAccesoDatos("Error al obtener detalles por donación.", ex);
            }
        }

        public IEnumerable<DetalleDonacionDto> ObtenerPorRecurso(int recursoId)
        {
            var lista = new List<DetalleDonacionDto>();
            var consulta = @"
            SELECT dd.id, dd.donacion_id,
                   dd.recurso_id, r.nombre AS recurso_nombre,
                   dd.cantidad_donada,
                   dd.ubicacion_id, u.nombre AS ubicacion_nombre,
                   dd.vigencia
            FROM detalle_donacion dd
            INNER JOIN recurso r ON dd.recurso_id = r.id
            INNER JOIN ubicacion u ON dd.ubicacion_id = u.id
            WHERE dd.recurso_id = @RecursoId";

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
                throw new ExcepcionAccesoDatos("Error al obtener detalles por recurso.", ex);
            }
        }

        public IEnumerable<DetalleDonacionDto> ObtenerPorUbicacion(int ubicacionId)
        {
            var lista = new List<DetalleDonacionDto>();
            var consulta = @"
            SELECT dd.id, dd.donacion_id,
                   dd.recurso_id, r.nombre AS recurso_nombre,
                   dd.cantidad_donada,
                   dd.ubicacion_id, u.nombre AS ubicacion_nombre,
                   dd.vigencia
            FROM detalle_donacion dd
            INNER JOIN recurso r ON dd.recurso_id = r.id
            INNER JOIN ubicacion u ON dd.ubicacion_id = u.id
            WHERE dd.ubicacion_id = @UbicacionId";

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
                throw new ExcepcionAccesoDatos("Error al obtener detalles por ubicación.", ex);
            }
        }

        public int Insertar(DetalleDonacionDto dto)
        {
            var consulta = @"
            INSERT INTO detalle_donacion (donacion_id, recurso_id, cantidad_donada, ubicacion_id, vigencia)
            VALUES (@DonacionId, @RecursoId, @CantidadDonada, @UbicacionId, @Vigencia);
            SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@DonacionId", dto.DonacionId);
                    cmd.Parameters.AddWithValue("@RecursoId", dto.RecursoId);
                    cmd.Parameters.AddWithValue("@CantidadDonada", dto.CantidadDonada);
                    cmd.Parameters.AddWithValue("@UbicacionId", dto.UbicacionId);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al insertar el detalle de donación.", ex);
            }
        }

        public bool Actualizar(DetalleDonacionDto dto)
        {
            var consulta = @"
            UPDATE detalle_donacion
            SET donacion_id = @DonacionId,
                recurso_id = @RecursoId,
                cantidad_donada = @CantidadDonada,
                ubicacion_id = @UbicacionId,
                vigencia = @Vigencia
            WHERE id = @Id";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Id", dto.Id);
                    cmd.Parameters.AddWithValue("@DonacionId", dto.DonacionId);
                    cmd.Parameters.AddWithValue("@RecursoId", dto.RecursoId);
                    cmd.Parameters.AddWithValue("@CantidadDonada", dto.CantidadDonada);
                    cmd.Parameters.AddWithValue("@UbicacionId", dto.UbicacionId);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al actualizar el detalle de donación.", ex);
            }
        }

        public bool Eliminar(int id)
        {
            var consulta = @"UPDATE detalle_donacion SET vigencia = 'I' WHERE id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al eliminar el detalle de donación.", ex);
            }
        }

        private DetalleDonacionDto MapearDto(SqlDataReader reader)
        {
            return new DetalleDonacionDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                DonacionId = reader.GetInt32(reader.GetOrdinal("donacion_id")),
                RecursoId = reader.GetInt32(reader.GetOrdinal("recurso_id")),
                RecursoNombre = reader.GetString(reader.GetOrdinal("recurso_nombre")),
                CantidadDonada = reader.GetDecimal(reader.GetOrdinal("cantidad_donada")),
                UbicacionId = reader.GetInt32(reader.GetOrdinal("ubicacion_id")),
                UbicacionNombre = reader.GetString(reader.GetOrdinal("ubicacion_nombre")),
                Vigencia = reader.GetString(reader.GetOrdinal("vigencia"))[0]
            };
        }
    }
}