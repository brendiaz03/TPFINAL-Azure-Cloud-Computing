using Microsoft.EntityFrameworkCore;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Entidades.Repositories.Interfaces;
using ReproductorDeMusica.Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Logica
{
    public class PagoService : IPagoService
    {
        private readonly IUsuarioPlanRepository _usuarioPlanRepository;

        public PagoService(IUsuarioPlanRepository usuarioPlanRepository) {
            _usuarioPlanRepository = usuarioPlanRepository;
        }

        public List<Plan> ObtenerTodosLosPlanes()
        {
            return _usuarioPlanRepository.GetListPlan();
        }

        public UsuarioPlan RealizarPago(int idPlan, int idUsuario)
        {
            return _usuarioPlanRepository.AgregarNuevoUsuarioPlan(idPlan, idUsuario);
        }

        public List<UsuarioPlan> ObtenerPlanesPorUsuarioId(int idUsuario)
        {
            return _usuarioPlanRepository.GetUsuariosPlansPorUsuario(idUsuario);
        }
    }
}
