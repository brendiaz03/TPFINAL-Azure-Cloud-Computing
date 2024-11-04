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

    public string? Duracion { get; set; }

    public string? RutaAudio { get; set; }

    public string? UrlPortada { get; set; }

    public virtual ICollection<CancionListaReproduccion> CancionListaReproduccions { get; set; } = new List<CancionListaReproduccion>();

    public virtual Usuario? CreadorNavigation { get; set; }
}
