using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;
using ReproductorDeMusica.Models;
using ReproductorDeMusica.Web.Models;

namespace ReproductorDeMusica.Web.Controllers;

public class UsuarioController : Controller
{
    private readonly IUsuarioLogica _usuarioLogica;


    public UsuarioController(IUsuarioLogica usuarioLogica)
    {
        _usuarioLogica = usuarioLogica;
    } 

    [HttpGet]
    public IActionResult RegistrarUsuario()
    {
        var usuarioId = HttpContext.Session.GetString("UsuarioId");
        var nombreUsuario = HttpContext.Session.GetString("NombreUsuario");

        ViewBag.EstaLoggeado = usuarioId != null;
        ViewBag.NombreUsuario = nombreUsuario;

        return View(new UsuarioViewModel());
    }

    [HttpPost]
    public IActionResult RegistrarUsuario(UsuarioViewModel usuarioModel){
          
        if (!ModelState.IsValid){
                return View(usuarioModel);
            }
        try{
            _usuarioLogica.RegistrarUsuario(UsuarioViewModel.ToUsuario(usuarioModel));
        }   catch(UsuarioExistenteException e){
            ModelState.AddModelError(string.Empty, e.Message);
            return View(usuarioModel);
        }

        return RedirectToAction("Login");

    }

    [HttpGet]
    public IActionResult Login()
    {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            var nombreUsuario = HttpContext.Session.GetString("NombreUsuario");

           ViewBag.EstaLoggeado = usuarioId != null;
           ViewBag.NombreUsuario = nombreUsuario;

        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel loginModel)
    {
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
            HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
            return RedirectToAction("Index", "Home");
        }

    }
    public IActionResult Logout(LoginViewModel loginModel)
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }

}


