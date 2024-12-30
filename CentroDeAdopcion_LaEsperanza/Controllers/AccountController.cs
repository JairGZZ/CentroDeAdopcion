using CentroDeAdopcion_LaEsperanza.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CentroDeAdopcion_LaEsperanza.DB_Context;
using CentroDeAdopcion_LaEsperanza.Models;

namespace CentroDeAdopcion_LaEsperanza.Controllers
{
    public class AccountController : Controller
    {
        private readonly CloudinaryService _cloudinaryService;
        private readonly CentroDeAdopcionContext _context;

        public AccountController(CloudinaryService cloudinaryService, CentroDeAdopcionContext context)
        {
            _cloudinaryService = cloudinaryService;
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeProfilePicture(IFormFile fotoFile)
        {
            if (fotoFile == null || fotoFile.Length == 0)
            {
                return BadRequest("No se seleccionó ninguna imagen.");
            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("Usuario no autenticado.");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (usuario == null)
            {
                return BadRequest("Usuario no encontrado.");
            }
            var imageUrl = await _cloudinaryService.UploadImageToCloudinary(fotoFile);
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest("No se pudo subir la imagen. Inténtalo nuevamente.");
            }

            try
            {
                usuario.FotoUrl = imageUrl;
                await _context.SaveChangesAsync();

                TempData["Mensaje"] = "Foto de perfil actualizada exitosamente.";

                return RedirectToAction("Index", "Mascotas");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al guardar la imagen en la base de datos.");
            }
        }
    }
}
