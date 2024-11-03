using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Controllers;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica.Interfaces;
using ReproductorDeMusica.Logica;

namespace ReproductorDeMusica.Web.Controllers
{
    public class PlanController : Controller
    {

        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioPlanService _usuarioPlanService;



        public PlanController(IUsuarioService usuarioService, IUsuarioPlanService usuarioPlanService)
        {
            _usuarioService = usuarioService;
            _usuarioPlanService = usuarioPlanService;
        }
        [HttpGet]
        public IActionResult Index()
        {

            if (HttpContext.Session.GetInt32("UsuarioId") == null)
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
                if (_usuarioPlanService.ObtenerUsuarioConPlan((int)usuarioId) != null)
                {
                    var usuario = _usuarioPlanService.ObtenerUsuarioConPlan((int)usuarioId);
                    var usuarioPlan = usuario.UsuarioPlans.LastOrDefault();
                    if (usuarioPlan != null && usuarioPlan.IdPlanNavigation.TipoPlan == "GRATUITO")
                    {
                        ViewBag.DeshabilitarSidebar = true;
                    }
                }
            }
            return View();
        }
    }
}
