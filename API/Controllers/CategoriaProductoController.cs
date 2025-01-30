using Microsoft.AspNetCore.Mvc;
using POSPizzeria.DTOs;
using POSPizzeria.Models;
using POSPizzeria.Repositories;

namespace POSPizzeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaProductoController : ControllerBase
    {
        private readonly ICategoriaProductoRepository _repository;

        public CategoriaProductoController(ICategoriaProductoRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Route("obtener-categorias-productos")]
        public async Task<IActionResult> ObtenerCategoriasProductos()
        {
            try
            {
                var categorias = await _repository.ObtenerCategoriasProductos();
                var categoriasDto = categorias.Select(c => new CategoriaProductoDTO
                {
                    IdCategoria = c.IdCategoria,
                    NombreCategoria = c.NombreCategoria,
                    Descripcion = c.Descripcion
                }).ToList();

                return Ok(categoriasDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("obtener-categoria-producto-Id/{idCategoria:int}")]
        public async Task<IActionResult> ObtenerCategoriaProductoById(int idCategoria)
        {
            try
            {
                var categoria = await _repository.ObtenerCategoriaProductoById(idCategoria);
                if (categoria == null)
                {
                    return NotFound("Categoría de producto no encontrada.");
                }

                var categoriaDto = new CategoriaProductoDTO
                {
                    IdCategoria = categoria.IdCategoria,
                    NombreCategoria = categoria.NombreCategoria,
                    Descripcion = categoria.Descripcion
                };

                return Ok(categoriaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("agregar-categoria-producto")]
        public async Task<IActionResult> AgregarCategoriaProducto([FromBody] CreateCategoriaProductoDTO nuevaCategoria)
        {
            try
            {
                if (string.IsNullOrEmpty(nuevaCategoria.NombreCategoria))
                {
                    return BadRequest("El nombre de la categoría es requerido.");
                }

                var categoria = new CategoriaProducto
                {
                    NombreCategoria = nuevaCategoria.NombreCategoria,
                    Descripcion = nuevaCategoria.Descripcion
                };

                categoria = await _repository.AgregarCategoriaProducto(categoria);

                var categoriaDto = new CategoriaProductoDTO
                {
                    IdCategoria = categoria.IdCategoria,
                    NombreCategoria = categoria.NombreCategoria,
                    Descripcion = categoria.Descripcion
                };

                return Ok(categoriaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("editar-categoria-producto")]
        public async Task<IActionResult> EditarCategoriaProducto([FromBody] UpdateCategoriaProductoDTO updateCategoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Datos inválidos.");
                }

                var categoria = await _repository.ObtenerCategoriaProductoById(updateCategoria.IdCategoria);
                if (categoria == null)
                {
                    return NotFound("Categoría de producto no encontrada.");
                }

                categoria.NombreCategoria = updateCategoria.NombreCategoria ?? categoria.NombreCategoria;
                categoria.Descripcion = updateCategoria.Descripcion ?? categoria.Descripcion;

                categoria = await _repository.EditarCategoriaProducto(categoria);

                var categoriaDto = new CategoriaProductoDTO
                {
                    IdCategoria = categoria.IdCategoria,
                    NombreCategoria = categoria.NombreCategoria,
                    Descripcion = categoria.Descripcion
                };

                return Ok(categoriaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
