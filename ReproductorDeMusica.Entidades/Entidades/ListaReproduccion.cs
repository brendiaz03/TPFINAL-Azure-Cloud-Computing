using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.Entidades.Entidades;

public partial class ListaReproduccion
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public string? UrlPortada { get; set; }
    public virtual ICollection<CancionListaReproduccion> CancionListaReproduccions { get; set; } = new List<CancionListaReproduccion>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
