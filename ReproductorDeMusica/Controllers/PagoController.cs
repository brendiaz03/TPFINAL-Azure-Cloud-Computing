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


        public PagoController(IPagoService pagoLogica, ICorreoService correoLogica)
        {
            _pagoService = pagoLogica;
            _correoService = correoLogica;
        }

        [HttpGet]
        public IActionResult Index(int planId)
        {
            ViewBag.planId = planId;   
            return View();
        }

        [HttpPost]
        public IActionResult RealizarPago()
        {
            int idUsuario = (int) HttpContext.Session.GetInt32("UsuarioId"); // Prueba
            int idPlan = int.Parse(Request.Form["idPlan"]);
            UsuarioPlan usuarioPlan = _pagoService.RealizarPago(idPlan, idUsuario);
            _correoService.EnviarCorreoPago(usuarioPlan.Id);
            return View("PagoRealizado");
        }

        [HttpGet]
        public IActionResult PagoRealizado()
        {
            return View();
        }


        /*Pruebas para la verificacion de datos*/
        [HttpGet]
        public IActionResult Prueba()
        {
            List<Plan> plan = _pagoService.ObtenerTodosLosPlanes();
            return View(plan);
        }

        [HttpGet]
        public IActionResult Prueba2() {
            List<UsuarioPlan> usuariosPlanes = _pagoService.ObtenerPlanesPorUsuarioId(3);
            return View(usuariosPlanes);
        }

        [HttpGet]
        public JsonResult ObtenerUltimoPlanPorUsuarioActual()
        {
            var idUsuario = HttpContext.Session.GetInt32("UsuarioId");

            if (!idUsuario.HasValue)
            {
                return Json(new { error = "Usuario no autenticado o sesión expirada." });
            }

            try
            {
                var usuarioPlan = _pagoService.GetUltimoPlanUsuario(idUsuario.Value);

                if (usuarioPlan == null)
                {
                    return Json(new { planNoDisponible = true });
                }

                var usuarioPlanDTO = new UsuarioPlanDTO
                {
                    Id = usuarioPlan.Id,
                    TipoPlan = usuarioPlan.TipoPlan,
                    Precio = usuarioPlan.Precio,
                    FechaExpiracion = usuarioPlan.FechaExpiracion
                };

                return Json(usuarioPlanDTO);
            }
            catch (Exception)
            {
                return Json(new { error = "Ocurrió un error al obtener el plan." });
            }
        }



    }
}
