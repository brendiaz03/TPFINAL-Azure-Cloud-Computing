using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ReproductorDeMusica.AzureFunctions.Services.Interfaces;
namespace ReproductorDeMusica.AzureFunctions.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private SmtpClient _smtpClient;

        public EmailService(SmtpClient smtpClient,IConfiguration configuration)
        {
            _smtpClient = smtpClient;
            _configuration = configuration; 
        }

        //Llama a la azure function (demo)
        public async Task EnviarMail(string subject, string contenido, string toEmail)
        {
            try
            {
                MailMessage mensajeCorreo = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:EmailCredential"]),
                    Subject = subject,
                    Body = contenido,
                    IsBodyHtml = true,
                };
                //Destinatario
                mensajeCorreo.To.Add("alanaruquipa242@gmail.com"); // ejemplo del destinatario (lo puede cambiar)
                _smtpClient.Send(mensajeCorreo);
                mensajeCorreo.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
