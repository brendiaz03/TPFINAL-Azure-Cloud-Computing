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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Prueba()
        {
            List<Plan> plan = _pagoLogica.GetListPlan();
            return View(plan);
        }
    }
}
