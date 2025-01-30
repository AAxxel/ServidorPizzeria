namespace POSPizzeria.DTOs
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }
        public string? NombreProveedor { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
    }

    public class CreateProveedorDTO
    {
        public string? NombreProveedor { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
    }

    public class UpdateProveedorDTO
    {
        public int IdProveedor { get; set; }
        public string? NombreProveedor { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
    }
}
