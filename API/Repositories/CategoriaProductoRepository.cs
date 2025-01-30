using POSPizzeria.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace POSPizzeria.Repositories
{
    public interface ICategoriaProductoRepository
    {
        Task<List<CategoriaProducto>> ObtenerCategoriasProductos();
        Task<CategoriaProducto> ObtenerCategoriaProductoById(int idCategoria);
        Task<CategoriaProducto> AgregarCategoriaProducto(CategoriaProducto nuevaCategoria);
        Task<CategoriaProducto> EditarCategoriaProducto(CategoriaProducto updateCategoria);
    }

    public class CategoriaProductoRepository : ICategoriaProductoRepository
    {
        private readonly PizzeriaPosContext _dbContext;

        public CategoriaProductoRepository(PizzeriaPosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CategoriaProducto>> ObtenerCategoriasProductos()
        {
            return await _dbContext.CategoriaProductos.ToListAsync();
        }

        public async Task<CategoriaProducto> ObtenerCategoriaProductoById(int idCategoria)
        {
            return await _dbContext.CategoriaProductos
                .Where(c => c.IdCategoria == idCategoria)
                .FirstOrDefaultAsync();
        }

        public async Task<CategoriaProducto> AgregarCategoriaProducto(CategoriaProducto nuevaCategoria)
        {
            _dbContext.CategoriaProductos.Add(nuevaCategoria);
            await _dbContext.SaveChangesAsync();
            return nuevaCategoria;
        }

        public async Task<CategoriaProducto> EditarCategoriaProducto(CategoriaProducto updateCategoria)
        {
            _dbContext.CategoriaProductos.Update(updateCategoria);
            await _dbContext.SaveChangesAsync();
            return updateCategoria;
        }
    }
}
