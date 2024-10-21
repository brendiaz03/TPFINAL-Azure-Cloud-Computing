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
        return View();
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
        return View();
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
            HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario);
            var nombreUsuario = HttpContext.Session.GetString("NombreUsuario");
            return RedirectToAction("HomeLogged", "Home");
        }

    }

}
