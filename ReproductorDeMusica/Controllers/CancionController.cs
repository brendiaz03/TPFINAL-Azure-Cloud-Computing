using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;
using ReproductorDeMusica.Logica.Interfaces;
using ReproductorDeMusica.Web.Models;

namespace ReproductorDeMusica.Web.Controllers
{
    public class CancionController : Controller
    {

        private readonly ICancionService _cancionService;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IUsuarioService _usuarioLogica;

        public CancionController(ICancionService cancionService, IBlobStorageService blobStorageService, IUsuarioService usuarioLogica)
        {
            _cancionService = cancionService;
            _blobStorageService = blobStorageService;
            _usuarioLogica = usuarioLogica;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GetAllCancionesDisponibles(int idListaReproduccion)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");


            ViewBag.EstaLoggeado = usuarioId != null;
            ViewBag.EsFormulario = false;

            if (usuarioId != null)
            {
                Usuario buscado = _usuarioLogica.BuscarUsuarioPorID((int)usuarioId);
                ViewBag.NombreUsuario = buscado.NombreUsuario;
            }
            HttpContext.Session.SetInt32("IdLista", idListaReproduccion);
            List<Cancion> canciones = _cancionService.GetCancions();
            return View("ListaCancionesDisponibles", canciones); 
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
        public async Task<IActionResult> EditarCancion(Cancion cancion)
        {
            try
            {
                Cancion editada = await _cancionService.CrearCancion(cancion);
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
        public async Task<IActionResult> ReproducirCancion(int id)
        {
            try
            {
                var cancion = await _cancionService.GetCancionById(id);
                return Ok(cancion);
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string titulo)
        {
            var resultados = await _cancionService.BuscarCancionesPorNombre(titulo);
            return PartialView("_ResultadoBusquedaPartial", resultados);
        }
    }
}
