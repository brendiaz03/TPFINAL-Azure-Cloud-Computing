using Microsoft.EntityFrameworkCore;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Entidades.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Entidades.Repositories
{
    public class UsuarioPlanRepository : IUsuarioPlanRepository
    {
        private readonly Tpweb3AzureContext _context;

        public UsuarioPlanRepository(Tpweb3AzureContext context)
        {
            _context = context;
        }

        public List<Plan> GetListPlan()
        {
            return _context.Plans.ToList();
        }

        public UsuarioPlan AgregarNuevoUsuarioPlan(int idPlan, int idUsuario)
        {
            Plan planAPagar = _context.Plans.Find(idPlan);
            Usuario usuario = _context.Usuarios.Find(idUsuario);

            UsuarioPlan pago = new UsuarioPlan();
            pago.IdUsuarioNavigation = usuario;
            pago.IdPlanNavigation = planAPagar;
            pago.FechaPago = DateTime.Now.Date;


            if (planAPagar.Id == 1)
                pago.FechaExpiracion = DateTime.Now.Date.AddYears(100);

            if (planAPagar.Id == 2)
                pago.FechaExpiracion = DateTime.Now.Date.AddMonths((int)planAPagar.Duracion);

            _context.UsuarioPlans.Add(pago);
            _context.SaveChanges();

            return pago;
        }


        public List<UsuarioPlan> GetUsuariosPlansPorUsuario(int idUsuario)
        {
            //Lazy Loading implicitamente 
            return _context.Usuarios
               .First(u => u.Id == idUsuario).UsuarioPlans.ToList();
        }

        public Usuario ObtenerUsuarioConPlan(int usuarioId)
        {
            return _context.Usuarios
        .Include(u => u.UsuarioPlans
            .OrderByDescending(up => up.FechaPago)
            .Take(1))
        .ThenInclude(up => up.IdPlanNavigation)
        .FirstOrDefault(u => u.Id == usuarioId);
        }

        public UsuarioPlanDTO GetUltimoPlanUsuario(int idUsuario)
        {
            var plan = _context.UsuarioPlans
                .Where(u => u.IdUsuario == idUsuario)
                .OrderByDescending(u => u.Id)
                .Select(u => new UsuarioPlanDTO
                {
                    Id = u.Id,
                    TipoPlan = u.IdPlanNavigation.TipoPlan,
                    Precio = u.IdPlanNavigation.Precio,
                    FechaExpiracion = u.FechaExpiracion
                })
                .FirstOrDefault();

            return plan;
        }

        public UsuarioPlan RealizarPagoAPremium(int idUsuario)
        {
            Plan planAPagar = _context.Plans.Find(2);
            Usuario usuario = _context.Usuarios.Find(idUsuario);

            UsuarioPlan pago = new UsuarioPlan();
            pago.IdUsuarioNavigation = usuario;
            pago.IdPlanNavigation = planAPagar;
            pago.FechaPago = DateTime.Now.Date;
            pago.FechaExpiracion = DateTime.Now.Date.AddMonths((int)((double)planAPagar.Duracion));

            _context.UsuarioPlans.Add(pago);
            _context.SaveChanges();

            return pago;
        }
    }
}
