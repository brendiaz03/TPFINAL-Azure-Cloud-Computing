using ReproductorDeMusica.Entidades.Entidades;

namespace ReproductorDeMusica.Web.Models
{
    public class CancionViewModel
    {
        public int IdCancion { get; set; }
        public string Titulo { get; set; }
        public string Artista { get; set; }
        public string Album { get; set; } = null!;
        public int Creador { get; set; }
        public virtual Usuario UsuarioCreador { get; set; } = null!;
        public string Duracion { get; set; }
        public virtual ICollection<CancionListaReproduccion> ListaCanciones { get; set; } = new List<CancionListaReproduccion>();
        public IFormFile Audio { get; set; }
        public IFormFile Imagen { get; set; }

        // Método para convertir el ViewModel a la entidad Cancion
        public static Cancion ToCancion(CancionViewModel model, string audioUrl, string imagenUrl, int idUsuario, string duracion)
        {
            return new Cancion
            {
                Creador = idUsuario,
                Titulo = model.Titulo,
                Artista = model.Artista,
                Album = model.Album,
                UrlPortada = imagenUrl,  // Asignar URL de la imagen
                RutaAudio = audioUrl,      // Asignar URL del archivo de audio
                Duracion = duracion
            };
        }
    }
}
