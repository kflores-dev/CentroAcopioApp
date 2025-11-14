using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Validaciones
{
    public class TipoRecursoValidador
    {
        public static void Validar(TipoRecursoDto dto)
        {
            if (dto == null)
                throw new ExcepcionValidacion("El objeto TipoRecursoDto no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ExcepcionValidacion("El nombre del tipo de recurso es obligatorio.");

            if (dto.Nombre.Length > 100)
                throw new ExcepcionValidacion("El nombre no puede superar los 100 caracteres.");

            if (dto.Vigencia != 'A' && dto.Vigencia != 'I')
                throw new ExcepcionValidacion("El campo Vigencia solo puede ser 'A' (Activo) o 'I' (Inactivo).");
        }
    }
}