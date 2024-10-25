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
using Azure.Storage.Blobs;
using ReproductorDeMusica.AzureFunctions.Repositories.Interfaces;
using ReproductorDeMusica.AzureFunctions.Repositories;
using ReproductorDeMusica.AzureFunctions.Entidades;



[assembly: FunctionsStartup(typeof(ReproductorDeMusica.AzureFunctions.Startup))]
namespace ReproductorDeMusica.AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<tpweb3_azureContext>();

            //Servicios
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IUsuarioService, UsuarioService>();
            builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();
            builder.Services.AddSingleton<IPlanService, PlanService>();
            builder.Services.AddSingleton<IEmailRegistroService, EmailRegistroService>();

            //Repositorios
            builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddSingleton<IPlanRepository, PlanRepository>();
            builder.Services.AddSingleton<IEmailRegistroRepository,EmailRegistroRepository>();

            //Configuracion smtp client
            builder.Services.AddSingleton(smtp =>
                new SmtpClient()
                {
                    Host = builder.GetContext().Configuration["EmailSettings:EmailHost"],
                    Port = int.Parse(builder.GetContext().Configuration["EmailSettings:EmailPort"]),
                    Credentials = new NetworkCredential(builder.GetContext().Configuration["EmailSettings:EmailCredential"], builder.GetContext().Configuration["EmailSettings:EmailPassword"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false
                });

            //Configuracion BlobServiceClient
            builder.Services.AddSingleton(sp =>
                new BlobServiceClient(builder.GetContext().Configuration["BlobStorageConnection"]));
        }
    }
}
