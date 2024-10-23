using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ReproductorDeMusica.AzureFunctions.Services.Interfaces;
using ReproductorDeMusica.AzureFunctions.Services;



[assembly: FunctionsStartup(typeof(ReproductorDeMusica.AzureFunctions.Startup))]
namespace ReproductorDeMusica.AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton(smtp =>
                new SmtpClient()
                {
                    Host = builder.GetContext().Configuration["EmailSettings:EmailHost"],
                    Port = int.Parse(builder.GetContext().Configuration["EmailSettings:EmailPort"]),
                    Credentials = new NetworkCredential(builder.GetContext().Configuration["EmailSettings:EmailCredential"], builder.GetContext().Configuration["EmailSettings:EmailPassword"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false
                });
        }
    }
}
