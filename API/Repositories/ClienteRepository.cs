using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using POSPizzeria.Models;
using POSPizzeria.Services;

namespace POSPizzeria.Repositories
{
    public interface IClienteRepository
    {
        public Task<List<Cliente>> obtenerClientes();
        public Task<Cliente> obtenerClienteById(int idCliente);
        public Task<Cliente> agregarCliente(Cliente nuevoCliente);
        public Task<Cliente> editarCliente(Cliente updateCliente);
    }

    public class ClienteRepository : IClienteRepository
    {
        private readonly PizzeriaPosContext _dbContext;

        public ClienteRepository(PizzeriaPosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Cliente>> obtenerClientes()
        {
            List<Cliente> listaCliente = new List<Cliente>();
            listaCliente = await _dbContext.Clientes.ToListAsync();
            return listaCliente;
        }

        public async Task<Cliente> obtenerClienteById(int idCliente)
        {
            Cliente? cliente = await _dbContext.Clientes.FindAsync(idCliente);
            if (cliente == null)
            {
                return null;
            }

            cliente = await _dbContext.Clientes.Where(p => p.IdCliente == idCliente).FirstOrDefaultAsync();
            return cliente;
        }

        public async Task<Cliente> agregarCliente(Cliente nuevoCliente)
        {
            _dbContext.Add(nuevoCliente);
            await _dbContext.SaveChangesAsync();
            return nuevoCliente;
        }

        public async Task<Cliente> editarCliente(Cliente updateCliente)
        {
            _dbContext.Update(updateCliente);
            await _dbContext.SaveChangesAsync();
            return updateCliente;
        }
    }
}
