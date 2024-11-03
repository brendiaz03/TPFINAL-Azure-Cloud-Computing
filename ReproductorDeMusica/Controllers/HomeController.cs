using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Models;
using ReproductorDeMusica.Web.Models;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Identity.Web;
using ReproductorDeMusica.Logica.Interfaces;

namespace ReproductorDeMusica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioPlanService _usuarioPlanService;



        public HomeController(ILogger<HomeController> logger, IUsuarioService usuarioService, IUsuarioPlanService usuarioPlanService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
            _usuarioPlanService = usuarioPlanService;
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
            ViewBag.DeshabilitarSidebar = false;


            if (usuarioId != null)
            {
                Usuario buscado = _usuarioService.BuscarUsuarioPorID((int)usuarioId);
                ViewBag.NombreUsuario = buscado.NombreUsuario;
                ViewBag.ImagenUsuario = buscado.ImagenUsuario;
                if (_usuarioPlanService.ObtenerUsuarioConPlan((int)usuarioId)!=null){
                    var usuario = _usuarioPlanService.ObtenerUsuarioConPlan((int)usuarioId);
                    var usuarioPlan = usuario.UsuarioPlans.LastOrDefault();
                    if (usuarioPlan != null && usuarioPlan.IdPlanNavigation.TipoPlan == "GRATUITO")
                    {
                        ViewBag.DeshabilitarSidebar = true;
                    }
                }
            }

            return View(new HomeViewModel());
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet("api/playlists")]
        public IActionResult GetPlaylists()
        {
            var playlists = new List<HomeViewModel>
    {
        new HomeViewModel { Name = "Shakira"},
      
        // Agrega más artistas o playlists aquí
    };

            return Json(playlists);
        }

    }
}
