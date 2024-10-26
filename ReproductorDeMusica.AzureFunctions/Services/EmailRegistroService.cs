using ReproductorDeMusica.AzureFunctions.Entidades;
using ReproductorDeMusica.AzureFunctions.Repositories.Interfaces;
using ReproductorDeMusica.AzureFunctions.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions.Services
{
    public class EmailRegistroService : IEmailRegistroService
    {
        private readonly IEmailRegistroRepository _emailRegistroRepository;

        public EmailRegistroService(IEmailRegistroRepository emailRegistroRepository)
        {
            _emailRegistroRepository = emailRegistroRepository;
        }

        public void ActualizarEmailEsEnviado(EmailRegistro emailRegistro)
        {
            _emailRegistroRepository.UpdateEsEnviadoATrue(emailRegistro);
        }

        public void GuardarEmailRegistro(EmailRegistro emailRegistro)
        {
            _emailRegistroRepository.SaveEmailRegistro(emailRegistro);
        }

        public List<EmailRegistro> ObtenerLosEmailsNoEnviados()
        {
            return _emailRegistroRepository.GetEmailRegistroNoEnviados();
        }
    }
}
