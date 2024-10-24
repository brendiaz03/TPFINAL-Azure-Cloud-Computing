using Microsoft.AspNetCore.Mvc;

namespace ReproductorDeMusica.Web.Controllers
{
    public class PlanController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
