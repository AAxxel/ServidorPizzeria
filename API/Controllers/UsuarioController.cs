using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSPizzeria.Models;
using POSPizzeria.Repositories;
using POSPizzeria.DTOs;

namespace POSPizzeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        
        [HttpGet]
        [Route("obtener-usuarios")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> obtenerUsuarios()
        {
            try
            {
                var users = await _repository.obtenerUsuarios();
                var usersDto = users.Select(user => new UsuarioDTO
                {
                    IdUsuario = user.IdUsuario,
                    IdRol = (int)user.IdRol,
                    NombreUsuario = user.NombreUsuario,
                    Telefono = user.Telefono,
                    Email = user.Email
                }).ToList();


                return Ok(usersDto);

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("obtener-usuario-Id/{idUsuario:int}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> obtenerUsuarioById(int idUsuario)
        {
            try
            {
                var user = await _repository.obtenerUsuarioById(idUsuario);
                var userDto = new UsuarioDTO();
                userDto.IdUsuario = user.IdUsuario;
                userDto.IdRol = (int)user.IdRol;
                userDto.NombreUsuario = user.NombreUsuario;
                userDto.Telefono = user.Telefono;
                userDto.Email = user.Email;

                return Ok(userDto);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("agregar-usuario")]
   //     [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> agregarUsuarios([FromBody] CreateUsuarioDTO nuevoUsuario)
        {
            try
            {
                if (string.IsNullOrEmpty(nuevoUsuario.NombreUsuario))
                {
                    return BadRequest("El nombre de usuario es requerido.");
                }

                if (string.IsNullOrEmpty(nuevoUsuario.Password))
                {
                    return BadRequest("La contraseña es requerida.");
                }

                if (string.IsNullOrEmpty(nuevoUsuario.Telefono))
                {
                    return BadRequest("El teléfono es requerido.");
                }

                if (string.IsNullOrEmpty(nuevoUsuario.Email))
                {
                    return BadRequest("El correo electrónico es requerido.");
                }

                if (nuevoUsuario.IdRol == 0 || nuevoUsuario.IdRol == null)
                {
                    return BadRequest("El rol es requerido.");
                }

                Usuario usuario = new Usuario
                {
                    IdRol = nuevoUsuario.IdRol,
                    NombreUsuario = nuevoUsuario.NombreUsuario,
                    Telefono = nuevoUsuario.Telefono,
                    Email = nuevoUsuario.Email,
                    Password = nuevoUsuario.Password
                };

                usuario = await _repository.agregarUsuarios(usuario);
                var userDto = new UsuarioDTO();
                userDto.IdUsuario = usuario.IdUsuario;
                userDto.IdRol = (int)usuario.IdRol;
                userDto.NombreUsuario = usuario.NombreUsuario;
                userDto.Telefono = usuario.Telefono;
                userDto.Email = usuario.Email;

                return Ok(userDto);

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar-usuario")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> editarUsuarios([FromBody] UpdateUsuarioDTO updateUsuario)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return BadRequest("Datos inválidos.");
                }

                
                Usuario usuario = await _repository.obtenerUsuarioById(updateUsuario.IdUsuario);

                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                
                usuario.NombreUsuario = updateUsuario.NombreUsuario ?? usuario.NombreUsuario;
                usuario.Telefono = updateUsuario.Telefono ?? usuario.Telefono;
                usuario.Email = updateUsuario.Email ?? usuario.Email;
                usuario.IdRol = updateUsuario.IdRol == 0 ? usuario.IdRol : updateUsuario.IdRol;

             
                usuario = await _repository.editarUsuarios(usuario);

                
                var userDto = new UsuarioDTO
                {
                    IdUsuario = usuario.IdUsuario,
                    IdRol = (int)usuario.IdRol,
                    NombreUsuario = usuario.NombreUsuario,
                    Telefono = usuario.Telefono,
                    Email = usuario.Email
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
