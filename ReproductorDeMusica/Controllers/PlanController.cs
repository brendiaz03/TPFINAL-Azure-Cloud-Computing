using Microsoft.AspNetCore.Mvc;

namespace ReproductorDeMusica.Web.Controllers
{
    public class PlanController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
