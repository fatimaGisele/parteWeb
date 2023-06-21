using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class MedioDePago
{
    public int Id { get; set; }

    public int Numero { get; set; }

    public virtual ICollection<ClienteMediodepago> ClienteMediodepagos { get; set; } = new List<ClienteMediodepago>();
}
