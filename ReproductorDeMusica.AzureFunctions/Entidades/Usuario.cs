using System;
using System.Collections.Generic;

namespace ReproductorDeMusica.AzureFunctions.Entidades
{
    public partial class Usuario
    {
        public Usuario()
        {
            UsuarioPlans = new HashSet<UsuarioPlan>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string ImagenUsuario { get; set; }

        public virtual ICollection<UsuarioPlan> UsuarioPlans { get; set; }
    }
}
