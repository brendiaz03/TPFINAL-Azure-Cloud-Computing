using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;

namespace ReproductorDeMusica.Web.Controllers
{
    public class PagoController : Controller
    {
        private IPagoLogica _pagoLogica;

        public PagoController(IPagoLogica pagoLogica)
        {
            _pagoLogica = pagoLogica;
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
            return View();
        }

        public IActionResult Prueba()
        {
            List<Plan> plan = _pagoLogica.GetListPlan();
            return View(plan);
        }
    }
}
