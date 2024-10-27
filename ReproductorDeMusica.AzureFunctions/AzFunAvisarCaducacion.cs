using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ReproductorDeMusica.AzureFunctions
{
    public static class AzFunAvisarCaducacion
    {

        //PRUEBA:  se ejecuta cada 5 minutos (test)
        [FunctionName("AzFunAvisarCaducacion")]
        public static void Run([TimerTrigger("* */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
