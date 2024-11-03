using Microsoft.Identity.Client;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Entidades.Repositories;
using ReproductorDeMusica.Entidades.Repositories.Interfaces;
using ReproductorDeMusica.Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Cancion CrearCancion(Cancion cancion)
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

        public Cancion GetCancionById(int id)
        {
            try
            {
                return _cancionRepository.GetCancionById(id);
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

        public IEnumerable<Cancion> BuscarCancionesPorNombre(string nombre)
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
    }
}
