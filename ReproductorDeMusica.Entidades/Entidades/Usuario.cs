﻿using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.Entidades.Entidades;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Email { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Contrasenia { get; set; }

    public string? ImagenUsuario { get; set; }

    public virtual ICollection<Cancion> Cancions { get; set; } = new List<Cancion>();

    public virtual ICollection<ListaReproduccion> ListaReproduccions { get; set; } = new List<ListaReproduccion>();

    public virtual ICollection<UsuarioPlan> UsuarioPlans { get; set; } = new List<UsuarioPlan>();
}
