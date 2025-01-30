using Microsoft.AspNetCore.Mvc;
using POSPizzeria.DTOs;
using POSPizzeria.Models;
using POSPizzeria.Repositories;

namespace POSPizzeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionClienteController : ControllerBase
    {
        private readonly IDireccionClienteRepository _repository;

        public DireccionClienteController(IDireccionClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("obtener-direcciones-clientes")]
        public async Task<IActionResult> ObtenerDireccionesClientes()
        {
            try
            {
                var direcciones = await _repository.ObtenerDireccionesClientes();
                var direccionesDto = direcciones.Select(d => new DireccionClienteDTO
                {
                    IdDireccion = d.IdDireccion,
                    IdCliente = d.IdCliente,
                    Direccion = d.Direccion
                }).ToList();

                return Ok(direccionesDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("obtener-direcciones-cliente/{idCliente:int}")]
        public async Task<IActionResult> ObtenerDireByCliente(int idCliente)
        {
            try
            {
                var direcciones = await _repository.ObtenerDireccionesbyCliente(idCliente);
                var direccionesDto = direcciones.Select(d => new DireccionClienteDTO
                {
                    IdDireccion = d.IdDireccion,
                    IdCliente = d.IdCliente,
                    Direccion = d.Direccion
                }).ToList();

                return Ok(direccionesDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("obtener-direccion/{idDireccion:int}")]
        public async Task<IActionResult> ObtenerDireccionClienteById(int idDireccion)
        {
            try
            {
                var direccion = await _repository.ObtenerDireccionClienteById(idDireccion);
                if (direccion == null)
                {
                    return NotFound("Dirección no encontrada.");
                }

                var direccionDto = new DireccionClienteDTO
                {
                    IdDireccion = direccion.IdDireccion,
                    IdCliente = direccion.IdCliente
                };

                return Ok(direccionDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("agregar-direccion-cliente")]
        public async Task<IActionResult> AgregarDireccionCliente([FromBody] CreateDireccionClienteDTO nuevaDireccion)
        {
            try
            {
                if (nuevaDireccion.IdCliente == 0)
                {
                    return BadRequest("El ID del cliente es requerido.");
                }

                var direccion = new DireccionCliente
                {
                    IdCliente = nuevaDireccion.IdCliente
                };

                direccion = await _repository.AgregarDireccionCliente(direccion);

                var direccionDto = new DireccionClienteDTO
                {
                    IdDireccion = direccion.IdDireccion,
                    IdCliente = direccion.IdCliente,
                    Direccion = direccion.Direccion
                };

                return Ok(direccionDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar-direccion-cliente")]
        public async Task<IActionResult> EditarDireccionCliente([FromBody] UpdateDireccionClienteDTO updateDireccion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Datos inválidos.");
                }

                var direccion = await _repository.ObtenerDireccionClienteById(updateDireccion.IdDireccion);
                if (direccion == null)
                {
                    return NotFound("Dirección no encontrada.");
                }

                direccion.IdCliente = updateDireccion.IdCliente ?? direccion.IdCliente;

                direccion = await _repository.EditarDireccionCliente(direccion);

                var direccionDto = new DireccionClienteDTO
                {
                    IdDireccion = direccion.IdDireccion,
                    IdCliente = direccion.IdCliente,
                    Direccion = direccion.Direccion
                };

                return Ok(direccionDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Eliminar una dirección de cliente
        [HttpDelete]
        [Route("eliminar-direccion-cliente/{idDireccion:int}")]
        public async Task<IActionResult> EliminarDireccionCliente(int idDireccion)
        {
            try
            {
                var eliminado = await _repository.EliminarDireccionCliente(idDireccion);
                if (!eliminado)
                {
                    return NotFound("Dirección no encontrada.");
                }

                return Ok("Dirección eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
