using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using POSPizzeria.DTOs;
using POSPizzeria.Models;
using POSPizzeria.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api_pos_pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly PizzeriaPosContext _context;

        public AuthController(IConfiguration config, PizzeriaPosContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .Include(c => c.IdRolNavigation)
                    .FirstOrDefaultAsync(x =>
                        x.NombreUsuario == login.Usuario);

                if (usuario == null)
                    return Unauthorized(new { message = "Usuario no encontrado." });

                if (!LoginServices.VerifyPassword(login.Clave, usuario.Password))
                    return Unauthorized(new { message = "Contraseña incorrecta" });

                var token = GenerateToken(usuario);

                return Ok(new
                {
                    message = "ok",
                    token = token,
                    usuario = new
                    {
                        id = usuario.IdUsuario,
                        nombre = usuario.NombreUsuario,
                        rol = usuario.IdRolNavigation.NombreRol
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        private string GenerateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.IdRolNavigation.NombreRol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}