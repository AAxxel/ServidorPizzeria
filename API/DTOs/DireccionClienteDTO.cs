namespace POSPizzeria.DTOs
{
    public class DireccionClienteDTO
    {
        public int IdDireccion { get; set; }
        public int? IdCliente { get; set; }
        public string? Direccion { get; set; }
    }

    public class CreateDireccionClienteDTO
    {
        public int? IdCliente { get; set; }
        public string? Direccion { get; set; }
    }

    public class UpdateDireccionClienteDTO
    {
        public int IdDireccion { get; set; }
        public int? IdCliente { get; set; }
        public string? Direccion { get; set; }
    }

    
     public class DeleteDireccionClienteDTO
     {
      public int IdDireccion { get; set; }
     }
    


}
