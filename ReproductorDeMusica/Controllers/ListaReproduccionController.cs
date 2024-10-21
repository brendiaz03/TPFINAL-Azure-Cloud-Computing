using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;
using ReproductorDeMusica.Logica.Interfaces;
using ReproductorDeMusica.Web.Models;

namespace ReproductorDeMusica.Web.Controllers
{
    public class ListaReproduccionController : Controller
    {
        private readonly IListaReproduccionService _reproduccionService;
        private readonly IBlobStorageService _blobStorageService;

        public ListaReproduccionController(IListaReproduccionService service, IBlobStorageService blobStorageService)
        {
            _reproduccionService = service;
            _blobStorageService = blobStorageService;
        }

        // Método sincrónico para mostrar la página de listas de reproducción
        public IActionResult Index(int usuarioId)
        {
            // Obtener las listas de reproducción del usuario de manera sincrónica
            var listasDeReproduccion = _reproduccionService.ObtenerListasDeReproduccionPorUsuario(usuarioId);

            // Pasarlas a la vista
            return View(listasDeReproduccion);
        }

        [HttpGet]
        public List<ListaReproduccion> GetAllListasReproduccion()
        {
            List<ListaReproduccion> listaReproduccions = _reproduccionService.GetListasReproduccions();
            return listaReproduccions;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarListaReproduccion([FromForm] ListaReproduccionViewModel listaReproduccionViewModel)
        {
            try
            {
                string urlImagen = await _blobStorageService.SubirArchivoAsync(listaReproduccionViewModel.Imagen, "imagenesListaReproduccion");

                // Convertir ViewModel a entidad ListaReproduccion
                ListaReproduccion listaReproduccion = ListaReproduccionViewModel.ToListaReproduccion(listaReproduccionViewModel, urlImagen);

                // Guardar lista en bdd
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
