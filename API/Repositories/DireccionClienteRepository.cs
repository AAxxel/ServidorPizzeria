using Microsoft.EntityFrameworkCore;
using POSPizzeria.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSPizzeria.Repositories
{
    public interface IDireccionClienteRepository
    {
        Task<List<DireccionCliente>> ObtenerDireccionesClientes();
        Task<List<DireccionCliente>> ObtenerDireccionesbyCliente(int idCliente);
        Task<DireccionCliente> ObtenerDireccionClienteById(int idDireccion);
        Task<DireccionCliente> AgregarDireccionCliente(DireccionCliente nuevaDireccion);
        Task<DireccionCliente> EditarDireccionCliente(DireccionCliente updateDireccion);
        Task<bool> EliminarDireccionCliente(int idDireccion);
    }

    public class DireccionClienteRepository : IDireccionClienteRepository
    {
        private readonly PizzeriaPosContext _dbContext;

        public DireccionClienteRepository(PizzeriaPosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DireccionCliente>> ObtenerDireccionesClientes()
        {
            return await _dbContext.DireccionClientes.ToListAsync();
        }

        public async Task<List<DireccionCliente>> ObtenerDireccionesbyCliente(int idCliente)
        {
            return await _dbContext.DireccionClientes.Where(d => d.IdCliente == idCliente).ToListAsync();
        }

        public async Task<DireccionCliente> ObtenerDireccionClienteById(int idDireccion)
        {
            return await _dbContext.DireccionClientes
                .Where(d => d.IdDireccion == idDireccion)
                .FirstOrDefaultAsync();
        }

        public async Task<DireccionCliente> AgregarDireccionCliente(DireccionCliente nuevaDireccion)
        {
            _dbContext.DireccionClientes.Add(nuevaDireccion);
            await _dbContext.SaveChangesAsync();
            return nuevaDireccion;
        }

        public async Task<DireccionCliente> EditarDireccionCliente(DireccionCliente updateDireccion)
        {
            _dbContext.DireccionClientes.Update(updateDireccion);
            await _dbContext.SaveChangesAsync();
            return updateDireccion;
        }

        public async Task<bool> EliminarDireccionCliente(int idDireccion)
        {
            var direccion = await ObtenerDireccionClienteById(idDireccion);
            if (direccion != null)
            {
                _dbContext.DireccionClientes.Remove(direccion);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
