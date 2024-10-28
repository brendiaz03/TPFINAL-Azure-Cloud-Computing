using ReproductorDeMusica.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Entidades.Repositories.Interfaces
{
    public interface IListaReproduccionRepository
    {
        ListaReproduccion AgregarListaReproduccion(ListaReproduccion listaReproduccion);
        ListaReproduccion EditarListaReproduccion(ListaReproduccion listaReproduccion);
        void EliminarListaReproduccion(int idListaReproduccion);
        List<ListaReproduccion> GetListaReproduccions();
        IEnumerable<ListaReproduccion> ObtenerListasPorUsuario(int usuarioId);
        ListaReproduccion ObtenerListaPorNombre(string nombre);
        ListaReproduccion ObtenerListaPorId(int id);
    }
}
