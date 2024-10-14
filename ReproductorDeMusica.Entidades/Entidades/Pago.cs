using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.Entidades.Entidades;

public partial class Pago
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdPlan { get; set; }

    public virtual Plan? IdPlanNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
