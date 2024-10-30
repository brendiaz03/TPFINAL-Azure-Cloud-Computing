using ReproductorDeMusica.AzureFunctions.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions.Repositories.Interfaces
{
    public interface IEmailRegistroRepository
    {
        EmailRegistro SaveEmailRegistro(EmailRegistro emailRegistro);

        List<EmailRegistro> GetEmailRegistroNoEnviados();

        void UpdateEsEnviadoATrue(EmailRegistro emailRegistro);
    }
}
