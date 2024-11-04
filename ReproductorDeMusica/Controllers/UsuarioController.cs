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

            _usuarioService.RegistrarUsuario(UsuarioViewModel.ToUsuario(usuarioModel, null));

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
        ViewBag.MostrarBotonPagar = false;
        ViewBag.MostrarPremium = false;
        ViewBag.DeshabilitarSidebar = false;


        if (usuarioId != null)
        {
            var usuario = _usuarioPlanService.ObtenerUsuarioConPlan((int)usuarioId);

            if (usuario != null)
            {
                var usuarioPlan = usuario.UsuarioPlans.LastOrDefault();
                ViewBag.NombreUsuario = usuario.NombreUsuario;
                ViewBag.ImagenUsuario = usuario.ImagenUsuario;

                if (usuarioPlan?.FechaPago.HasValue == true)
                {
                    DateTime fechaPago = usuarioPlan.FechaPago.Value;
                    DateTime fechaFinalizacionPremium = usuarioPlan.FechaExpiracion;
                    int diasTotales = (fechaFinalizacionPremium - fechaPago).Days;
                    int diasRestantes = (fechaFinalizacionPremium - DateTime.Now).Days;

                    ViewBag.DiasRestantes = diasRestantes;
                    ViewBag.DiasTotales = diasTotales;
                    ViewBag.FechaFinalizacionPremium = fechaFinalizacionPremium.ToString("D", new System.Globalization.CultureInfo("es-ES"));
                }

                var CuentaViewModel = new CuentaViewModel
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    NombreUsuario = usuario.NombreUsuario,
                    FechaPago = usuarioPlan.FechaPago,
                    TipoPlan = usuarioPlan.IdPlanNavigation?.TipoPlan,
                    FechaFinalizacionPremium = ViewBag.FechaFinalizacionPremium
                };

                if (usuarioPlan != null && usuarioPlan.IdPlanNavigation.TipoPlan == "GRATUITO")
                {
                    ViewBag.MostrarBotonPagar = true;
                    ViewBag.DeshabilitarSidebar = true;
                }
                else
                {
                    ViewBag.MostrarPremium = true;
                }
                return View(CuentaViewModel);
            }
        }

        return View(new CuentaViewModel());
    }


    [HttpPost]
    public async Task<IActionResult> UpdateProfilePhotoAsync(IFormFile photo)
    {
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        Usuario usuario = _usuarioService.BuscarUsuarioPorID(usuarioId.Value);

        if (photo == null || photo.Length == 0)
        {
            return Json(new { success = false, message = "No se ha subido ninguna imagen." });
        }

        try
        {
            string urlImagen = await _blobStorageService.SubirArchivoAsync(photo, "usuarios-imagenes");
            usuario.ImagenUsuario = urlImagen;
            _usuarioService.ActualizarInfoUsuario(usuario);

        } catch (Exception ex)
        {
            return Json(new { success = false, message = "No hemos podido modificar la imágen" });
        }

        return Json(usuarioId);

    }


}


