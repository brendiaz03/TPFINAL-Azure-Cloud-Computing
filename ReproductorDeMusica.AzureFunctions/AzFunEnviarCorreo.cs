using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;
namespace ReproductorDeMusica.AzureFunctions
{
    public static class AzFunEnviarCorreo
    {
        [FunctionName("AzFunEnviarCorreo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("AzEnviarCorreo Trigger http");
            try
            {
                string to= req.Query["tp"];
                string content = req.Query["content"];
                string subject = req.Query["subject"];

                IEmailService service = new EmailService();
                await service.EnviarMail(subject, content, to);
                log.LogInformation("Correo enviado");
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestObjectResult("Correo no enviado");
            }
            return new OkObjectResult("Correo enviado con exito");
        }

    }
}
