using System;
using System.Collections.Generic;

namespace POSPizzeria.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public decimal? PrecioProducto { get; set; }

    public decimal? Impuesto { get; set; }

    public int? Stock { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdProveedor { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual CategoriaProducto? IdCategoriaNavigation { get; set; }

    public virtual Proveedore? IdProveedorNavigation { get; set; }
}
