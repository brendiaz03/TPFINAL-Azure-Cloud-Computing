using ReproductorDeMusica.AzureFunctions.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        public Usuario GetUsuarioPorId(int id);
    }
}
