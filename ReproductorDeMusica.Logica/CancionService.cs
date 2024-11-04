using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Entidades.Repositories.Interfaces;
using ReproductorDeMusica.Logica.Interfaces;

namespace ReproductorDeMusica.Logica
{
    public class CancionService : ICancionService
    {
        private readonly ICancionRepository _cancionRepository;
        private readonly ICancionListaReproduccionRepository _cancionListaReproduccionRepository;

        public CancionService(ICancionRepository cancionRepository, ICancionListaReproduccionRepository cancionListaReproduccionRepository)
        {
            _cancionRepository = cancionRepository;
            _cancionListaReproduccionRepository = cancionListaReproduccionRepository;
        }

        public async Task<Cancion> CrearCancion(Cancion cancion)
        {
            try
            {
                return _cancionRepository.CrearCancion(cancion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cancion EditarCancion(Cancion cancion)
        {
            try
            {
                return _cancionRepository.EditarCancion(cancion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCancion(int idCancion)
        {
            try
            {
                _cancionListaReproduccionRepository.EliminarCancionDeTodasLasListas(idCancion);
                _cancionRepository.EliminarCancion(idCancion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Cancion> GetCancionById(int id)
        {
            try
            {
                return  _cancionRepository.GetCancionById(id);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Cancion> GetCancions()
        {
            try
            {
                return _cancionRepository.GetCancions();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Cancion>> BuscarCancionesPorNombre(string nombre)
        {
            try
            {
                return _cancionRepository.BuscarCancionesPorNombre(nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Cancion>> GetCancionesPorCreador(int idUsuario)
        {
            try
            {
                return _cancionRepository.BuscarCancionesPorCreador(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
