using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;
using ReproductorDeMusica.Logica.Interfaces;
using ReproductorDeMusica.Models;
using ReproductorDeMusica.Web.Models;
using System.Runtime.CompilerServices;

namespace ReproductorDeMusica.Web.Controllers;

public class UsuarioController : Controller
{
    private readonly IUsuarioLogica _usuarioLogica;
    private readonly IBlobStorageService _blobStorageService;


    public UsuarioController(IUsuarioLogica usuarioLogica, IBlobStorageService blobStorageService)
    {
        _usuarioLogica = usuarioLogica;
        _blobStorageService = blobStorageService;
    }

    [HttpGet]
    public IActionResult RegistrarUsuario()
    {
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        ViewBag.EstaLoggeado = usuarioId != null;
        ViewBag.EsFormulario = true;


        return View(new UsuarioViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarUsuario(UsuarioViewModel usuarioModel)
    {

        ViewBag.EsFormulario = true;

        if (!ModelState.IsValid)
        {
            return View(usuarioModel);
        }

        try
        {

            // Subir los archivos a Azure Blob Storage
            string imagenUrl = await _blobStorageService.SubirArchivoAsync(usuarioModel.ImagenUsuario, "fotoDePerfil-usuarios");

            // Convertir el UsuarioViewModel a la entidad Usuario
            Usuario usuario = UsuarioViewModel.ToUsuario(usuarioModel, imagenUrl);

            _usuarioLogica.RegistrarUsuario(UsuarioViewModel.ToUsuario(usuarioModel, imagenUrl));

        }
        catch (UsuarioExistenteException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return View(usuarioModel);
        }

        return RedirectToAction("Login");

    }

    [HttpGet]
    public IActionResult Login()
    {
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        ViewBag.EstaLoggeado = usuarioId != null;
        ViewBag.EsFormulario = true;

        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel loginModel)
    {
        ViewBag.EsFormulario = true;

        if (!ModelState.IsValid)
        {
            return View(loginModel);
        }

        var usuario = _usuarioLogica.ValidarUsuario(loginModel.NombreUsuario, loginModel.Contrasenia);

        if (usuario == null)
        {
            ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos");
            return View(loginModel);
        }
        else
        {
            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            return RedirectToAction("Index", "Home");
        }

    }
    public async Task<IActionResult> Logout(LoginViewModel loginModel)
    {
        HttpContext.Session.Clear();
        
        if (User.Identity.IsAuthenticated)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Cuenta()
    {

        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        ViewBag.EstaLoggeado = usuarioId != null;

        if (usuarioId != null)
        {
            Usuario buscado = _usuarioLogica.buscarUsuarioPorID((int)usuarioId);
            var usuarioViewModel = UsuarioViewModel.FromUsuario(buscado);
            ViewBag.NombreUsuario = buscado.NombreUsuario;
            return View(usuarioViewModel);

        }
        else
        {
            return View(new UsuarioViewModel());
        }


    }
}


