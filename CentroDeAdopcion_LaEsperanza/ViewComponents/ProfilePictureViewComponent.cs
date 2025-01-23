using CentroDeAdopcion_LaEsperanza.DB_Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CentroDeAdopcion_LaEsperanza.ViewComponents
{
    public class ProfilePictureViewComponent : ViewComponent
    {
        private readonly CentroDeAdopcionContext _context;
        private const string DefaultImageUrl = "/imagenes/user.png";

        public ProfilePictureViewComponent(CentroDeAdopcionContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string imageUrl = DefaultImageUrl;

            try
            {
                var userEmail = UserClaimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;

                if (!string.IsNullOrEmpty(userEmail))
                {
                    var usuario = await _context.Usuarios
                        .AsNoTracking()
                        .Where(u => u.Email == userEmail)
                        .Select(u => u.FotoUrl)
                        .FirstOrDefaultAsync();

                    if (!string.IsNullOrEmpty(usuario))
                    {
                        imageUrl = usuario;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo la foto de perfil: {ex.Message}");
            }

            return View("Default", imageUrl);
        }
    }
}
