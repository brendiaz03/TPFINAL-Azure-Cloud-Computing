using ReproductorDeMusica.Entidades.Entidades;

namespace ReproductorDeMusica.Web.Models
{
    public class ListaReproduccionViewModel
    {
        public int Id { get; set; }
        public int IdListaReproduccion { get; set; }
        public string Nombre { get; set; }
        public IFormFile? Imagen { get; set; }

        // Método para convertir el ViewModel a la entidad ListaReproduccion
        public static ListaReproduccion ToListaReproduccion(ListaReproduccionViewModel model, string imagenUrl)
        {
            return new ListaReproduccion
            {
                Nombre = model.Nombre,
                UrlPortada = imagenUrl
            };
        }
    }

}
