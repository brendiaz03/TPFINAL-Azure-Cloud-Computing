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

        public void SaveEmailRegistro(EmailRegistro emailRegistro)
        {
            _context.EmailRegistros.Add(emailRegistro);
            _context.SaveChanges();
        }
    }
}
