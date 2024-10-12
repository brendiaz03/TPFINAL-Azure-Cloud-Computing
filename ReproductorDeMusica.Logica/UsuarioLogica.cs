using ReproductorDeMusica.Entidades;

namespace ReproductorDeMusica.Logica
{
    public class UsuarioLogica:IUsuarioLogica
    {

        private List<Usuario> _items = new List<Usuario>();

        public void RegistrarUsuario(Usuario usuario)
        {
        usuario.Id = _items.Count == 0 ? 1 : _items.Max(x => x.Id) + 1;
            _items.Add(usuario);
        }

        public Usuario ValidarUsuario(string nombreUsuario, string contrasenia)
        {
            return _items.FirstOrDefault(u => u.NombreUsuario == nombreUsuario && u.Contrasenia == contrasenia);
        }

    }
    public interface IUsuarioLogica
    {
        void RegistrarUsuario(Usuario usuario);
        Usuario ValidarUsuario(string nombreUsuario, string contrasenia); 

    }
}
