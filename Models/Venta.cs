using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Venta
{
    public int Idventas { get; set; }

    public DateTime Fecha { get; set; }

    public double Total { get; set; }

    public int ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
