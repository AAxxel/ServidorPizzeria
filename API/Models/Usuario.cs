using System;
using System.Collections.Generic;

namespace POSPizzeria.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public int? IdRol { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual Role? IdRolNavigation { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
