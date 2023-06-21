using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class IndumentariaHasVenta
{
    public int IndumentariaId { get; set; }

    public int VentasId { get; set; }

    public int Cantidad { get; set; }
}
