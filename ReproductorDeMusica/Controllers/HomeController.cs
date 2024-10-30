using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Models;
using ReproductorDeMusica.Web.Models;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Identity.Web;

namespace ReproductorDeMusica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioLogica _usuarioLogica;


        public HomeController(ILogger<HomeController> logger, IUsuarioLogica usuarioLogica)
        {
            _logger = logger;
            _usuarioLogica = usuarioLogica;

        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            ViewBag.EstaLoggeado = usuarioId != null;
            ViewBag.EsFormulario = false;


            if(usuarioId != null)
            {
                Usuario buscado = _usuarioLogica.buscarUsuarioPorID((int)usuarioId);
                ViewBag.NombreUsuario = buscado.NombreUsuario;
            }

            return View(new HomeViewModel());
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
