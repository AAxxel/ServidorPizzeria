using Microsoft.EntityFrameworkCore;
using POSPizzeria.Models;
using POSPizzeria.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSPizzeria.Repositories
{
    public interface IPedidoRepository
    {
        Task<List<Pedido>> ObtenerPedidos();
        Task<Pedido> ObtenerPedidoById(int idPedido);
        Task<Pedido> CrearPedido(Pedido nuevoPedido, List<DetallePedido> detalles);
        Task<Pedido> ActualizarPedido(Pedido pedido);
    }

    public class PedidoRepository : IPedidoRepository
    {
        private readonly PizzeriaPosContext _dbContext;
        private readonly IPedidoService _pedidoService;

        public PedidoRepository(PizzeriaPosContext dbContext, IPedidoService pedidoService)
        {
            _dbContext = dbContext;
            _pedidoService = pedidoService;
        }

  
        public async Task<List<Pedido>> ObtenerPedidos()
        {
            return await _dbContext.Pedidos
                .Include(p => p.DetallePedidos)
                .ThenInclude(dp => dp.IdProductoNavigation)  
                .ToListAsync();
        }


        public async Task<Pedido> ObtenerPedidoById(int idPedido)
        {
            return await _dbContext.Pedidos
                .Include(p => p.DetallePedidos)
                .ThenInclude(dp => dp.IdProductoNavigation)  
                .FirstOrDefaultAsync(p => p.IdPedido == idPedido);
        }

 
        public async Task<Pedido> CrearPedido(Pedido nuevoPedido, List<DetallePedido> detalles)
        {

            nuevoPedido.Total = _pedidoService.CalcularTotal(detalles);


            nuevoPedido.DetallePedidos = detalles;


            await _dbContext.Pedidos.AddAsync(nuevoPedido);
            await _dbContext.SaveChangesAsync();

            return nuevoPedido;
        }


        public async Task<Pedido> ActualizarPedido(Pedido pedido)
        {
            var pedidoExistente = await _dbContext.Pedidos
                .Include(p => p.DetallePedidos)
                .FirstOrDefaultAsync(p => p.IdPedido == pedido.IdPedido);

            if (pedidoExistente != null)
            {

                pedido.Total = _pedidoService.CalcularTotal(pedido.DetallePedidos);


                _dbContext.Pedidos.Update(pedido);
                await _dbContext.SaveChangesAsync();
            }

            return pedidoExistente;
        }
    }

}
