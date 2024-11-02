using System;

namespace ReproductorDeMusica.Logica
{
    public class UsuarioExistenteException : Exception
    {
        public UsuarioExistenteException()
            : base("El usuario ya existe.")
        {
        }

        public UsuarioExistenteException(string message)
            : base(message)
        {
        }

        public UsuarioExistenteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
