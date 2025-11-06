using CentroAcopioApp.DTO;
using CentroAcopioApp.Excepciones;

namespace CentroAcopioApp.Negocio.Validaciones
{
    public class RecursoValidador
    {
        public static void Validar(RecursoDto dto)
        {
            if (dto == null)
                throw new ExcepcionValidacion("El objeto RecursoDto no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ExcepcionValidacion("El nombre del recurso es obligatorio.");

            if (dto.Nombre.Length > 100)
                throw new ExcepcionValidacion("El nombre del recurso no puede superar los 100 caracteres.");

            if (dto.TipoId <= 0)
                throw new ExcepcionValidacion("Debe especificarse un tipo de recurso válido.");

            if (string.IsNullOrWhiteSpace(dto.UnidadMedida))
                throw new ExcepcionValidacion("La unidad de medida es obligatoria.");

            if (dto.UnidadMedida.Length > 20)
                throw new ExcepcionValidacion("La unidad de medida no puede superar los 20 caracteres.");

            if (dto.Vigencia != 'A' && dto.Vigencia != 'I')
                throw new ExcepcionValidacion("El campo Vigencia solo puede ser 'A' (Activo) o 'I' (Inactivo).");
        }
    }
}