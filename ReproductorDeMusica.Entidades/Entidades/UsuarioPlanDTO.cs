using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Entidades.Entidades
{
    public class UsuarioPlanDTO
    {
        public int Id { get; set; }
        public string TipoPlan { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaExpiracion { get; set; }

    }
}
