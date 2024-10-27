using ReproductorDeMusica.AzureFunctions.Entidades;
using ReproductorDeMusica.AzureFunctions.Repositories.Interfaces;
using ReproductorDeMusica.AzureFunctions.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions.Services
{
    public class UsuarioPlanService : IUsuarioPlanService
    {
        private readonly IUsuarioPlanRepository _usuarioPlanRepository;

        public UsuarioPlanService(IUsuarioPlanRepository usuarioPlanRepository)
        {
            _usuarioPlanRepository = usuarioPlanRepository;
        }

        public async Task<UsuarioPlan> ObtenerUsuarioPlanPorId(int id)
        {
            return _usuarioPlanRepository.GetUsuarioPlanById(id);
        }
    }
}
