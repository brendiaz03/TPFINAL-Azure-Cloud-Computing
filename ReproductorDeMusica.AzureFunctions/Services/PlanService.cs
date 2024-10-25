using ReproductorDeMusica.AzureFunctions.Entidades;
using ReproductorDeMusica.AzureFunctions.Repositories;
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
        private readonly PlanRepository _planRepository;

        public PlanService(PlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Plan> ObtenerPlanPorId(int id)
        {
            return _planRepository.GetPlanById(id);
        }
    }
}
