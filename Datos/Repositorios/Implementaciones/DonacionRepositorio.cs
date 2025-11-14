using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Datos.Repositorios.Implementaciones
{
    public class DonacionRepositorio : IRepositorioBase<DonacionDto>
    {
        public IEnumerable<DonacionDto> ObtenerTodo()
        {
            var lista = new List<DonacionDto>();
            var consulta = @"
                SELECT d.id, d.proveedor_id, p.nombre AS proveedor_nombre,
                       d.fecha, d.observaciones, d.vigencia
                FROM donacion d
                INNER JOIN proveedor p ON d.proveedor_id = p.id";

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
                throw new ExcepcionAccesoDatos("Error al obtener las donaciones.", ex);
            }
        }

        public DonacionDto ObtenerPorId(int id)
        {
            DonacionDto dto = null;
            var consulta = @"
                SELECT d.id, d.proveedor_id, p.nombre AS proveedor_nombre,
                       d.fecha, d.observaciones, d.vigencia
                FROM donacion d
                INNER JOIN proveedor p ON d.proveedor_id = p.id
                WHERE d.id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al obtener la donación.", ex);
            }
        }

        public IEnumerable<DonacionDto> ObtenerPorProveedor(int proveedorId)
        {
            var lista = new List<DonacionDto>();
            var consulta = @"
                SELECT d.id, d.proveedor_id, p.nombre AS proveedor_nombre,
                       d.fecha, d.observaciones, d.vigencia
                FROM donacion d
                INNER JOIN proveedor p ON d.proveedor_id = p.id
                WHERE d.proveedor_id = @ProveedorId";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@ProveedorId", proveedorId);
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
                throw new ExcepcionAccesoDatos("Error al obtener donaciones por proveedor.", ex);
            }
        }

        public IEnumerable<DonacionDto> ObtenerPorFecha(DateTime fecha)
        {
            var lista = new List<DonacionDto>();
            var consulta = @"
                SELECT d.id, d.proveedor_id, p.nombre AS proveedor_nombre,
                       d.fecha, d.observaciones, d.vigencia
                FROM donacion d
                INNER JOIN proveedor p ON d.proveedor_id = p.id
                WHERE CAST(d.fecha AS DATE) = @Fecha";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
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
                throw new ExcepcionAccesoDatos("Error al obtener donaciones por fecha.", ex);
            }
        }

        public int Insertar(DonacionDto dto)
        {
            var consulta = @"
                INSERT INTO donacion (proveedor_id, fecha, observaciones, vigencia)
                VALUES (@ProveedorId, @Fecha, @Observaciones, @Vigencia);
                SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@ProveedorId", dto.ProveedorId);
                    cmd.Parameters.AddWithValue("@Fecha", dto.Fecha);
                    cmd.Parameters.AddWithValue("@Observaciones", (object)dto.Observaciones ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al insertar la donación.", ex);
            }
        }

        public bool Actualizar(DonacionDto dto)
        {
            var consulta = @"
                UPDATE donacion
                SET proveedor_id = @ProveedorId,
                    fecha = @Fecha,
                    observaciones = @Observaciones,
                    vigencia = @Vigencia
                WHERE id = @Id";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Id", dto.Id);
                    cmd.Parameters.AddWithValue("@ProveedorId", dto.ProveedorId);
                    cmd.Parameters.AddWithValue("@Fecha", dto.Fecha);
                    cmd.Parameters.AddWithValue("@Observaciones", (object)dto.Observaciones ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al actualizar la donación.", ex);
            }
        }

        public bool Eliminar(int id)
        {
            var consulta = @"UPDATE donacion SET vigencia = 'I' WHERE id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al eliminar la donación.", ex);
            }
        }

        private DonacionDto MapearDto(SqlDataReader reader)
        {
            return new DonacionDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                ProveedorId = reader.GetInt32(reader.GetOrdinal("proveedor_id")),
                ProveedorNombre = reader.GetString(reader.GetOrdinal("proveedor_nombre")),
                Fecha = reader.GetDateTime(reader.GetOrdinal("fecha")),
                Observaciones = reader.IsDBNull(reader.GetOrdinal("observaciones")) ? null : reader.GetString(reader.GetOrdinal("observaciones")),
                Vigencia = reader.GetString(reader.GetOrdinal("vigencia"))[0]
            };
        }
    }
   
}