using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica.Interfaces;
using System.Text;
using System.Security.Cryptography;
using ReproductorDeMusica.Entidades.Repositories.Interfaces;

using ReproductorDeMusica.Entidades.Repositories;

namespace ReproductorDeMusica.Logica
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioPlanRepository _usuarioPlanRepository;


        public UsuarioService(IUsuarioRepository usuarioRepository, IUsuarioPlanRepository usuarioPlanRepository)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioPlanRepository = usuarioPlanRepository;
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            if (BuscarUsuario(usuario) == null)
            {
                usuario.Contrasenia=this.HashPassword(usuario.Contrasenia);
                const int PLAN_GRATUITO = 1;
                this._usuarioRepository.RegistrarUsuario(usuario);
                this._usuarioPlanRepository.AgregarNuevoUsuarioPlan(PLAN_GRATUITO, usuario.Id);
            }
            else
            {
                throw new UsuarioExistenteException("El usuario ya existe.");
            }
        }

        public Usuario BuscarUsuario(Usuario usuario)
        {
            return this._usuarioRepository.BuscarUsuario(usuario);
        }

        public Usuario ValidarUsuario(string nombreUsuario, string contrasenia)
        {

            var usuario = this._usuarioRepository.buscarUsuarioPorUsername(nombreUsuario);
            if (usuario == null)
            {
                return null;
            }

            bool isPasswordValid = VerifyPassword(contrasenia, usuario.Contrasenia);

            return isPasswordValid ? usuario : null;
        }

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

        public Usuario BuscarUsuarioPorID(int usuarioId)
        {
            return this._usuarioRepository.buscarUsuarioPorID(usuarioId);
        }

        public Usuario BuscarUsuarioPorMail(string mail)
        {
            return this._usuarioRepository.BuscarUsuarioPorMail(mail);
        }

        public Usuario ActualizarInfoUsuario(Usuario usuario)
        {
            try
            {
                if (BuscarUsuarioPorID(usuario.Id) != null)
                {
                    this._usuarioRepository.ActualizarInfoUsuario(usuario);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return usuario;
        }

    }

  
}
