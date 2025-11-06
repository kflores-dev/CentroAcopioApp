namespace CentroAcopioApp.DTO
{
    public class RecursoUbicacionDto
    {
        public int Id { get; set; }
        public int RecursoId { get; set; }
        public string RecursoNombre { get; set; } // viene del join con recurso
        public int UbicacionId { get; set; }
        public string UbicacionNombre { get; set; } // viene del join con ubicacion
        public decimal Cantidad { get; set; }
        public char Vigencia { get; set; }
    }
}