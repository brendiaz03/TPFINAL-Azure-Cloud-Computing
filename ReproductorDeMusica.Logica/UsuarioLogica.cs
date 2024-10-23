using ReproductorDeMusica.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace ReproductorDeMusica.Logica
{
    public class UsuarioLogica : IUsuarioLogica
    {
        private readonly Tpweb3AzureContext _context;

        public UsuarioLogica(Tpweb3AzureContext context)
        {
            _context = context;
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            if (BuscarUsuario(usuario) == null)
            {
                usuario.Contrasenia=this.HashPassword(usuario.Contrasenia);
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
            }
            else
            {
                throw new UsuarioExistenteException("El usuario ya existe.");
            }
        }

        public Usuario BuscarUsuario(Usuario usuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == usuario.NombreUsuario || u.Email == usuario.Email);
        }

        public Usuario ValidarUsuario(string nombreUsuario, string contrasenia)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null)
            {
                return null;
            }

            bool isPasswordValid = VerifyPassword(contrasenia, usuario.Contrasenia);

            return isPasswordValid ? usuario : null;
        }

       
        //HASH DE CONTRASEÑAS
        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashOfInput = HashPassword(inputPassword);

            return hashOfInput.Equals(storedHash);
        }

        public Usuario buscarUsuarioPorID(int usuarioId)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
        }
    }

    public interface IUsuarioLogica
    {
        Usuario buscarUsuarioPorID(int usuarioId);
        void RegistrarUsuario(Usuario usuario);
        Usuario ValidarUsuario(string nombreUsuario, string contrasenia);
    }
}
