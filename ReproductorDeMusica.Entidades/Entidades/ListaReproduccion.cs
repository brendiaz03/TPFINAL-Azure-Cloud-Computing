using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.Entidades.Entidades;

public partial class ListaReproduccion
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<ListaCancione> ListaCanciones { get; set; } = new List<ListaCancione>();

    public string UrlPortada { get; set; }
}
