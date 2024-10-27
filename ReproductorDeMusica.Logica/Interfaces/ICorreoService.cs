using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Logica.Interfaces
{
    public interface ICorreoService
    {
        Task EnviarCorreoPago(string toEmail);
    }
}
