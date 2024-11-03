using Microsoft.EntityFrameworkCore;
using ReproductorDeMusica.AzureFunctions.Entidades;
using ReproductorDeMusica.AzureFunctions.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions.Repositories
{
    public class UsuarioPlanRepository : IUsuarioPlanRepository
    {
        private readonly tpweb3_azureContext _context;

        public UsuarioPlanRepository(tpweb3_azureContext context)
        {
            _context = context;
        }

        public UsuarioPlan GetUsuarioPlanById(int id)
        {
            return _context.UsuarioPlans.Include(up=>up.IdPlanNavigation)
                .Include(up=>up.IdUsuarioNavigation).First(up => up.Id == id);

        }

        public List<UsuarioPlan> GetAllUsuarioPlanesExpirados() {
            return _context.UsuarioPlans.Include(up => up.IdPlanNavigation)
                .Include(up => up.IdUsuarioNavigation).Where(up=>up.FechaExpiracion == DateTime.Now.AddDays(-1).Date).ToList();
        }

        public void DeleteUsuarioPlan(UsuarioPlan usuarioPlan)
        {
            _context.UsuarioPlans.Remove(usuarioPlan);
            _context.SaveChanges();
        }
    }
}
