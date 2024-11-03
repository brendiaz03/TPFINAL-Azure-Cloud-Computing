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
using ReproductorDeMusica.AzureFunctions.Entidades;
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

        public async Task EnviarMail(EmailRegistro email, TipoMensaje tipoMensaje)
        {
            try
            {

                string body = await GenerarTemplatePorTipoMensaje(tipoMensaje);
                string subject = await GenerarSubjectPorTipoMensaje(tipoMensaje);

                body = await ReplaceTemplatePorTipoMensaje(body, email, tipoMensaje);
                
                MailMessage mensajeCorreo = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:EmailCredential"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                //Destinatario
                mensajeCorreo.To.Add(email.IdUsuarioPlanNavigation.IdUsuarioNavigation.Email);
                _smtpClient.Send(mensajeCorreo);
                mensajeCorreo.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<string> ReplaceTemplatePorTipoMensaje(string body, EmailRegistro email, TipoMensaje tipoMensaje)
        {
           // if (tipoMensaje == TipoMensaje.MENSAJE_PAGO)
            //{
                body = body.Replace("{{Usuario}}", email.IdUsuarioPlanNavigation.IdUsuarioNavigation.Nombre + " " + email.IdUsuarioPlanNavigation.IdUsuarioNavigation.Apellido);
                body = body.Replace("{{Plan}}", email.IdUsuarioPlanNavigation.IdPlanNavigation.TipoPlan);
                body = body.Replace("{{Fecha}}", email.IdUsuarioPlanNavigation.FechaExpiracion.Value.Date.ToString());

           // }
            /*
            if (tipoMensaje == TipoMensaje.MENSAJE_CADUCACION)
            {
                body = body.Replace("{{Usuario}}", email.IdUsuarioPlanNavigation.IdUsuarioNavigation.Nombre);
                body = body.Replace("{{Plan}}", email.IdUsuarioPlanNavigation.IdPlanNavigation.TipoPlan);
                body = body.Replace("{{Fecha}}", email.IdUsuarioPlanNavigation.FechaExpiracion.ToString());  
            }*/

            return body;
        }

        private async Task<string> GenerarTemplatePorTipoMensaje(TipoMensaje tipoMensaje)
        {
            switch (tipoMensaje)
            {
                case TipoMensaje.MENSAJE_PAGO:
                    return await _blobStorageService.CargarTemplateDesdeBlobAsync("templates-test", "RippieEmailTemplateBienvenidaPago/RippieEmailTemplate.html");
                case TipoMensaje.MENSAJE_CADUCACION:
                    return await _blobStorageService.CargarTemplateDesdeBlobAsync("templates-test", "RippieEmailTemplateCaducacion/RippieEmailTemplateCaducacion.html");
            }
            return "";
        }
        private async Task<string> GenerarSubjectPorTipoMensaje(TipoMensaje tipoMensaje)
        {
            switch (tipoMensaje)
            {
                case TipoMensaje.MENSAJE_PAGO:
                    return "Bienvenido a Rippie";
                case TipoMensaje.MENSAJE_CADUCACION:
                    return "Recordatorio de expiración";

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
