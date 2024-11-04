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
    public class ListaReproduccionRepository : IListaReproduccionRepository
    {
        private readonly Tpweb3AzureContext _context;

        public ListaReproduccionRepository(Tpweb3AzureContext context)
        {
            _context = context;
        }

        public ListaReproduccion AgregarListaReproduccion(ListaReproduccion listaReproduccion)
        {
            var existente = _context.ListaReproduccions
            .FirstOrDefault(l => l.Nombre == listaReproduccion.Nombre 
            && l.IdUsuario == listaReproduccion.IdUsuario);

            if (existente == null)
            {
                _context.Add(listaReproduccion);
                _context.SaveChanges();
                return listaReproduccion;
            }

            return null;
        }

        public ListaReproduccion EditarListaReproduccion(ListaReproduccion listaReproduccion)
        {
            _context.Update(listaReproduccion);
            _context.SaveChanges();
            return _context.ListaReproduccions.Find(listaReproduccion);
        }

        public void EliminarListaReproduccion(int idListaReproduccion)
        {
            ListaReproduccion p = _context.ListaReproduccions.Find(idListaReproduccion);
            _context.Remove(p);
            _context.SaveChanges();
        }

        public List<ListaReproduccion> GetListaReproduccions()
        {
            return _context.ListaReproduccions.ToList();
        }

        public IEnumerable<ListaReproduccion> ObtenerListasPorUsuario(int usuarioId)
        {
            return _context.ListaReproduccions
                           .Include(l => l.CancionListaReproduccions) 
                           .Where(l => l.IdUsuario == usuarioId)
                           .ToList();
        }

        public ListaReproduccion ObtenerListaPorId(int id)
        {
            return _context.ListaReproduccions
                   .Include(l => l.CancionListaReproduccions)
                   .ThenInclude(cl => cl.IdCancionNavigation)
                   .FirstOrDefault(l => l.Id == id);

        }

        public ListaReproduccion ObtenerListaPorNombre(string nombre)
        {
            return _context.ListaReproduccions
                           .Where(l => l.Nombre.ToLower() == nombre.ToLower())
                           .FirstOrDefault(); 
        }
    }
}
