using System;

namespace CentroAcopioApp.DTO
{
    public class DonacionDto
    {
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public string ProveedorNombre { get; set; } // viene del join con proveedor
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public char Vigencia { get; set; }
    }
}