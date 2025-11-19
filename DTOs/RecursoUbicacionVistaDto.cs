namespace CentroAcopioApp.DTO
{
    public class RecursoUbicacionVistaDto
    {
        public int Id { get; set; } // Id de la relación Recurso-Ubicacion
        public int UbicacionId { get; set; } // Id real de la ubicación
        public string UbicacionNombre { get; set; }
        public decimal Cantidad { get; set; }
    }
}