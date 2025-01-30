namespace POSPizzeria.DTOs
{
    public class ClientesDTO
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }

    public class CreateClientesDTO
    {
        public string NombreCliente { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }

    public class UpdateClientesDTO
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }

}
