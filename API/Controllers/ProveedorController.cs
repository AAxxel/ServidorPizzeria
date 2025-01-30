using Microsoft.AspNetCore.Mvc;
using POSPizzeria.DTOs;
using POSPizzeria.Models;
using POSPizzeria.Repositories;

namespace POSPizzeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorRepository _repository;

        public ProveedorController(IProveedorRepository repository)
        {
            _repository = repository;
        }

        // Obtener todos los proveedores
        [HttpGet]
        [Route("obtener-proveedores")]
        public async Task<IActionResult> ObtenerProveedores()
        {
            try
            {
                var proveedores = await _repository.ObtenerProveedores();
                var proveedoresDto = proveedores.Select(p => new ProveedorDTO
                {
                    IdProveedor = p.IdProveedor,
                    NombreProveedor = p.NombreProveedor,
                    Direccion = p.Direccion,
                    Telefono = p.Telefono,
                    Email = p.Email
                }).ToList();

                return Ok(proveedoresDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Obtener proveedor por ID
        [HttpGet]
        [Route("obtener-proveedor-Id/{idProveedor:int}")]
        public async Task<IActionResult> ObtenerProveedorById(int idProveedor)
        {
            try
            {
                var proveedor = await _repository.ObtenerProveedorById(idProveedor);
                if (proveedor == null)
                {
                    return NotFound("Proveedor no encontrado.");
                }

                var proveedorDto = new ProveedorDTO
                {
                    IdProveedor = proveedor.IdProveedor,
                    NombreProveedor = proveedor.NombreProveedor,
                    Direccion = proveedor.Direccion,
                    Telefono = proveedor.Telefono,
                    Email = proveedor.Email
                };

                return Ok(proveedorDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Agregar un nuevo proveedor
        [HttpPost]
        [Route("agregar-proveedor")]
        public async Task<IActionResult> AgregarProveedor([FromBody] CreateProveedorDTO nuevoProveedor)
        {
            try
            {
                if (string.IsNullOrEmpty(nuevoProveedor.NombreProveedor))
                {
                    return BadRequest("El nombre del proveedor es requerido.");
                }

                if (string.IsNullOrEmpty(nuevoProveedor.Telefono))
                {
                    return BadRequest("El teléfono del proveedor es requerido.");
                }

                if (string.IsNullOrEmpty(nuevoProveedor.Email))
                {
                    return BadRequest("El correo electrónico del proveedor es requerido.");
                }

                var proveedor = new Proveedore
                {
                    NombreProveedor = nuevoProveedor.NombreProveedor,
                    Direccion = nuevoProveedor.Direccion,
                    Telefono = nuevoProveedor.Telefono,
                    Email = nuevoProveedor.Email
                };

                proveedor = await _repository.AgregarProveedor(proveedor);

                var proveedorDto = new ProveedorDTO
                {
                    IdProveedor = proveedor.IdProveedor,
                    NombreProveedor = proveedor.NombreProveedor,
                    Direccion = proveedor.Direccion,
                    Telefono = proveedor.Telefono,
                    Email = proveedor.Email
                };

                return Ok(proveedorDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Editar un proveedor existente
        [HttpPut]
        [Route("editar-proveedor")]
        public async Task<IActionResult> EditarProveedor([FromBody] ProveedorDTO updateProveedor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Datos inválidos.");
                }

                var proveedor = await _repository.ObtenerProveedorById(updateProveedor.IdProveedor);
                if (proveedor == null)
                {
                    return NotFound("Proveedor no encontrado.");
                }

                proveedor.NombreProveedor = updateProveedor.NombreProveedor ?? proveedor.NombreProveedor;
                proveedor.Direccion = updateProveedor.Direccion ?? proveedor.Direccion;
                proveedor.Telefono = updateProveedor.Telefono ?? proveedor.Telefono;
                proveedor.Email = updateProveedor.Email ?? proveedor.Email;

                proveedor = await _repository.EditarProveedor(proveedor);

                var proveedorDto = new ProveedorDTO
                {
                    IdProveedor = proveedor.IdProveedor,
                    NombreProveedor = proveedor.NombreProveedor,
                    Direccion = proveedor.Direccion,
                    Telefono = proveedor.Telefono,
                    Email = proveedor.Email
                };

                return Ok(proveedorDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
