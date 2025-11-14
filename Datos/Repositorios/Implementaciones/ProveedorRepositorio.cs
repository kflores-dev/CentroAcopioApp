using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CentroAcopioApp.Datos.Conexion;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Datos.Repositorios.Implementaciones
{
    public class ProveedorRepositorio : IProveedorRepositorio
    {
        public IEnumerable<ProveedorDto> ObtenerTodo()
        {
            var lista = new List<ProveedorDto>();
            var consulta = @"
                SELECT id, nombre, contacto, telefono, correo, direccion, vigencia
                FROM proveedor";

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
                throw new ExcepcionAccesoDatos("Error al obtener todos los proveedores.", ex);
            }
        }

        public ProveedorDto ObtenerPorId(int id)
        {
            ProveedorDto dto = null;
            var consulta = @"
                SELECT id, nombre, contacto, telefono, correo, direccion, vigencia
                FROM proveedor
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
                throw new ExcepcionAccesoDatos("Error al obtener el proveedor por ID.", ex);
            }
        }

        public IEnumerable<ProveedorDto> ObtenerPorNombre(string nombre)
        {
            var lista = new List<ProveedorDto>();
            var consulta = @"
                SELECT id, nombre, contacto, telefono, correo, direccion, vigencia
                FROM proveedor
                WHERE nombre LIKE '%' + @Nombre + '%'";

            try
            {
                using (var conn = DbConexion.CrearConexion())
                using (var cmd = DbConexion.CrearComando(conn, consulta))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
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
                throw new ExcepcionAccesoDatos("Error al obtener proveedores por nombre.", ex);
            }
        }

        public int Insertar(ProveedorDto dto)
        {
            var consulta = @"
                INSERT INTO proveedor (nombre, contacto, telefono, correo, direccion, vigencia)
                VALUES (@Nombre, @Contacto, @Telefono, @Correo, @Direccion, @Vigencia);
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
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al insertar el proveedor.", ex);
            }
        }

        public bool Actualizar(ProveedorDto dto)
        {
            var consulta = @"
                UPDATE proveedor
                SET nombre = @Nombre,
                    contacto = @Contacto,
                    telefono = @Telefono,
                    correo = @Correo,
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
                    cmd.Parameters.AddWithValue("@Contacto", dto.Contacto);
                    cmd.Parameters.AddWithValue("@Telefono", dto.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", dto.Correo);
                    cmd.Parameters.AddWithValue("@Direccion", dto.Direccion);
                    cmd.Parameters.AddWithValue("@Vigencia", dto.Vigencia);

                    conn.Open();
                    var filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new ExcepcionAccesoDatos("Error al actualizar el proveedor.", ex);
            }
        }

        public bool Eliminar(int id)
        {
            var consulta = @"UPDATE proveedor SET vigencia = 'I' WHERE id = @Id";

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
                throw new ExcepcionAccesoDatos("Error al eliminar el proveedor.", ex);
            }
        }

        private ProveedorDto MapearDto(SqlDataReader lector)
        {
            return new ProveedorDto
            {
                Id = lector.GetInt32(lector.GetOrdinal("id")),
                Nombre = lector.GetString(lector.GetOrdinal("nombre")),
                Contacto = lector.GetString(lector.GetOrdinal("contacto")),
                Telefono = lector.GetString(lector.GetOrdinal("telefono")),
                Correo = lector.GetString(lector.GetOrdinal("correo")),
                Direccion = lector.GetString(lector.GetOrdinal("direccion")),
                Vigencia = lector.GetString(lector.GetOrdinal("vigencia"))[0]
            };
        }
    }
}