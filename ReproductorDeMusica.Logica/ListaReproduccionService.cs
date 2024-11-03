using Microsoft.EntityFrameworkCore;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Entidades.Repositories;
using ReproductorDeMusica.Entidades.Repositories.Interfaces;
using ReproductorDeMusica.Logica.Interfaces;


namespace ReproductorDeMusica.Logica
{
    public class ListaReproduccionService : IListaReproduccionService
    {
        private readonly IListaReproduccionRepository _listaReproduccionRepository;

        public ListaReproduccionService(IListaReproduccionRepository listaReproduccionRepository)
        {
            _listaReproduccionRepository = listaReproduccionRepository;
        }

        public bool AgregarListaReproduccion(ListaReproduccion listaReproduccion)
        {
            try
            {
                ListaReproduccion listaRepetida = _listaReproduccionRepository.ObtenerListaPorNombre(listaReproduccion.Nombre);

                if(listaReproduccion == null)
                {
                    _listaReproduccionRepository.AgregarListaReproduccion(listaReproduccion);
                    return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ListaReproduccion EditarListaReproduccion(ListaReproduccion listaReproduccion)
        {
            try
            {
                return _listaReproduccionRepository.EditarListaReproduccion(listaReproduccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarListaReproduccion(int idListaReproduccion)
        {
            try
            {
                _listaReproduccionRepository.EliminarListaReproduccion(idListaReproduccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ListaReproduccion>> GetListasReproduccions()
        {
            try
            {
                return _listaReproduccionRepository.GetListaReproduccions();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ListaReproduccion> ObtenerListasDeReproduccionPorUsuario(int usuarioId)
        {
            return _listaReproduccionRepository.ObtenerListasPorUsuario(usuarioId);
        }

        public ListaReproduccion ObtenerListasDeReproduccionPorId(int id)
        {
            return _listaReproduccionRepository.ObtenerListaPorId(id);
        }
    }
}
