﻿using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.Entidades.Entidades;

public partial class Plan
{
    public int Id { get; set; }

    public string? TipoPlan { get; set; }

    public decimal? Precio { get; set; }

    public int? Duracion { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}