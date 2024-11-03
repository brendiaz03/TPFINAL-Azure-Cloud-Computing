using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Controllers;
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
        private readonly IUsuarioService _usuarioLogica;

        public ListaReproduccionController(IListaReproduccionService service, IBlobStorageService blobStorageService, IUsuarioService usuarioLogica)
        {
            _usuarioLogica = usuarioLogica;
            _reproduccionService = service;
            _blobStorageService = blobStorageService;
            _usuarioLogica = usuarioLogica;
        }

        public IActionResult Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");


            ViewBag.EstaLoggeado = usuarioId != null;
            ViewBag.EsFormulario = false;

            if (usuarioId != null)
            {
                Usuario buscado = _usuarioLogica.BuscarUsuarioPorID((int)usuarioId);
                ViewBag.NombreUsuario = buscado.NombreUsuario;
                ViewBag.ImagenUsuario = buscado.ImagenUsuario;

                var listasDeReproduccion = _reproduccionService.ObtenerListasDeReproduccionPorUsuario((int)usuarioId);

                return View(listasDeReproduccion);
            }
            return View();
        }
        public IActionResult VerListaReproduccion(int id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            ViewBag.EstaLoggeado = usuarioId != null;
            ViewBag.EsFormulario = false;

            if (usuarioId != null)
            {
                Usuario buscado = _usuarioLogica.BuscarUsuarioPorID((int)usuarioId);
                ViewBag.NombreUsuario = buscado.NombreUsuario;

                var listaReproduccion = _reproduccionService.ObtenerListasDeReproduccionPorId(id); // Cargar la lista por ID
                var canciones = listaReproduccion.CancionListaReproduccions.Select(cl => cl.IdCancionNavigation).ToList();

                var viewModel = new ListaReproduccionCancionViewModel
                {
                    ListaReproduccion = listaReproduccion,
                    Canciones = canciones
                };
                return View(viewModel);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<List<ListaReproduccion>> GetAllListasReproduccion()
        {
            List<ListaReproduccion> listaReproduccions = await _reproduccionService.GetListasReproduccions();
            return listaReproduccions;
        }

        #region Post
        [HttpPost]
        public async Task<IActionResult> AgregarListaReproduccion([FromForm] ListaReproduccionViewModel listaReproduccionViewModel)
        {
            try
            {
                var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

                ViewBag.EstaLoggeado = usuarioId != null;
                ViewBag.EsFormulario = false;

                if (usuarioId != null)
                {
                    Usuario buscado = _usuarioLogica.BuscarUsuarioPorID((int)usuarioId);
                    ViewBag.NombreUsuario = buscado.NombreUsuario;
                }
                else
                {
                    return NotFound();
                }
                string urlImagen = await _blobStorageService.SubirArchivoAsync(listaReproduccionViewModel.Imagen, "imagenes-listareproduccion");

                    ListaReproduccion listaReproduccion = ListaReproduccionViewModel.ToListaReproduccion((int)usuarioId, listaReproduccionViewModel, urlImagen);

                bool agregado = _reproduccionService.AgregarListaReproduccion(listaReproduccion);

                return View();
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return RedirectToAction("Index", "ListaReproduccion");
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
        #endregion
    
    }

}
