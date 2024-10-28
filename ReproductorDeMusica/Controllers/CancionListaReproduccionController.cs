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
        private readonly IUsuarioLogica _usuarioLogica;

        public CancionListaReproduccionController(ICancionListaReproduccionService service)
        {
            _cancionListaReproduccionService = service;
        }

        [HttpGet]  // Asegúrate de que este atributo sea GET
        public IActionResult AgregarCancionListaReproduccion(int idCancion)
        {
            // Obtener el ID de la lista de reproducción desde la sesión
            var idListaReproduccion = HttpContext.Session.GetInt32("IdLista");

            // Verificar si el ID de la lista de reproducción es válido
            if (idListaReproduccion <= 0)
            {
                return Json(new { success = false, message = "ID de lista de reproducción no válido." });
            }

            // Lógica para agregar la canción a la lista de reproducción
            var resultado = _cancionListaReproduccionService.AgregarCancionListaReproduccion(idCancion, (int)idListaReproduccion);

            return RedirectToAction("VerListaReproduccion", "ListaReproduccion",new { id = idListaReproduccion });

        }

    }

}
