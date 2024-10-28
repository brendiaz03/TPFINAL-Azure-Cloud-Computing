using ReproductorDeMusica.Entidades.Entidades;

namespace ReproductorDeMusica.Web.Models
{
    public class ListaReproduccionCancionViewModel
    {
        public ListaReproduccion ListaReproduccion { get; set; } // Datos de la lista de reproducción
        public IEnumerable<Cancion> Canciones { get; set; } // Lista de canciones asociadas
    }

}
