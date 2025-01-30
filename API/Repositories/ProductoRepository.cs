using Microsoft.EntityFrameworkCore;
using POSPizzeria.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSPizzeria.Repositories
{
    public interface IProductoRepository
    {
        Task<List<Producto>> ObtenerProductos();
        Task<Producto> ObtenerProductoById(int idProducto);
        Task<Producto> AgregarProducto(Producto nuevoProducto);
        Task<Producto> EditarProducto(Producto updateProducto);
    }

    public class ProductoRepository : IProductoRepository
    {
        private readonly PizzeriaPosContext _dbContext;

        public ProductoRepository(PizzeriaPosContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener todos los productos
        public async Task<List<Producto>> ObtenerProductos()
        {
            return await _dbContext.Productos.ToListAsync();
        }

        // Obtener un producto por ID
        public async Task<Producto> ObtenerProductoById(int idProducto)
        {
            return await _dbContext.Productos
                .Where(p => p.IdProducto == idProducto)
                .FirstOrDefaultAsync();
        }

        // Agregar un nuevo producto
        public async Task<Producto> AgregarProducto(Producto nuevoProducto)
        {
            _dbContext.Productos.Add(nuevoProducto);
            await _dbContext.SaveChangesAsync();
            return nuevoProducto;
        }

        // Editar un producto existente
        public async Task<Producto> EditarProducto(Producto updateProducto)
        {
            _dbContext.Productos.Update(updateProducto);
            await _dbContext.SaveChangesAsync();
            return updateProducto;
        }
    }
}
