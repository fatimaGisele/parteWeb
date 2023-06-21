using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Categorium
{
    public int Id { get; set; }

    public string CategoriaNombre { get; set; } = null!;

    public virtual ICollection<Indumentarium> Indumentaria { get; set; } = new List<Indumentarium>();
}
