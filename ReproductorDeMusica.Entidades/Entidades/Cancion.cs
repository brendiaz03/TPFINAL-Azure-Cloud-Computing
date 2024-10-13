using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.Entidades.Entidades;

public partial class Cancion
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Artista { get; set; } = null!;

    public string Album { get; set; } = null!;

    public int Creador { get; set; }

    public int Duracion { get; set; }

    public virtual Usuario CreadorNavigation { get; set; } = null!;

    public virtual ICollection<ListaCancione> ListaCanciones { get; set; } = new List<ListaCancione>();
}
