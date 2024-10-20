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

        public CancionService(ICancionRepository cancionRepository)
        {
            _cancionRepository = cancionRepository;
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
                _cancionRepository.EliminarCancion(idCancion);
            }
            catch (Exception ex)
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
    }
}
