using System.Collections.Generic;
using CentroAcopioApp.Datos.Repositorios.Interfaces;
using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;
using CentroAcopioApp.Negocio.Transaccion;
using CentroAcopioApp.Negocio.Validaciones;
using CentroAcopioApp.Utilidades;

namespace CentroAcopioApp.Negocio.Servicios
{
    public class UsuarioServicio
    {
         private readonly IUsuarioRepositorio _repositorio;

    public UsuarioServicio(IUsuarioRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public IEnumerable<UsuarioDto> ObtenerTodo()
    {
        return _repositorio.ObtenerTodo();
    }

    public UsuarioDto ObtenerPorId(int id)
    {
        if (id <= 0)
            throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

        var dto = _repositorio.ObtenerPorId(id);
        if (dto == null)
            throw new ExcepcionServicio("No se encontró el usuario especificado.");

        return dto;
    }

    public IEnumerable<UsuarioDto> BuscarPorNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ExcepcionValidacion("Debe proporcionar un nombre para la búsqueda.");

        return _repositorio.ObtenerPorNombre(nombre);
    }

    public UsuarioDto ObtenerPorUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ExcepcionValidacion("Debe proporcionar un nombre de usuario.");

        var dto = _repositorio.ObtenerPorUsername(username);
        if (dto == null)
            throw new ExcepcionServicio("No se encontró el usuario especificado.");

        return dto;
    }

    public int Crear(UsuarioDto dto)
    {
        UsuarioValidador.Validar(dto);
        
        dto.PasswordHash = PasswordHasher.GenerarHash(dto.PasswordHash);

        using (var tx = new TransaccionManager())
        {
            return tx.EjecutarResultado(() =>
            {
                var id = _repositorio.Insertar(dto);
                return id;
            });
        }
    }

    public bool Actualizar(UsuarioDto dto)
    {
        UsuarioValidador.Validar(dto);
        
        // Si la contraseña se actualiza, regenerar el hash
        if (!string.IsNullOrWhiteSpace(dto.PasswordHash))
            dto.PasswordHash = PasswordHasher.GenerarHash(dto.PasswordHash);

        using (var tx = new TransaccionManager())
        {
            return tx.EjecutarResultado(() =>
            {
                var actualizado = _repositorio.Actualizar(dto);
                if (!actualizado)
                    throw new ExcepcionServicio("No se pudo actualizar el usuario.");
                return actualizado;
            });
        }
    }

    public bool Eliminar(int id)
    {
        if (id <= 0)
            throw new ExcepcionValidacion("El ID debe ser mayor a cero.");

        using (var tx = new TransaccionManager())
        {
            return tx.EjecutarResultado(() =>
            {
                var eliminado = _repositorio.Eliminar(id);
                if (!eliminado)
                    throw new ExcepcionServicio("No se encontró el usuario para eliminar.");
                return eliminado;
            });
        }
    }
    }
}