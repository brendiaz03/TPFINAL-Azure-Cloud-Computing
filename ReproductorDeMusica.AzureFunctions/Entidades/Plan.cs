using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.AzureFunctions.Entidades
{
    public partial class Plan
    {
        public Plan()
        {
            UsuarioPlans = new HashSet<UsuarioPlan>();
        }

        public int Id { get; set; }
        public string TipoPlan { get; set; }
        public decimal? Precio { get; set; }
        public int? Duracion { get; set; }

        public virtual ICollection<UsuarioPlan> UsuarioPlans { get; set; }
    }
}
