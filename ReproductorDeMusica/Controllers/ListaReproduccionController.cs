using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica.Interfaces;

namespace ReproductorDeMusica.Web.Controllers
{
    public class ListaReproduccionController : Controller
    {
        private readonly IListaReproduccionService _reproduccionService;

        public ListaReproduccionController(IListaReproduccionService service)
        {
            _reproduccionService = service;
        }

        public IActionResult Index()
        {
            List<ListaReproduccion> listaReproduccions = _reproduccionService.GetListaReproduccions();
            return View(listaReproduccions);
        }

        [HttpPost]
        public IActionResult AgregarListReproduccion(ListaReproduccion listaReproduccion)
        {
            try
            {
                _reproduccionService.AgregarListaReproduccion(listaReproduccion);
                return Ok(listaReproduccion);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }

        [HttpPost]
        public IActionResult EditarListaReproduccion(ListaReproduccion listaReproduccion)
        {
            try
            {
                ListaReproduccion editada = _reproduccionService.EditarListaReproduccion(listaReproduccion);
                return Ok(editada);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }

        [HttpPost]
        public IActionResult EliminarListaReproduccion(int idLista)
        {
            try
            {
                _reproduccionService.EliminarListaReproduccion(idLista);
                return Ok(idLista);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }

    }
}
}
