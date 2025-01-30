using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POSPizzeria.DTOs;
using POSPizzeria.Models;
using POSPizzeria.Repositories;

namespace POSPizzeria.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _repository;

        public ProductoController(IProductoRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Route("obtener-productos")]
        public async Task<IActionResult> ObtenerProductos()
        {
            try
            {
                var productos = await _repository.ObtenerProductos();
                var productosDto = productos.Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto,
                    PrecioProducto = p.PrecioProducto,
                    Impuesto = p.Impuesto,
                    Stock = p.Stock,
                    IdCategoria = p.IdCategoria,
                    IdProveedor = p.IdProveedor
                }).ToList();

                return Ok(productosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        [HttpGet]
        [Route("obtener-producto-Id/{idProducto:int}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> ObtenerProductoById(int idProducto)
        {
            try
            {
                var producto = await _repository.ObtenerProductoById(idProducto);
                if (producto == null)
                {
                    return NotFound("Producto no encontrado.");
                }

                var productoDto = new ProductoDTO
                {
                    IdProducto = producto.IdProducto,
                    NombreProducto = producto.NombreProducto,
                    PrecioProducto = producto.PrecioProducto,
                    Impuesto = producto.Impuesto,
                    Stock = producto.Stock,
                    IdCategoria = producto.IdCategoria,
                    IdProveedor = producto.IdProveedor
                };

                return Ok(productoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Agregar un nuevo producto
        [HttpPost]
        [Route("agregar-producto")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> AgregarProducto([FromBody] CreateProductoDTO nuevoProducto)
        {
            try
            {
                if (string.IsNullOrEmpty(nuevoProducto.NombreProducto))
                {
                    return BadRequest("El nombre del producto es requerido.");
                }

                if (nuevoProducto.PrecioProducto == null)
                {
                    return BadRequest("El precio del producto es requerido.");
                }

                var producto = new Producto
                {
                    NombreProducto = nuevoProducto.NombreProducto,
                    PrecioProducto = nuevoProducto.PrecioProducto,
                    Impuesto = nuevoProducto.Impuesto,
                    Stock = nuevoProducto.Stock,
                    IdCategoria = nuevoProducto.IdCategoria,
                    IdProveedor = nuevoProducto.IdProveedor
                };

                producto = await _repository.AgregarProducto(producto);

                var productoDto = new ProductoDTO
                {
                    IdProducto = producto.IdProducto,
                    NombreProducto = producto.NombreProducto,
                    PrecioProducto = producto.PrecioProducto,
                    Impuesto = producto.Impuesto,
                    Stock = producto.Stock,
                    IdCategoria = producto.IdCategoria,
                    IdProveedor = producto.IdProveedor
                };

                return Ok(productoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Editar un producto existente
        [HttpPut]
        [Route("editar-producto")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> EditarProducto([FromBody] UpdateProductoDTO updateProducto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Datos inválidos.");
                }

                var producto = await _repository.ObtenerProductoById(updateProducto.IdProducto);
                if (producto == null)
                {
                    return NotFound("Producto no encontrado.");
                }

                producto.NombreProducto = updateProducto.NombreProducto ?? producto.NombreProducto;
                producto.PrecioProducto = updateProducto.PrecioProducto ?? producto.PrecioProducto;
                producto.Impuesto = updateProducto.Impuesto ?? producto.Impuesto;
                producto.Stock = updateProducto.Stock ?? producto.Stock;
                producto.IdCategoria = updateProducto.IdCategoria ?? producto.IdCategoria;
                producto.IdProveedor = updateProducto.IdProveedor ?? producto.IdProveedor;

                producto = await _repository.EditarProducto(producto);

                var productoDto = new ProductoDTO
                {
                    IdProducto = producto.IdProducto,
                    NombreProducto = producto.NombreProducto,
                    PrecioProducto = producto.PrecioProducto,
                    Impuesto = producto.Impuesto,
                    Stock = producto.Stock,
                    IdCategoria = producto.IdCategoria,
                    IdProveedor = producto.IdProveedor
                };

                return Ok(productoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
