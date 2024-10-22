using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica.Interfaces;
using ReproductorDeMusica.Web.Models;

namespace ReproductorDeMusica.Web.Controllers
{
    public class CancionController : Controller
    {

        private readonly ICancionService _cancionService;
        private readonly IBlobStorageService _blobStorageService;

        public CancionController(ICancionService cancionService, IBlobStorageService blobStorageService)
        {
            _cancionService = cancionService;
            _blobStorageService = blobStorageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<Cancion> GetAllCanciones()
        {
            List<Cancion> canciones = _cancionService.GetCancions();
            return canciones;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarCancion([FromForm] CancionViewModel model)
        {
            try
            {
                // Subir los archivos a Azure Blob Storage
                string urlAudio = await _blobStorageService.SubirArchivoAsync(model.Audio, "audios");
                string urlImagen = await _blobStorageService.SubirArchivoAsync(model.Imagen, "imagenes-canciones");

                // Convertir el ViewModel a la entidad Cancion
                Cancion cancion = CancionViewModel.ToCancion(model, urlAudio, urlImagen);

                // Guardar la canción en la base de datos
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

        [HttpGet]
        public IActionResult Buscar(string titulo)
        {
            var resultados = _cancionService.BuscarCancionesPorNombre(titulo);
            return PartialView("_ResultadoBusquedaPartial", resultados);
        }
    }
}
