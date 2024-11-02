using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;
using ReproductorDeMusica.Logica.Interfaces;
using ReproductorDeMusica.Models;
using ReproductorDeMusica.Web.Models;
using System.Runtime.CompilerServices;

namespace ReproductorDeMusica.Web.Controllers;

public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioService;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IUsuarioPlanService _usuarioPlanService;



    public UsuarioController(IUsuarioService usuarioService, IBlobStorageService blobStorageService, IUsuarioPlanService usuarioPlanService)
    {
        _usuarioService = usuarioService;
        _blobStorageService = blobStorageService;
        _usuarioPlanService = usuarioPlanService;
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
            string imagenUrl = await _blobStorageService.SubirArchivoAsync(usuarioModel.ImagenUsuario, "usuarios-imagenes");

            // Convertir el UsuarioViewModel a la entidad Usuario
            Usuario usuario = UsuarioViewModel.ToUsuario(usuarioModel, imagenUrl);

            _usuarioService.RegistrarUsuario(UsuarioViewModel.ToUsuario(usuarioModel, imagenUrl));

        }
        catch (UsuarioExistenteException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return View(usuarioModel);
        }
        catch (Exception e)
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

        var usuario = _usuarioService.ValidarUsuario(loginModel.NombreUsuario, loginModel.Contrasenia);

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
            var usuario = _usuarioPlanService.ObtenerUsuarioConPlan((int)usuarioId);

            if (usuario != null)
            {
                var usuarioPlan = usuario.UsuarioPlans.FirstOrDefault();
                ViewBag.NombreUsuario = usuario.NombreUsuario;
                ViewBag.ImagenUsuario = usuario.ImagenUsuario;
                var CuentaViewModel = new CuentaViewModel
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    NombreUsuario = usuario.NombreUsuario,
                    FechaPago = usuarioPlan?.FechaPago,
                    TipoPlan = usuarioPlan?.IdPlanNavigation?.TipoPlan
                };

                return View(CuentaViewModel);
            }
        }

        return View(new CuentaViewModel());
    }

}


