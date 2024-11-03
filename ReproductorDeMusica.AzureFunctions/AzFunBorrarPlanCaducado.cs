using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using ReproductorDeMusica.AzureFunctions.Entidades;
using ReproductorDeMusica.AzureFunctions.Services;
using ReproductorDeMusica.AzureFunctions.Services.Interfaces;

namespace ReproductorDeMusica.AzureFunctions
{
    public class AzFunBorrarPlanCaducado
    {
        private readonly IUsuarioPlanService _usuarioPlanService;
        private readonly IEmailRegistroService _emailRegistroService;

        public AzFunBorrarPlanCaducado(IUsuarioPlanService usuarioPlanService, IEmailRegistroService emailRegistroService)
        {
            _usuarioPlanService = usuarioPlanService;
            _emailRegistroService = emailRegistroService;
        }

        [FunctionName("AzFunBorrarPlanCaducado")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Triger de borrado de planes caducados, se ejecuto en el tiempo: {DateTime.Now}");
            try
            {
                List<UsuarioPlan> usuarioPlanesExpirados = await _usuarioPlanService.ObtenerUsuariosPlanesExpirados();

                foreach(UsuarioPlan usuarioPlan in usuarioPlanesExpirados)
                {
                    await _emailRegistroService.EliminarEmailRegistroPorUsuarioPlanId(usuarioPlan.Id);
                    await _usuarioPlanService.EliminarUsuarioPlan(usuarioPlan);
                    log.LogInformation("Eliminado plan caducado");
                }
            }
            catch (Exception ex) {
                log.LogError(ex.Message);
            }

        }
    }
}
