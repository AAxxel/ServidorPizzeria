namespace POSPizzeria.DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
    }

    public class CreateUsuarioDTO
    {
        public int IdRol { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class UpdateUsuarioDTO
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }

    }
}