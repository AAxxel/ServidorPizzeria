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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;    
        public ClienteController(IClienteRepository repository)
        {
            _repository = repository;
        }

        
        [HttpGet]
        [Route("obtener-clientes")]
        public async Task<IActionResult> obtenerClientes()
        {
            try
            {
                var clientes = await _repository.obtenerClientes();
                var clientesDto = clientes.Select(cliente => new ClientesDTO
                {
                    IdCliente = cliente.IdCliente,
                    NombreCliente = cliente.NombreCliente,
                    Telefono = cliente.Telefono,
                    Email = cliente.Email,
                }).ToList();

                return Ok(clientesDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Gerente")]
        [HttpGet]
        [Route("obtener-cliente-Id/{idCliente:int}")]
        public async Task<IActionResult> obtenerClienteById(int idCliente)
        {
            try
            {
                var cliente = await _repository.obtenerClienteById(idCliente);
                if (cliente == null)
                {
                    return NotFound("Cliente no encontrado.");
                }

                var clienteDto = new ClientesDTO
                {
                    IdCliente = cliente.IdCliente,
                    NombreCliente = cliente.NombreCliente,
                    Telefono = cliente.Telefono,
                    Email = cliente.Email,
                };

                return Ok(clienteDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("agregar-cliente")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> agregarCliente([FromBody] CreateClientesDTO nuevoCliente)
        {
            try
            {
                if (string.IsNullOrEmpty(nuevoCliente.NombreCliente))
                {
                    return BadRequest("El nombre de cliente es requerido.");
                }

                if (string.IsNullOrEmpty(nuevoCliente.Telefono))
                {
                    return BadRequest("El teléfono es requerido.");
                }

                if (string.IsNullOrEmpty(nuevoCliente.Email))
                {
                    return BadRequest("El correo electrónico es requerido.");
                }


                Cliente cliente = new Cliente
                {
                    NombreCliente = nuevoCliente.NombreCliente,
                    Telefono = nuevoCliente.Telefono,
                    Email = nuevoCliente.Email,
 
                };

                cliente = await _repository.agregarCliente(cliente);
                var clienteDto = new ClientesDTO
                {
                    IdCliente = cliente.IdCliente,
                    NombreCliente = cliente.NombreCliente,
                    Telefono = cliente.Telefono,
                    Email = cliente.Email,
                };

                return Ok(clienteDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar-cliente")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> editarCliente([FromBody] UpdateClientesDTO updateCliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Datos inválidos.");
                }

                Cliente cliente = await _repository.obtenerClienteById(updateCliente.IdCliente);

                if (cliente == null)
                {
                    return NotFound("Cliente no encontrado.");
                }

                cliente.NombreCliente = updateCliente.NombreCliente ?? cliente.NombreCliente;
                cliente.Telefono = updateCliente.Telefono ?? cliente.Telefono;
                cliente.Email = updateCliente.Email ?? cliente.Email;


                cliente = await _repository.editarCliente(cliente);

                var clienteDto = new ClientesDTO
                {
                    IdCliente = cliente.IdCliente,
                    NombreCliente = cliente.NombreCliente,
                    Telefono = cliente.Telefono,
                    Email = cliente.Email,

                };

                return Ok(clienteDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
