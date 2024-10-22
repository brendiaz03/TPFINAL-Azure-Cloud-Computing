using ReproductorDeMusica.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Entidades.Repositories.Interfaces
{
    public interface ICancionRepository
    {
        Cancion CrearCancion(Cancion cancion);
        Cancion EditarCancion(Cancion cancion);
        void EliminarCancion(int idCancion);
        List<Cancion> GetCancions();
        IEnumerable<Cancion> BuscarCancionesPorNombre(string nombre);
    }
}
