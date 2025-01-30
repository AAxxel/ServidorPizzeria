using System;
using System.Collections.Generic;

namespace POSPizzeria.Models;

public partial class DireccionCliente
{
    public int IdDireccion { get; set; }

    public int? IdCliente { get; set; }
    public string? Direccion { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
