using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Indumentarium
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string Detalle { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Talle { get; set; }

    public int Stock { get; set; }

    public string Img { get; set; } = null!;

    public int CategoriaId { get; set; }

    public int GeneroId { get; set; }

    public virtual Categorium Categoria { get; set; } = null!;

    public virtual Genero Genero { get; set; } = null!;
}
