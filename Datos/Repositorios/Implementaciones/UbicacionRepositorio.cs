using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Datos.Repositorios.Implementaciones
{
    public class UbicacionRepositorio : IUbicacionRepositorio
    {
         public IEnumerable<UbicacionDto> ObtenerTodo()
        {
            try
            {
                var lista = new List<UbicacionDto>();
                var consulta = "SELECT id, nombre, direccion, vigencia FROM ubicacion";

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
                throw new ExcepcionAccesoDatos("Error al obtener las ubicaciones.", ex);
            }
        }

        public UbicacionDto ObtenerPorId(int id)
        {
            UbicacionDto dto = null;
            var consulta = "SELECT id, nombre, direccion, vigencia FROM ubicacion WHERE id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al obtener la ubicación.", ex);
            }
        }

        public IEnumerable<UbicacionDto> ObtenerPorNombre(string nombre)
        {
            var lista = new List<UbicacionDto>();
            var consulta = "SELECT id, nombre, direccion, vigencia FROM ubicacion WHERE nombre LIKE @Nombre";

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
                throw new ExcepcionAccesoDatos("Error al buscar ubicaciones por nombre.", ex);
            }
        }

        public int Insertar(UbicacionDto dto)
        {
            var consulta = @"INSERT INTO ubicacion (nombre, direccion, vigencia)
                             VALUES (@Nombre, @Direccion, @Vigencia);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Nombre", dto.Nombre);
                    cmd.Parameters.AddWithValue("@Direccion", dto.Direccion);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al insertar la ubicación.", ex);
            }
        }

        public bool Actualizar(UbicacionDto dto)
        {
            var consulta = @"UPDATE ubicacion
                             SET nombre = @Nombre,
                                 direccion = @Direccion,
                                 vigencia = @Vigencia
                             WHERE id = @Id";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Id", dto.Id);
                    cmd.Parameters.AddWithValue("@Nombre", dto.Nombre);
                    cmd.Parameters.AddWithValue("@Direccion", dto.Direccion);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al actualizar la ubicación.", ex);
            }
        }

        public bool Eliminar(int id)
        {
            var consulta = @"UPDATE ubicacion SET vigencia = 'I' WHERE id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al eliminar la ubicación.", ex);
            }
        }

        private UbicacionDto MapearDto(SqlDataReader reader)
        {
            return new UbicacionDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                Direccion = reader.GetString(reader.GetOrdinal("direccion")),
                Vigencia = reader.GetString(reader.GetOrdinal("vigencia"))[0]
            };
        }
    }
}