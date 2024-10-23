using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ReproductorDeMusica.Web.Models
{
    public class LoginViewModel
    {
        [DisplayName("Nombre de Usuario")]
        [Required(ErrorMessage = "Nombre de usuario es requerido")]
        public string NombreUsuario { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public string Contrasenia { get; set; }
    }
}
