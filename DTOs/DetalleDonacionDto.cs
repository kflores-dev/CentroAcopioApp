using System;

namespace CentroAcopioApp.DTO
{
    public class DetalleDonacionDto
    {
        public int Id { get; set; }

        public int DonacionId { get; set; }

        // public DateTime DonacionFecha { get; set; } // viene del join con donacion
        public int RecursoId { get; set; }
        public string RecursoNombre { get; set; } // viene del join con recurso
        public decimal CantidadDonada { get; set; }
        public int UbicacionId { get; set; }
        public string UbicacionNombre { get; set; } // viene del join con ubicacion
        public char Vigencia { get; set; }
    }
}