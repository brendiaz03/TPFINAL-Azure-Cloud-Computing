using ReproductorDeMusica.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Entidades.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Usuario BuscarUsuario(Usuario usuario);
    Usuario buscarUsuarioPorID(int usuarioId);
    void RegistrarUsuario(Usuario usuario);
    Usuario buscarUsuarioPorUsername(string nombreUsuario);
    Usuario BuscarUsuarioPorMail(string mail);
    Usuario ActualizarInfoUsuario(Usuario usuario);

}
