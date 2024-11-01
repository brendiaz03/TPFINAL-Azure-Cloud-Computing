using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Logica;
using System.Security.Claims;

namespace ReproductorDeMusica.Web.Controllers
{
    public class AutenticacionController : Controller
    {
        private readonly IUsuarioLogica _usuarioLogica;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AutenticacionController(IUsuarioLogica usuarioLogica, IHttpContextAccessor httpContextAccessor)
        {
            _usuarioLogica = usuarioLogica;
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task<IActionResult> Index()
        {
           
            var email = User.Identity.Name;
            Usuario usuario = _usuarioLogica.BuscarUsuarioPorMail(email);

            if (usuario == null)
            {
                // Invalida la sesión actual
                await _httpContextAccessor.HttpContext.SignOutAsync();
                // Redirigir a Azure para que vuelva a autenticar
                return Challenge(new AuthenticationProperties()); // Ajusta la RedirectUri según sea necesario
            }
            

           // HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            // Resto de la lógica...
           // return RedirectToAction("Index","Home");
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim("UsuarioId", usuario.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            Console.WriteLine("iddddddddddddddd: " + HttpContext.Session.GetInt32("UsuarioId"));
            return RedirectToAction("Index", "Home");
        }


        public IActionResult ErrorAutenticacion()
        {
            return View();
        }
    }
}
