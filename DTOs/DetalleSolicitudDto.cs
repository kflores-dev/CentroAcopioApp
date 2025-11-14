namespace CentroAcopioApp.DTO
{
    public class DetalleSolicitudDto
    {
        public int Id { get; set; }
        public int SolicitudId { get; set; }
        public string SolicitudNombre { get; set; } // viene del join con solicitud
        public int RecursoId { get; set; }
        public string RecursoNombre { get; set; } // viene del join con recurso
        public decimal CantidadSolicitada { get; set; }
        public decimal CantidadEntregada { get; set; }
        public char Vigencia { get; set; }
    }
}