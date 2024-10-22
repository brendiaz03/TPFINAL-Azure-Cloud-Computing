using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Models;
using ReproductorDeMusica.Web.Models;
using System.Diagnostics;

namespace ReproductorDeMusica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            var nombreUsuario = HttpContext.Session.GetString("NombreUsuario");

            ViewBag.EstaLoggeado = usuarioId != null;
            ViewBag.NombreUsuario = nombreUsuario;

            return View(new HomeViewModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
