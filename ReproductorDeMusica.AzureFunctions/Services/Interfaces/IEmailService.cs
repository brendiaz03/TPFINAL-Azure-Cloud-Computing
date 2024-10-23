using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions.Services.Interfaces
{
    public interface IEmailService
    {
        Task EnviarMail(string subject, string contenido, string toEmail);
    }
}
