using ReproductorDeMusica.Entidades.Entidades;

namespace ReproductorDeMusica.Web.Models
{
    public class ListaReproduccionViewModel
    {
        public int Id { get; set; }
        public int IdListaReproduccion { get; set; }
        public string Nombre { get; set; }
        public IFormFile? Imagen { get; set; }
        public DateOnly? FechaCreacion { get; set; }

        // Método para convertir el ViewModel a la entidad ListaReproduccion
        public static ListaReproduccion ToListaReproduccion(int id, ListaReproduccionViewModel model, string imagenUrl)
        {
            return new ListaReproduccion
            {
                IdUsuario = id,
                Nombre = model.Nombre,
                UrlPortada = imagenUrl,
                FechaCreacion = DateOnly.FromDateTime(DateTime.Now),
            };
        }
    }

}
