using Microsoft.EntityFrameworkCore;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Entidades.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Entidades.Repositories
{
    public class CancionListaReproduccionRepository : ICancionListaReproduccionRepository
    {
        private readonly Tpweb3AzureContext _context;

        public CancionListaReproduccionRepository(Tpweb3AzureContext context)
        {
            _context = context;
        }

        public CancionListaReproduccion CrearCancionListaReproduccion(CancionListaReproduccion cancionLista)
        {
            _context.CancionListaReproduccions.Add(cancionLista);
            _context.SaveChanges();
            return cancionLista;
        }
    }
}
