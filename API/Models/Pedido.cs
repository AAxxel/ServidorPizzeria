using System;
using System.Collections.Generic;

namespace POSPizzeria.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int? IdEmpleado { get; set; }

    public int? IdDireccionCliente { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual DireccionCliente? IdDireccionClienteNavigation { get; set; }

    public virtual Usuario? IdEmpleadoNavigation { get; set; }
}
