using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.Entidades.Entidades;

public partial class Plan
{
    public int Id { get; set; }

    public string TipoPlan { get; set; } = null!;

    public decimal? Precio { get; set; }

    public int? Duracion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
