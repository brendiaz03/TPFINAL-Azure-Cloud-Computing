using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Models;
using ReproductorDeMusica.Web.Models;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;
using System.Diagnostics;
using ReproductorDeMusica.Logica.Interfaces;

namespace ReproductorDeMusica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioLogica _usuarioLogica;
        private readonly ICancionService _cancionService;

        public HomeController(ILogger<HomeController> logger, IUsuarioLogica usuarioLogica, ICancionService cancionService)
        {
            _logger = logger;
            _usuarioLogica = usuarioLogica;
            _cancionService = cancionService;
        }

        public IActionResult Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");


            ViewBag.EstaLoggeado = usuarioId != null;
            ViewBag.EsFormulario = false;

            if (usuarioId != null)
            {
                Usuario buscado = _usuarioLogica.buscarUsuarioPorID((int)usuarioId);
                ViewBag.NombreUsuario = buscado.NombreUsuario;
            }

            List<Cancion> canciones = _cancionService.GetCancions();
            ViewBag.Canciones = canciones;
            var viewModel = new CancionesViewModel
            {
                Canciones = canciones ?? new List<Cancion>()
            };

            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
