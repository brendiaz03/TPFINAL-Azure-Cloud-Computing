using ReproductorDeMusica.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Logica.Interfaces
{
    public interface ICancionListaReproduccionService
    {
        bool AgregarCancionListaReproduccion(int idListaReproduccion, int idCancion);
        void EliminarCancionDeLaLista(int idCancion, int idLista);
        void EliminarCancionDeTodasLasListas(int idCancion);
    }
}
