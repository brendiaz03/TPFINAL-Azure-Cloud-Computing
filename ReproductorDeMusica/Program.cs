using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Entidades.Repositories.Interfaces;
using ReproductorDeMusica.Entidades.Repositories;
using ReproductorDeMusica.Logica;
using ReproductorDeMusica.Logica.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;



var builder = WebApplication.CreateBuilder(args);

// Configurar los servicios de autenticaci�n
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(); 

// Add Contexto
builder.Services.AddSingleton<Tpweb3AzureContext>();

// Configurar BlobServiceClient
builder.Services.AddSingleton(sp =>
    new BlobServiceClient(builder.Configuration.GetConnectionString("BlobStorageConnection")));

// Repositorios
builder.Services.AddSingleton<ICancionRepository, CancionRepository>();
builder.Services.AddSingleton<IListaReproduccionRepository, ListaReproduccionRepository>();
builder.Services.AddSingleton<IUsuarioPlanRepository, UsuarioPlanRepository>();
builder.Services.AddSingleton<ICancionListaReproduccionRepository, CancionListaReproduccionRepository>();

// Servicios
builder.Services.AddSingleton<IUsuarioLogica, UsuarioLogica>();
builder.Services.AddSingleton<IPagoService, PagoService>();
builder.Services.AddSingleton<ICorreoService, CorreoService>();
builder.Services.AddSingleton<ICancionService, CancionService>();
builder.Services.AddSingleton<IListaReproduccionService, ListaReproduccionService>();
builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();
builder.Services.AddSingleton<ICancionListaReproduccionService, CancionListaReproduccionService>();

// Add HttpClient
builder.Services.AddSingleton<HttpClient>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
