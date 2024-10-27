﻿using ReproductorDeMusica.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Logica.Interfaces
{
    public interface ICancionService
    {
        Cancion CrearCancion(Cancion cancion);
        Cancion EditarCancion(Cancion cancion);
        void EliminarCancion(int idCancion);
        List<Cancion> GetCancions();
        Cancion GetCancionById(int id);
        IEnumerable<Cancion> BuscarCancionesPorNombre(string nombre);
    }
}