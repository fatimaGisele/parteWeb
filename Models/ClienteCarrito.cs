using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class ClienteCarrito
{
    public int IdCliente { get; set; }

    public int IdCarrito { get; set; }

    public int Cantidad { get; set; }

    public virtual Carrito IdCarritoNavigation { get; set; } = null!;

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
