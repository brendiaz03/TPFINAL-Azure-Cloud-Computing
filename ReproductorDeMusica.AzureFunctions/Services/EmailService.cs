using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions
{

    public interface IEmailService {
        Task EnviarMail(string subject, string contenido, string toEmail);
    }


    public class EmailService : IEmailService
    {
        private const string _emailCredencial = "alangta242@gmail.com";
        private const string _contrasenaCredencial = "rjmc cyke owyh dvng";
        private const string _host = "smtp.gmail.com";
        private const int _port = 587;
        private SmtpClient _smtpCliente;

        public EmailService()
        {

            _smtpCliente = new SmtpClient
            {
                Host = _host,
                Port = _port,
                Credentials = new NetworkCredential(_emailCredencial, _contrasenaCredencial),
                EnableSsl = true,
                UseDefaultCredentials = false
            };
        }

        //Llama a la azure function (demo)
        public async Task EnviarMail(string subject, string contenido, string toEmail)
        {
            try
            {

                MailMessage mensajeCorreo = new MailMessage
                {
                    From = new MailAddress(_emailCredencial),
                    Subject = subject,
                    Body = contenido,
                    IsBodyHtml = true,
                };

                //Destinatario
                mensajeCorreo.To.Add("alanaruquipa242@gmail.com"); // ejemplo del destinatario (lo puede cambiar)
                _smtpCliente.Send(mensajeCorreo);
                mensajeCorreo.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
