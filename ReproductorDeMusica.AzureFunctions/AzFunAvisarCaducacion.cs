using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReproductorDeMusica.AzureFunctions.Services.Interfaces;
using System.Collections.Generic;
using ReproductorDeMusica.AzureFunctions.Services;
using ReproductorDeMusica.AzureFunctions.Entidades;
using ReproductorDeMusica.AzureFunctions.Enumeradores;

namespace ReproductorDeMusica.AzureFunctions
{
    public class AzFunAvisarCaducacion
    {
        private readonly IEmailRegistroService _emailRegistroService;
        private readonly IEmailService _emailService;

        public AzFunAvisarCaducacion(IEmailRegistroService emailRegistroService, IEmailService emailService) { 
            _emailRegistroService = emailRegistroService;
            _emailService = emailService;
        }

        //PRUEBA:  se ejecuta cada  minuto (test)
        [FunctionName("AzFunAvisarCaducacion")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Triger de aviso de caducacion se ejecuto en el tiempo: {DateTime.Now}");
            try
            {
                List<EmailRegistro> emailNoEnviados = _emailRegistroService.ObtenerLosEmailsNoEnviados();

                foreach (EmailRegistro email in emailNoEnviados) {
                    if (DateTime.Now >= email.FechaProxima)
                    {   
                        await _emailService.EnviarMail(new Usuario { Nombre = email.Usuario, Email = email.Email },
                           new Plan { TipoPlan = email.TipoPlan },
                           TipoMensaje.MENSAJE_CADUCACION);

                        _emailRegistroService.ActualizarEmailEsEnviado(email);
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
        }
    }
}
