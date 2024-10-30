using ReproductorDeMusica.AzureFunctions.Entidades;
using ReproductorDeMusica.AzureFunctions.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly tpweb3_azureContext _context;

        public UsuarioRepository(tpweb3_azureContext context)
        {
            _context = context; 
        }

        public Usuario GetUsuarioPorId(int id)
        {
            return _context.Usuarios.First(u => u.Id == id);  
        }
    }
}
