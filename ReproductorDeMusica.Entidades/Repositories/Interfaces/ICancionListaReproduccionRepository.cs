using ReproductorDeMusica.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Entidades.Repositories.Interfaces
{
    public interface ICancionListaReproduccionRepository
    {
        CancionListaReproduccion CrearCancionListaReproduccion(CancionListaReproduccion cancionLista);
    }
}
