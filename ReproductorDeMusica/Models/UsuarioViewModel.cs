using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ReproductorDeMusica.Entidades.Entidades;
using System.Text.RegularExpressions;

namespace ReproductorDeMusica.Web.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El campo Nombre no puede tener más de 50 caracteres")]
        public string Nombre { get; set; }

        [DisplayName("Apellido")]
        [Required(ErrorMessage = "Apellido es requerido")]
        [MaxLength(50, ErrorMessage = "El campo Apellido no puede tener más de 50 caracteres")]
        public string Apellido { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email es requerido")]
        [EmailAddress(ErrorMessage = "El formato del Email es incorrecto")] // Valida el formato de email
        [MaxLength(50, ErrorMessage = "El campo Email no puede tener más de 50 caracteres")]
        public string Email { get; set; }

        [DisplayName("NombreUsuario")]
        [Required(ErrorMessage = "Nombre de usuario es requerido")]
        [MaxLength(50, ErrorMessage = "El campo Nombre de Usuario no puede tener más de 50 caracteres")]
        public string NombreUsuario { get; set; }

        [DisplayName("Contrasenia")]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [MaxLength(50, ErrorMessage = "El campo Contraseña no puede tener más de 50 caracteres")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$",
            ErrorMessage = "La contraseña debe tener al menos una letra, un número y 6 caracteres")] // Valida la contraseña
        public string Contrasenia { get; set; }

        public IFormFile? ImagenUsuario { get; set; }


        public static Usuario ToUsuario(UsuarioViewModel usuarioModel, string imagenUrl)
        {
            return new Usuario
            {
                Id = usuarioModel.Id,
                Nombre = usuarioModel.Nombre,
                Apellido = usuarioModel.Apellido,
                Email = usuarioModel.Email,
                NombreUsuario = usuarioModel.NombreUsuario,
                Contrasenia = usuarioModel.Contrasenia,
                ImagenUsuario = imagenUrl
            };
        }

        public static UsuarioViewModel FromUsuario(Usuario usuario)
        {
            return new UsuarioViewModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                NombreUsuario = usuario.NombreUsuario,
                Contrasenia = usuario.Contrasenia
            };
        }
    }
}