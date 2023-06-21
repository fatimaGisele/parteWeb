using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class ClienteMediodepago
{
    public int IdCliente { get; set; }

    public int IdmedioDePago { get; set; }

    public string? Tipo { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual MedioDePago IdmedioDePagoNavigation { get; set; } = null!;
}
