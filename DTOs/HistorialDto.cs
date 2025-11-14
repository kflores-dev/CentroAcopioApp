using System;

namespace CentroAcopioApp.DTO
{
    public class HistorialDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; } // viene del join con usuario
        public string Accion { get; set; }
        public string Entidad { get; set; }
        public int EntidadId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Descripcion { get; set; }
        public char Vigencia { get; set; }
    }
}