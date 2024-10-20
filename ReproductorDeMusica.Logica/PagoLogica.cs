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

            Pago pago = new Pago();
            pago.IdUsuarioNavigation = usuario;
            pago.IdPlanNavigation = planAPagar;

            _context.Pagos.Add(pago);
            _context.SaveChanges();
        }
    }
}
