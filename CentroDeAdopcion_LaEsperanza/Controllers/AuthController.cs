using CentroDeAdopcion_LaEsperanza.DB_Context;
using CentroDeAdopcion_LaEsperanza.Models;
using CentroDeAdopcion_LaEsperanza.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;



namespace CentroDeAdopcion_LaEsperanza.Controllers
{
    public class AuthController : Controller
    {
        private readonly CentroDeAdopcionContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;
        public AuthController(CentroDeAdopcionContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();

        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioViewModel viewmodel)
        {
            if (viewmodel.Email.IsNullOrEmpty() || viewmodel.Contrasena.IsNullOrEmpty())
            {
                ViewData["errorRequeridos"] = "El correo y la contraseña son requeridos.";
                return View();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == viewmodel.Email);

            if (usuario == null)
            {
                ViewData["errorUsuarioNoEncontrado"] = "Correo electrónico o contraseña incorrectos";
                return View();
            }

            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Contrasena, viewmodel.Contrasena);

            if (result == PasswordVerificationResult.Failed)
            {
                ViewData["errorUsuarioNoEncontrado"] = "Correo electrónico o contraseña incorrectos";
                return View();
            }
         //   string profilePictureUrl = "/Imagenes/user.png"; // Usa la URL predeterminada si no hay una foto

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,usuario.Nombre),
                new Claim(ClaimTypes.Email,usuario.Email),
                new Claim(ClaimTypes.Role,usuario.Rol),
                new Claim(ClaimTypes.NameIdentifier,usuario.IdUsuario.ToString())
              //  new Claim("ProfilePicture",profilePictureUrl)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(

                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);

            if (usuario.Rol =="admin")
            {

                return RedirectToAction("Index", "Usuarios");
            }
            else if (usuario.Rol == "adoptante")
            {
                return RedirectToAction("Index", "Mascotas");
            }
            else if (usuario.Rol =="propietario")
            {

                return RedirectToAction("Index", "Mascotas");
            }
            return RedirectToAction("Login");

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UsuarioViewModel viewmodel)

        {
            if (viewmodel.Contrasena != viewmodel.ConfirmarContrasena)
            {
                ViewData["errorCC"] = "Las contraseñas deben ser iguales";
                return View();
            }

            if (ModelState.IsValid)
            {
                var nuevoUsuario = new Usuario
                {
                    Nombre = viewmodel.Nombre,
                    Apellido = viewmodel.Apellido,
                    Email = viewmodel.Email,
                    Telefono = viewmodel.Telefono,
                    Direccion = viewmodel.Direccion,
                    Rol = viewmodel.Rol,
                };

                nuevoUsuario.Contrasena = _passwordHasher.HashPassword(nuevoUsuario, viewmodel.Contrasena);



                _context.Add(nuevoUsuario);
                await _context.SaveChangesAsync();


                return RedirectToAction("Login", "Auth");
            }


            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


    }
}
