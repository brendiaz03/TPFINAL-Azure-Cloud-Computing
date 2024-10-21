using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;

namespace ReproductorDeMusica.Web.Controllers
{
    public class PagoController : Controller
    {
        private readonly IPagoLogica _pagoLogica;
        private readonly ICorreoLogica _correoLogica;


        public PagoController(IPagoLogica pagoLogica, ICorreoLogica correoLogica)
        {
            _pagoLogica = pagoLogica;
            _correoLogica = correoLogica;
        }

        public IActionResult Index(int planId)
        {
            ViewBag.planId = planId;   
            return View();
        }

        [HttpPost]
        public IActionResult RealizarPago()
        {
            int idUsuario = 3; // Prueba
            int idPlan = int.Parse(Request.Form["idPlan"]);
            _pagoLogica.RealizarPago(idPlan, idUsuario);
            _correoLogica.EnviarCorreoPago("acavauncorreo@gmail.com");
            return View("PagoRealizado");
        }

        public IActionResult PagoRealizado()
        {
            return View();
        }

        public IActionResult Prueba()
        {
            List<Plan> plan = _pagoLogica.GetListPlan();
            return View(plan);
        }

        public IActionResult Prueba2() {
            List<UsuarioPlan> usuariosPlanes = _pagoLogica.GetUsuariosPlansPorUsuario(3);
            return View(usuariosPlanes);
        }
    }
}
