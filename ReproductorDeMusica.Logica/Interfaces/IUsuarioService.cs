using ReproductorDeMusica.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Logica.Interfaces;

public interface IUsuarioService
{
    Usuario BuscarUsuarioPorID(int usuarioId);
    void RegistrarUsuario(Usuario usuario);
    Usuario ValidarUsuario(string nombreUsuario, string contrasenia);
    Usuario BuscarUsuarioPorMail(string mail);
}
