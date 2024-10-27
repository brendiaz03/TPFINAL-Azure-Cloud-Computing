using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.AzureFunctions.Entidades
{
    public partial class EmailRegistro
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool? EsEnviado { get; set; }
        public DateTime? FechaCreada { get; set; }
        public DateTime? FechaProxima { get; set; }
        public int? IdUsuarioPlan { get; set; }

        public virtual UsuarioPlan IdUsuarioPlanNavigation { get; set; }
    }
}
