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
    public class ListaReproduccionService:IListaReproduccionService
    {
        private readonly IListaReproduccionRepository _listaReproduccionRepository;

        public ListaReproduccionService(IListaReproduccionRepository listaReproduccionRepository)
        {
            _listaReproduccionRepository = listaReproduccionRepository;
        }

        public ListaReproduccion AgregarListaReproduccion(ListaReproduccion listaReproduccion)
        {
            try
            {
                return _listaReproduccionRepository.AgregarListaReproduccion(listaReproduccion);
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

        public List<ListaReproduccion> GetListaReproduccions()
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
    }
}
