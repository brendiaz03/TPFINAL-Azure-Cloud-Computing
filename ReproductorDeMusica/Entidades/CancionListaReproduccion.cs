﻿using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.Web.Entidades;

public partial class CancionListaReproduccion
{
    public int Id { get; set; }

    public int? IdListaReproduccion { get; set; }

    public int? IdCancion { get; set; }

    public virtual Cancion? IdCancionNavigation { get; set; }

    public virtual ListaReproduccion? IdListaReproduccionNavigation { get; set; }
}
