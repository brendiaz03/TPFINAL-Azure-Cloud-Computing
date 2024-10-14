using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.Entidades.Entidades;

public partial class Cancion
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Artista { get; set; }

    public string? Album { get; set; }

    public int? Creador { get; set; }

    public int? Duracion { get; set; }

    public virtual Usuario? CreadorNavigation { get; set; }

    public virtual ICollection<ListaCancione> ListaCanciones { get; set; } = new List<ListaCancione>();

    public string UrlPortada { get; set; }
}
