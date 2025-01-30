using POSPizzeria.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSPizzeria.Repositories
{
    public interface IProveedorRepository
    {
        Task<List<Proveedore>> ObtenerProveedores();
        Task<Proveedore> ObtenerProveedorById(int idProveedor);
        Task<Proveedore> AgregarProveedor(Proveedore nuevoProveedor);
        Task<Proveedore> EditarProveedor(Proveedore updateProveedor);
    }

    public class ProveedorRepository : IProveedorRepository
    {
        private readonly PizzeriaPosContext _dbContext;

        public ProveedorRepository(PizzeriaPosContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<Proveedore>> ObtenerProveedores()
        {
            return await _dbContext.Proveedores.ToListAsync();
        }


        public async Task<Proveedore> ObtenerProveedorById(int idProveedor)
        {
            return await _dbContext.Proveedores
                .Where(p => p.IdProveedor == idProveedor)
                .FirstOrDefaultAsync();
        }


        public async Task<Proveedore> AgregarProveedor(Proveedore nuevoProveedor)
        {
            _dbContext.Proveedores.Add(nuevoProveedor);
            await _dbContext.SaveChangesAsync();
            return nuevoProveedor;
        }


        public async Task<Proveedore> EditarProveedor(Proveedore updateProveedor)
        {
            _dbContext.Proveedores.Update(updateProveedor);
            await _dbContext.SaveChangesAsync();
            return updateProveedor;
        }
    }
}
