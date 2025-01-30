using POSPizzeria.Models;

namespace POSPizzeria.Services
{
    public interface IPedidoService
    {
        decimal CalcularTotal(ICollection<DetallePedido> detalles);
    }

    public class PedidoService : IPedidoService
    {
        
        public decimal CalcularTotal(ICollection<DetallePedido> detalles)
        {
            try
            {
                
                return detalles.Sum(d => d.Subtotal);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular el total del pedido: " + ex.Message);
            }
        }
    }
}
