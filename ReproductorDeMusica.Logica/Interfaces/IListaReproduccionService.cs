using ReproductorDeMusica.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Logica.Interfaces
{
    public interface IListaReproduccionService
    {
        bool AgregarListaReproduccion(ListaReproduccion listaReproduccion);
        ListaReproduccion EditarListaReproduccion(ListaReproduccion listaReproduccion);
        void EliminarListaReproduccion(int idListaReproduccion);
        List<ListaReproduccion> GetListasReproduccions();
        IEnumerable<ListaReproduccion> ObtenerListasDeReproduccionPorUsuario(int usuarioId);
        ListaReproduccion ObtenerListasDeReproduccionPorId(int id);

    }
}
