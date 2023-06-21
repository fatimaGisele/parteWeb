using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Genero
{
    public int Idgenero { get; set; }

    public string Genero1 { get; set; } = null!;

    public virtual ICollection<Indumentarium> Indumentaria { get; set; } = new List<Indumentarium>();
}
