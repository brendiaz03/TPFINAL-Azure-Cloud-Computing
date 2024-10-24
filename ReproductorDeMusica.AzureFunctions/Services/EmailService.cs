using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ReproductorDeMusica.AzureFunctions.Services.Interfaces;
using ReproductorDeMusica.AzureFunctions.Enumeradores;
namespace ReproductorDeMusica.AzureFunctions.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IBlobStorageService _blobStorageService;
        private SmtpClient _smtpClient;

        public EmailService(SmtpClient smtpClient,IConfiguration configuration, IBlobStorageService blobStorageService)
        {
            _smtpClient = smtpClient;
            _configuration = configuration;
            _blobStorageService = blobStorageService;
        }

        public async Task EnviarMail(string toEmail, string usuario, string plan, TipoMensaje tipoMensaje)
        {
            try
            {

                string body = await GenerarTemplatePorTipoMensaje(tipoMensaje);
                string subject = await GenerarSubjectPorTipoMensaje(tipoMensaje);

                body = body.Replace("{{Usuario}}", usuario);
                body = body.Replace("{{Plan}}", plan);


                MailMessage mensajeCorreo = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:EmailCredential"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                //Destinatario
                mensajeCorreo.To.Add(toEmail);
                _smtpClient.Send(mensajeCorreo);
                mensajeCorreo.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<string> GenerarTemplatePorTipoMensaje(TipoMensaje tipoMensaje)
        {
            switch (tipoMensaje)
            {
                case TipoMensaje.MENSAJE_PAGO:
                    return await _blobStorageService.CargarTemplateDesdeBlobAsync("templates-test", "RippieEmailTemplateBienvenidaPago/RippieEmailTemplate.html");
                case TipoMensaje.MENSAJE_RECORDATORIO:
                    return await _blobStorageService.CargarTemplateDesdeBlobAsync("templates-test", "RippieEmailTemplateBienvenidaPago/RippieEmailTemplate.html");
            }
            return "";
        }
        private async Task<string> GenerarSubjectPorTipoMensaje(TipoMensaje tipoMensaje)
        {
            switch (tipoMensaje)
            {
                case TipoMensaje.MENSAJE_PAGO:
                    return "Bienvenido a Rippie";
                case TipoMensaje.MENSAJE_RECORDATORIO:
                    return "Expiracion";

            }
            return "";
        }

        //---------------Metodo para testeo
        public async Task EnviarMailTest()
        {
            try
            {

                string body = await _blobStorageService.CargarTemplateDesdeBlobAsync("templates-test", "RippieEmailTemplateBienvenidaPago/RippieEmailTemplate.html");
                body = body.Replace("{{Usuario}}", "Juan");
                body = body.Replace("{{Plan}}", "Premium");


                MailMessage mensajeCorreo = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:EmailCredential"]),
                    Subject = "Test",
                    Body = body,
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
