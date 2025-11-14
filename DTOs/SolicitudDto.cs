using System;

namespace CentroAcopioApp.DTO
{
    public class SolicitudDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public string Prioridad { get; set; }
        public string Observaciones { get; set; }
        public char Vigencia { get; set; }
    }
}