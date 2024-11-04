using Microsoft.EntityFrameworkCore;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Entidades.Repositories;
using ReproductorDeMusica.Entidades.Repositories.Interfaces;
using ReproductorDeMusica.Logica.Interfaces;


namespace ReproductorDeMusica.Logica
{
    public class CancionListaReproduccionService : ICancionListaReproduccionService
    {
        private readonly ICancionListaReproduccionRepository _cancionListaReproduccionRepository;

        public CancionListaReproduccionService(ICancionListaReproduccionRepository cancionlistaReproduccionRepository)
        {
            _cancionListaReproduccionRepository = cancionlistaReproduccionRepository;
        }

        public bool AgregarCancionListaReproduccion(int IdCancion, int IdLista)
        {
            try
            {
                CancionListaReproduccion cancionLista = new CancionListaReproduccion
                {
                    IdCancion = IdCancion,
                    IdListaReproduccion = IdLista
                };

                if(_cancionListaReproduccionRepository.CrearCancionListaReproduccion(cancionLista) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCancionDeLaLista(int idCancion, int idLista)
        {
            try
            {
                _cancionListaReproduccionRepository.EliminarCancionDeLaLista(idCancion, idLista);
            } catch (Exception ex) { 
                throw ex; 
            }
        }

        public void EliminarCancionDeTodasLasListas(int idCancion)
        {
            try
            {
                _cancionListaReproduccionRepository.EliminarCancionDeTodasLasListas(idCancion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
