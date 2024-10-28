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
    public class EmailRegistroRepository : IEmailRegistroRepository
    {
        private readonly tpweb3_azureContext _context;

        public EmailRegistroRepository(tpweb3_azureContext context) { 
            _context = context;
        }

        public List<EmailRegistro> GetEmailRegistroNoEnviados()
        {
            return _context.EmailRegistros
                .Include(e=>e.IdUsuarioPlanNavigation)
                    .ThenInclude(up=>up.IdPlanNavigation)
                .Include(e=>e.IdUsuarioPlanNavigation)
                    .ThenInclude(up=>up.IdUsuarioNavigation)
                .Where(em => em.EsEnviado == false).ToList();
        }

        public EmailRegistro SaveEmailRegistro(EmailRegistro emailRegistro)
        {
            _context.EmailRegistros.Add(emailRegistro);
            _context.SaveChanges();
            return emailRegistro;
        }

        public void UpdateEsEnviadoATrue(EmailRegistro emailRegistro)
        {
            emailRegistro.EsEnviado  = true;
            _context.SaveChanges();
        }
    }
}
