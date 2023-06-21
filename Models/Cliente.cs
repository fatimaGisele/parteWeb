using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public int Telefono { get; set; }

    public int Rol { get; set; }

    public virtual ICollection<ClienteCarrito> ClienteCarritos { get; set; } = new List<ClienteCarrito>();

    public virtual ICollection<ClienteMediodepago> ClienteMediodepagos { get; set; } = new List<ClienteMediodepago>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
