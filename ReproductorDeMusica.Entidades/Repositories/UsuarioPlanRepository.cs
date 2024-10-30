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
            pago.FechaExpiracion = DateTime.Now.Date.AddDays((double)planAPagar.Duracion);

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
    }
}
