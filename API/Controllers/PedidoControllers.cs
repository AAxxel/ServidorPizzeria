using Microsoft.AspNetCore.Mvc;
using POSPizzeria.DTOs;
using POSPizzeria.Models;
using POSPizzeria.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using POSPizzeria.Services;

namespace POSPizzeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProductoService _productoService;

        public PedidoController(IPedidoRepository pedidoRepository, IProductoService productoService)
        {
            _pedidoRepository = pedidoRepository;
            _productoService = productoService;
        }

        [HttpGet]
        [Route("obtener-pedidos")]
        public async Task<IActionResult> ObtenerPedidos()
        {
            try
            {
                var pedidos = await _pedidoRepository.ObtenerPedidos();

                var pedidosDto = new List<PedidoDTO>();

                foreach (var p in pedidos)
                {
                    var detallesDto = new List<DetallePedidoDTO>();

                    // Aquí procesas cada detalle uno por uno, sin usar Task.WhenAll.
                    foreach (var d in p.DetallePedidos)
                    {
                        var producto = await _productoService.obtenerProductoById(d.IdProducto);

                        if (producto != null)
                        {
                            var subtotal = (producto.PrecioProducto ?? 0) * d.Cantidad;
                            detallesDto.Add(new DetallePedidoDTO
                            {
                                IdDetalle = d.IdDetalle,
                                IdPedido = d.IdPedido,
                                IdProducto = d.IdProducto,
                                Cantidad = d.Cantidad,
                                Subtotal = subtotal
                            });
                        }
                    }

                    pedidosDto.Add(new PedidoDTO
                    {
                        IdPedido = p.IdPedido,
                        IdEmpleado = p.IdEmpleado,
                        IdDireccionCliente = p.IdDireccionCliente,
                        Total = p.Total,
                        Detalles = detallesDto
                    });
                }

                return Ok(pedidosDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener pedidos: {ex.Message}");
            }
        
        }

        [HttpPost]
        [Route("crear-pedido")]
        public async Task<IActionResult> CrearPedido([FromBody] CreatePedidoDTO nuevoPedidoDto)
        {
            try
            {
                if (nuevoPedidoDto.Detalles == null || !nuevoPedidoDto.Detalles.Any())
                {
                    return BadRequest("El pedido debe tener al menos un detalle.");
                }

                var nuevoPedido = new Pedido
                {
                    IdEmpleado = nuevoPedidoDto.IdEmpleado,
                    IdDireccionCliente = nuevoPedidoDto.IdDireccionCliente
                };

                var detalles = new List<DetallePedido>();

                foreach (var d in nuevoPedidoDto.Detalles)
                {
                    var producto = await _productoService.obtenerProductoById(d.IdProducto);

                    if (producto != null)
                    {
                        var subtotal = (producto.PrecioProducto ?? 0) * d.Cantidad;

                        detalles.Add(new DetallePedido
                        {
                            IdProducto = d.IdProducto,
                            Cantidad = d.Cantidad,
                            Subtotal = subtotal 
                        });
                    }
                }

              
                nuevoPedido.Total = detalles.Sum(d => d.Subtotal);

                var pedidoCreado = await _pedidoRepository.CrearPedido(nuevoPedido, detalles);

                var pedidoDto = new PedidoDTO
                {
                    IdPedido = pedidoCreado.IdPedido,
                    IdEmpleado = pedidoCreado.IdEmpleado,
                    IdDireccionCliente = pedidoCreado.IdDireccionCliente,
                    Total = pedidoCreado.Total,
                    Detalles = pedidoCreado.DetallePedidos.Select(d => new DetallePedidoDTO
                    {
                        IdDetalle = d.IdDetalle,
                        IdPedido = d.IdPedido,
                        IdProducto = d.IdProducto,
                        Cantidad = d.Cantidad,
                        Subtotal = d.Subtotal
                    }).ToList()
                };

                return Ok(pedidoDto);
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                return BadRequest($"Error al crear el pedido: {ex.Message}. Inner exception: {innerException?.Message}");
            }
        }
    }
}
