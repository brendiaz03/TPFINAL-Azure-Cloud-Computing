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

        public void EliminarCancionDeLaLista(int idCancion, int idLista)
        {
            var lista = _context.CancionListaReproduccions
                .Where(i => i.IdListaReproduccion == idLista && i.IdCancion == idCancion);

            _context.Remove(lista);
            _context.SaveChanges();
        }

        public void EliminarCancionDeTodasLasListas(int idCancion)
        {
            var listasConIdCancion = _context.CancionListaReproduccions
                .Where(c => c.IdCancion == idCancion)
                .ToList();

            foreach (var lista in listasConIdCancion)
            {
                _context.Remove(lista);
            }

            _context.SaveChanges();
        }
    }
}
