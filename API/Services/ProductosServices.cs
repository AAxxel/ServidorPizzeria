using POSPizzeria.DTOs;
using POSPizzeria.Models;
using POSPizzeria.Repositories;

namespace POSPizzeria.Services
{
    public interface IProductoService
    {
        public Task<Producto> obtenerProductoById(int idProducto);
    }

    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;
        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }
        public async Task<Producto> obtenerProductoById(int idProducto)
        {
            try
            {

                var producto = await _repository.ObtenerProductoById(idProducto);    

                return producto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular el total del pedido: " + ex.Message);
            }
        }
    }
}
