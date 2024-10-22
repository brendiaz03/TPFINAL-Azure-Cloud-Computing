using Microsoft.EntityFrameworkCore;
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
        void RealizarPago(int idPlan, int idUsuario);
        List<UsuarioPlan> GetUsuariosPlansPorUsuario(int idUsuario);
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

        public void RealizarPago(int idPlan, int idUsuario)
        {
            Plan planAPagar = _context.Plans.Find(idPlan);
            Usuario usuario = _context.Usuarios.Find(idUsuario);

            UsuarioPlan pago = new UsuarioPlan();
            pago.IdUsuarioNavigation = usuario;
            pago.IdPlanNavigation = planAPagar;

            _context.UsuarioPlans.Add(pago);
            _context.SaveChanges();
        }


        public List<UsuarioPlan> GetUsuariosPlansPorUsuario(int idUsuario)
        {

            //Eager loading
            /*
            return _context.Usuarios
                .Include(u => u.UsuarioPlans) // incluyo la lista
                    .ThenInclude(up=> up.IdPlanNavigation)    //incluyo de la lista los planes
                .First(u => u.Id == idUsuario).UsuarioPlans.ToList();
             */

            //Lazy Loading implicitamente 
            return _context.Usuarios
               .First(u => u.Id == idUsuario).UsuarioPlans.ToList();
        }
    }
}
