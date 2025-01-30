namespace POSPizzeria.DTOs
{
    public class DetallePedidoDTO
    {
        public int IdDetalle { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; } 
    }
}

namespace POSPizzeria.DTOs
{
    public class PedidoDTO
    {
        public int IdPedido { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdDireccionCliente { get; set; }
        public decimal? Total { get; set; }
        public List<DetallePedidoDTO> Detalles { get; set; } = new List<DetallePedidoDTO>();
    }
    public class CreatePedidoDTO
    {
        public int IdEmpleado { get; set; }
        public int IdDireccionCliente { get; set; }
        public List<CreateDetallePedidoDTO> Detalles { get; set; } = new List<CreateDetallePedidoDTO>();
    }

    public class CreateDetallePedidoDTO
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
