using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Entidades.Repositories.Interfaces;
using System.Text;
using ReproductorDeMusica.Logica;

namespace ReproductorDeMusica.Entidades.Repositories
{
    public class UsuarioRepository:IUsuarioRepository
    {

        private readonly Tpweb3AzureContext _context;

        public UsuarioRepository(Tpweb3AzureContext context)
        {
            _context = context;
        }

        public void RegistrarUsuario(Usuario usuario)
        {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
        }

        public Usuario BuscarUsuario(Usuario usuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == usuario.NombreUsuario || u.Email == usuario.Email);
        }

        public Usuario buscarUsuarioPorUsername(string nombreUsuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
        }

        public Usuario buscarUsuarioPorID(int usuarioId)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
        }

        public Usuario BuscarUsuarioPorMail(string mail)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == mail);
        }
    }
}

