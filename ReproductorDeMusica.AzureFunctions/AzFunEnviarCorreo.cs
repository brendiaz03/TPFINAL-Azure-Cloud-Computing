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
        private readonly IUsuarioService _usuarioService;
        private readonly IPlanService _planService;
        public AzFunEnviarCorreo(IEmailService emailService, IUsuarioService usuarioService, IPlanService planService) { 
            _emailService = emailService;
            _usuarioService = usuarioService;
            _planService = planService;
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
                int idUsuario = int.Parse(req.Query["idUsuario"]);
                int idPlan = int.Parse(req.Query["idPlan"]);

                //obtengo el usuario id en la bd 
                Usuario usuario = await _usuarioService.ObtenerUsuarioPorId(idUsuario);
                Plan plan = await _planService.ObtenerPlanPorId(idPlan);

                await _emailService.EnviarMail(usuario,TipoMensaje.MENSAJE_PAGO);

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
