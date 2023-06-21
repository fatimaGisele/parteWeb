using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Carrito
{
    public int IdCarrito { get; set; }

    public int IdIndumentaria { get; set; }

    public decimal MontoTotal { get; set; }

    public virtual ICollection<ClienteCarrito> ClienteCarritos { get; set; } = new List<ClienteCarrito>();
}
