using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Controllers;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;
using ReproductorDeMusica.Logica.Interfaces;
using ReproductorDeMusica.Web.Models;

namespace ReproductorDeMusica.Web.Controllers
{
    public class CancionListaReproduccionController : Controller
    {
        private readonly ICancionListaReproduccionService _cancionListaReproduccionService;

        public CancionListaReproduccionController(ICancionListaReproduccionService service)
        {
            _cancionListaReproduccionService = service;
        }

        [HttpGet]
        public IActionResult AgregarCancionListaReproduccion(int idCancion)
        {

            try
            {
                var idListaReproduccion = HttpContext.Session.GetInt32("IdLista");

                if (idListaReproduccion <= 0)
                {
                    return Json(new { success = false, message = "ID de lista de reproducción no válido." });
                }


                var resultado = _cancionListaReproduccionService.AgregarCancionListaReproduccion(idCancion, (int)idListaReproduccion);

                return Ok(idListaReproduccion);

            } catch (Exception ex)
            {
                return Problem(ex.ToString());
            }

        }

    }

}
