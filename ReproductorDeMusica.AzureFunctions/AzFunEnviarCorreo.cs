using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;
using ReproductorDeMusica.AzureFunctions.Services.Interfaces;
using ReproductorDeMusica.AzureFunctions.Services;
using ReproductorDeMusica.AzureFunctions.Entidades;
using ReproductorDeMusica.AzureFunctions.Enumeradores;


namespace ReproductorDeMusica.AzureFunctions
{
    public class AzFunEnviarCorreo
    {

        private readonly IEmailService _emailService;
        private readonly IUsuarioPlanService _usuarioPlanService;
        private readonly IEmailRegistroService _emailRegistroService;


        public AzFunEnviarCorreo(IEmailService emailService, IUsuarioPlanService usuarioPlanService, IEmailRegistroService emailRegistroService) { 
            _emailService = emailService;
            _usuarioPlanService = usuarioPlanService;
            _emailRegistroService = emailRegistroService;
        }

        [FunctionName("AzFunEnviarCorreo")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("AzEnviarCorreo Trigger http");
            try
            {
                //idUsuario y idPlan
                int idUsuarioPlan = int.Parse(req.Query["id"]);

                //obtengo el usuario id en la bd 
                UsuarioPlan usuarioPlan = await _usuarioPlanService.ObtenerUsuarioPlanPorId(idUsuarioPlan);

                //Registro el emailLog
                EmailRegistro emailRegistro = new EmailRegistro()
                {
                    Email = usuarioPlan.IdUsuarioNavigation.Email,
                    EsEnviado = false,
                    FechaCreada = DateTime.Now.Date,
                    FechaProxima = usuarioPlan.FechaExpiracion,
                    IdUsuarioPlanNavigation = usuarioPlan
                };

                emailRegistro = await _emailRegistroService.GuardarEmailRegistro(emailRegistro);

                await _emailService.EnviarMail(emailRegistro,TipoMensaje.MENSAJE_PAGO);

                //Metodo de test
                //await _emailService.EnviarMailTest();
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
