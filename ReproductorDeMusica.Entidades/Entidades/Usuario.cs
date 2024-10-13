using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.Entidades.Entidades;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public int IdPlan { get; set; }

    public virtual ICollection<Cancion> Cancions { get; set; } = new List<Cancion>();

    public virtual Plan IdPlanNavigation { get; set; } = null!;

    public virtual ICollection<ListaReproduccion> ListaReproduccions { get; set; } = new List<ListaReproduccion>();
}
