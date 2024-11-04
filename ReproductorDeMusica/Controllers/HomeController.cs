using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Models;
using ReproductorDeMusica.Web.Models;
using ReproductorDeMusica.Entidades.Entidades;
using System.Diagnostics;
using ReproductorDeMusica.Logica.Interfaces;

namespace ReproductorDeMusica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioPlanService _usuarioPlanService;
        private readonly ICancionService _cancionService;



        public HomeController(ILogger<HomeController> logger, IUsuarioService usuarioService, IUsuarioPlanService usuarioPlanService, ICancionService cancionService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
            _usuarioPlanService = usuarioPlanService;
            _cancionService = cancionService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return View();
            }

            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            ViewBag.EstaLoggeado = usuarioId != null;
            ViewBag.EsFormulario = false;


            if (usuarioId != null)
            {
                Usuario buscado = _usuarioService.BuscarUsuarioPorID((int)usuarioId);
                ViewBag.NombreUsuario = buscado.NombreUsuario;
                ViewBag.ImagenUsuario = buscado.ImagenUsuario;
            }

            List<Cancion> canciones = _cancionService.GetCancions();
            ViewBag.Canciones = canciones;
           
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    //    [HttpGet("api/playlists")]
    //    public IActionResult GetPlaylists()
    //    {
    //        var playlists = new List<HomeViewModel>
    //{
    //    new HomeViewModel { Name = "Shakira"},
      
    //    // Agrega m�s artistas o playlists aqu�
    //};

    //        return Json(playlists);
    //    }

    }
}
