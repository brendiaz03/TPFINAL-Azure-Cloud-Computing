using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica.Interfaces;

namespace ReproductorDeMusica.Web.Controllers
{
    public class CancionController : Controller
    {
        private readonly ICancionService _cancionService;

        public IActionResult Index()
        {
            List<Cancion> canciones = _cancionService.GetCancions();
            return View(canciones);
        }

        public CancionController(ICancionService cancionService)
        {
            _cancionService = cancionService;
        }

        [HttpPost]
        public IActionResult AgregarCancion(Cancion cancion)
        {
            try
            {
                _cancionService.CrearCancion(cancion);
                return Ok(cancion);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }

        [HttpPost]
        public IActionResult EditarCancion(Cancion cancion)
        {
            try
            {
                Cancion editada = _cancionService.CrearCancion(cancion);
                return Ok(editada);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }

        [HttpPost]
        public IActionResult EliminarCancion(int idCancion)
        {
            try
            {
                _cancionService.EliminarCancion(idCancion);
                return Ok(idCancion);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }

    }
}
