namespace CentroAcopioApp.DTO
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public char Vigencia { get; set; }
    }
}