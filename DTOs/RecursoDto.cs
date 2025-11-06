namespace CentroAcopioApp.DTO
{
    public class RecursoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TipoId { get; set; }
        public string TipoNombre { get; set; } // viene del join con tipo_recurso
        public string UnidadMedida { get; set; }
        public char Vigencia { get; set; }
    }
}