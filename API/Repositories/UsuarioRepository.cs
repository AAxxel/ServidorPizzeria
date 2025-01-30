using System.Collections.Generic;
using POSPizzeria.Models;
using Microsoft.EntityFrameworkCore;
using POSPizzeria.Services;
namespace POSPizzeria.Repositories
{
    public interface IUsuarioRepository
    {
        public Task<List<Usuario>> obtenerUsuarios();
        public Task<Usuario> obtenerUsuarioById(int idUsuario);
        public Task<Usuario> agregarUsuarios(Usuario newUsuario);
        public Task<Usuario> editarUsuarios(Usuario newUsuario);
    }
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly PizzeriaPosContext _dbContext;
        public UsuarioRepository(PizzeriaPosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Usuario>> obtenerUsuarios()
        {
            List<Usuario> listaUsuario = new List<Usuario>();
            listaUsuario = await _dbContext.Usuarios.ToListAsync();
            return listaUsuario;
        }

        public async Task<Usuario> obtenerUsuarioById(int idUsuario)
        {
            Usuario? usuario = await _dbContext.Usuarios.FindAsync(idUsuario);
            if (usuario == null)
            {
                return null;
            }

            usuario = await _dbContext.Usuarios.Where(p => p.IdUsuario == idUsuario).FirstOrDefaultAsync();

            return usuario;
        }

        public async Task<Usuario> agregarUsuarios(Usuario newUsuario)
        {
            newUsuario.Password = LoginServices.HashPassword(newUsuario.Password);
            _dbContext.Add(newUsuario);
            await _dbContext.SaveChangesAsync();
            return newUsuario;
        }

        public async Task<Usuario> editarUsuarios(Usuario newUsuario)
        {
            _dbContext.Update(newUsuario);
            await _dbContext.SaveChangesAsync();
            return newUsuario;
        }
    }
}
