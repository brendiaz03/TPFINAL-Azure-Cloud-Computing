using ReproductorDeMusica.AzureFunctions.Entidades;
using ReproductorDeMusica.AzureFunctions.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly tpweb3_azureContext _context;

        public PlanRepository(tpweb3_azureContext context)
        {
            _context = context;
        }
        
        public Plan GetPlanById(int id)
        {
            return _context.Plans.First(p=>p.Id == id);  
        }
    }
}
