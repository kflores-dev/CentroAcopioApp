using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Datos.Repositorios.Implementaciones
{
    public class SolicitudRepositorio : ISolicitudRepositorio
    {
        public IEnumerable<SolicitudDto> ObtenerTodo()
        {
            var lista = new List<SolicitudDto>();
            var consulta = @"
                SELECT id, nombre, contacto, telefono, correo, direccion,
                       fecha, estado, prioridad, observaciones, vigencia
                FROM solicitud";

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
                throw new ExcepcionAccesoDatos("Error al obtener todas las solicitudes.", ex);
            }
        }

        public SolicitudDto ObtenerPorId(int id)
        {
            SolicitudDto dto = null;
            var consulta = @"
                SELECT id, nombre, contacto, telefono, correo, direccion,
                       fecha, estado, prioridad, observaciones, vigencia
                FROM solicitud
                WHERE id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al obtener la solicitud por ID.", ex);
            }
        }

        public IEnumerable<SolicitudDto> ObtenerPorEstado(string estado)
        {
            var lista = new List<SolicitudDto>();
            var consulta = @"
                SELECT id, nombre, contacto, telefono, correo, direccion,
                       fecha, estado, prioridad, observaciones, vigencia
                FROM solicitud
                WHERE estado = @Estado";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Estado", estado);
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
                throw new ExcepcionAccesoDatos("Error al obtener solicitudes por estado.", ex);
            }
        }

        public IEnumerable<SolicitudDto> ObtenerPorPrioridad(string prioridad)
        {
            var lista = new List<SolicitudDto>();
            var consulta = @"
                SELECT id, nombre, contacto, telefono, correo, direccion,
                       fecha, estado, prioridad, observaciones, vigencia
                FROM solicitud
                WHERE prioridad = @Prioridad";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Prioridad", prioridad);
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
                throw new ExcepcionAccesoDatos("Error al obtener solicitudes por prioridad.", ex);
            }
        }

        public int Insertar(SolicitudDto dto)
        {
            var consulta = @"
                INSERT INTO solicitud (nombre, contacto, telefono, correo, direccion,
                                       fecha, estado, prioridad, observaciones, vigencia)
                VALUES (@Nombre, @Contacto, @Telefono, @Correo, @Direccion,
                        @Fecha, @Estado, @Prioridad, @Observaciones, @Vigencia);
                SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Nombre", dto.Nombre);
                    cmd.Parameters.AddWithValue("@Contacto", dto.Contacto);
                    cmd.Parameters.AddWithValue("@Telefono", dto.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", dto.Correo);
                    cmd.Parameters.AddWithValue("@Direccion", dto.Direccion);
                    cmd.Parameters.AddWithValue("@Fecha", dto.Fecha);
                    cmd.Parameters.AddWithValue("@Estado", dto.Estado);
                    cmd.Parameters.AddWithValue("@Prioridad", dto.Prioridad);
                    cmd.Parameters.AddWithValue("@Observaciones", dto.Observaciones ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al insertar la solicitud.", ex);
            }
        }

        public bool Actualizar(SolicitudDto dto)
        {
            var consulta = @"
                UPDATE solicitud
                SET nombre = @Nombre,
                    contacto = @Contacto,
                    telefono = @Telefono,
                    correo = @Correo,
                    direccion = @Direccion,
                    fecha = @Fecha,
                    estado = @Estado,
                    prioridad = @Prioridad,
                    observaciones = @Observaciones,
                    vigencia = @Vigencia
                WHERE id = @Id";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Id", dto.Id);
                    cmd.Parameters.AddWithValue("@Nombre", dto.Nombre);
                    cmd.Parameters.AddWithValue("@Contacto", dto.Contacto);
                    cmd.Parameters.AddWithValue("@Telefono", dto.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", dto.Correo);
                    cmd.Parameters.AddWithValue("@Direccion", dto.Direccion);
                    cmd.Parameters.AddWithValue("@Fecha", dto.Fecha);
                    cmd.Parameters.AddWithValue("@Estado", dto.Estado);
                    cmd.Parameters.AddWithValue("@Prioridad", dto.Prioridad);
                    cmd.Parameters.AddWithValue("@Observaciones", dto.Observaciones ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al actualizar la solicitud.", ex);
            }
        }

        public bool Eliminar(int id)
        {
            var consulta = @"UPDATE solicitud SET vigencia = 'I' WHERE id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al eliminar la solicitud.", ex);
            }
        }

        private SolicitudDto MapearDto(SqlDataReader lector)
        {
            return new SolicitudDto
            {
                Id = lector.GetInt32(lector.GetOrdinal("id")),
                Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                Contacto = lector.GetString(lector.GetOrdinal("contacto")),
                Telefono = lector.GetString(lector.GetOrdinal("telefono")),
                Correo = lector.GetString(lector.GetOrdinal("correo")),
                Direccion = lector.GetString(lector.GetOrdinal("direccion")),
                Fecha = lector.GetDateTime(lector.GetOrdinal("fecha")),
                Estado = lector.GetString(lector.GetOrdinal("estado")),
                Prioridad = lector.GetString(lector.GetOrdinal("prioridad")),
                Observaciones = lector.IsDBNull(lector.GetOrdinal("observaciones"))
                    ? null
                    : lector.GetString(lector.GetOrdinal("observaciones")),
                Vigencia = lector.GetString(lector.GetOrdinal("vigencia"))[0]
            };
        }
    }
}