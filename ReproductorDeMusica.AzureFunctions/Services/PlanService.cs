using ReproductorDeMusica.AzureFunctions.Entidades;
using ReproductorDeMusica.AzureFunctions.Repositories;
using ReproductorDeMusica.AzureFunctions.Repositories.Interfaces;
using ReproductorDeMusica.AzureFunctions.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Plan> ObtenerPlanPorId(int id)
        {
            return _planRepository.GetPlanById(id);
        }
    }
}
