using ReproductorDeMusica.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Logica
{
    public interface IPagoLogica {
        List<Plan> GetListPlan();
    }

    public class PagoLogica : IPagoLogica
    {
        Tpweb3AzureContext _context;

        public PagoLogica(Tpweb3AzureContext context) { 
            _context = context;
        }

        public List<Plan> GetListPlan()
        {
            return _context.Plans.ToList();
        }
    }
}
