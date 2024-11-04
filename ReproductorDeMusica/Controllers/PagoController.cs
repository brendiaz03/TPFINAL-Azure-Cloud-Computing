using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;
using ReproductorDeMusica.Logica.Interfaces;

namespace ReproductorDeMusica.Web.Controllers
{
    public class PagoController : Controller
    {
        private readonly IPagoService _pagoService;
        private readonly ICorreoService _correoService;
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioPlanService _usuarioPlanService;


        public PagoController(IPagoService pagoLogica, ICorreoService correoLogica, IUsuarioService usuarioService, IUsuarioPlanService usuarioPlanService)
        {
            _pagoService = pagoLogica;
            _correoService = correoLogica;
            _usuarioService = usuarioService;
            _usuarioPlanService = usuarioPlanService;
        }

        [HttpGet]
        public IActionResult Index()//int planId
        {
            //ViewBag.planId = planId;
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuario = _usuarioService.BuscarUsuarioPorID((int)usuarioId);

            ViewBag.ImagenUsuario = usuario.ImagenUsuario;
            ViewBag.DataUsuario = usuario;
            ViewBag.EstaLoggeado = usuarioId != null;
            ViewBag.DeshabilitarSidebar = false;


            if (_usuarioPlanService.ObtenerUsuarioConPlan((int)usuarioId) != null)
            {
                var usuarioP = _usuarioPlanService.ObtenerUsuarioConPlan((int)usuarioId);
                var usuarioPlan = usuarioP.UsuarioPlans.LastOrDefault();
                if (usuarioPlan != null && usuarioPlan.IdPlanNavigation.TipoPlan == "GRATUITO")
                {
                    ViewBag.DeshabilitarSidebar = true;
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult RealizarPago()
        {
            int idUsuario = (int) HttpContext.Session.GetInt32("UsuarioId"); 
            UsuarioPlan usuarioPlan = _pagoService.RealizarPagoAPremium(idUsuario);
            _correoService.EnviarCorreoPago(usuarioPlan.Id);
            HttpContext.Session.SetInt32("Plan", 2);
            return View("PagoRealizado");
        }

        [HttpGet]
        public IActionResult PagoRealizado()
        {
            return View();
        }


        /*Pruebas para la verificacion de datos*/
        //[HttpGet]
        //public IActionResult Prueba()
        //{
        //    List<Plan> plan = _pagoService.ObtenerTodosLosPlanes();
        //    return View(plan);
        //}

        //[HttpGet]
        //public IActionResult Prueba2() {
        //    List<UsuarioPlan> usuariosPlanes = _pagoService.ObtenerPlanesPorUsuarioId(3);
        //    return View(usuariosPlanes);
        //}

        //[HttpGet]
        //public JsonResult ObtenerUltimoPlanPorUsuarioActual()
        //{
        //    var idUsuario = HttpContext.Session.GetInt32("UsuarioId");

        //    if (!idUsuario.HasValue)
        //    {
        //        return Json(new { error = "Usuario no autenticado o sesión expirada." });
        //    }

        //    try
        //    {
        //        var usuarioPlan = _pagoService.GetUltimoPlanUsuario(idUsuario.Value);

        //        if (usuarioPlan == null)
        //        {
        //            return Json(new { planNoDisponible = true });
        //        }

        //        var usuarioPlanDTO = new UsuarioPlanDTO
        //        {
        //            Id = usuarioPlan.Id,
        //            TipoPlan = usuarioPlan.TipoPlan,
        //            Precio = usuarioPlan.Precio,
        //            FechaExpiracion = usuarioPlan.FechaExpiracion
        //        };

        //        return Json(usuarioPlanDTO);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { error = "Ocurrió un error al obtener el plan." });
        //    }
        //}

    }
}
