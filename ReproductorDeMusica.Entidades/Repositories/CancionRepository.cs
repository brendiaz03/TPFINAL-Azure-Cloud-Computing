﻿using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Entidades.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Entidades.Repositories
{
    public class CancionRepository : ICancionRepository
    {
        private readonly Tpweb3AzureContext _context;

        public Cancion CrearCancion(Cancion cancion)
        {
            _context.Cancions.Add(cancion);
            _context.SaveChanges();
            return cancion;
        }

        public Cancion EditarCancion(Cancion cancion)
        {
            _context.Update(cancion);
            _context.SaveChanges();
            return _context.Cancions.Find(cancion);
        }

        public void EliminarCancion(int idCancion)
        {
            Cancion c = _context.Cancions.Find(idCancion);
            _context.Remove(c);
            _context.SaveChanges();
        }

        public List<Cancion> GetCancions()
        {
            return _context.Cancions.ToList();
        }
    }
}
