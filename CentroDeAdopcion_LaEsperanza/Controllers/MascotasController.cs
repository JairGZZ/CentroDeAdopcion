using CentroDeAdopcion_LaEsperanza.Services;
using CentroDeAdopcion_LaEsperanza.DB_Context;
using CentroDeAdopcion_LaEsperanza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;



namespace CentroDeAdopcion_LaEsperanza.Controllers
{
    [Authorize(Roles ="adoptante")]
    public class MascotasController : Controller
    {
        private readonly CentroDeAdopcionContext _context;
        private readonly CloudinaryService _cloudinaryService;

        public MascotasController(CentroDeAdopcionContext context, CloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<IActionResult> Index(string buscar)
        {
            var centroDeAdopcionContext = _context.Mascotas.Include(m => m.IdPropietarioNavigation).Where(a => a.Estado == "Disponible").AsQueryable();
            if (!String.IsNullOrEmpty(buscar))
            {
                buscar = buscar.Trim();
                centroDeAdopcionContext = centroDeAdopcionContext.Where(b => b.Tipo.Contains(buscar) || b.Sexo!.Contains(buscar));
            } 

            return View(await centroDeAdopcionContext.ToListAsync());
        }

        // GET: Mascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .Include(m => m.IdPropietarioNavigation)
                .FirstOrDefaultAsync(m => m.IdMascota == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // GET: Mascotas/Create
        public IActionResult Create()
        {

            ViewData["IdPropietario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMascota,Nombre,Tipo,Raza,Edad,Tamaño,Sexo,EstadoSalud,Foto,Estado")] Mascota mascota, IFormFile fotoFile)
        {
            if (ModelState.IsValid)
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                var propietario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == userEmail);
                if (propietario == null)
                {
                    return BadRequest("El propietario no fue encontrado.");
                }

                mascota.IdPropietario = propietario.IdUsuario;

                if (fotoFile != null && fotoFile.Length > 0)
                {
                    var imageUrl = await _cloudinaryService.UploadImageToCloudinary(fotoFile);
                    if (imageUrl != null)
                    {
                        mascota.Foto = imageUrl;
                    }
                }

                _context.Add(mascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hay errores de validación, devuelve la vista con los datos actuales
            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMascota,Nombre,Tipo,Raza,Edad,Tamaño,Sexo,EstadoSalud,Foto")] Mascota mascota, IFormFile? fotoFile)
        {
            if (id != mascota.IdMascota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingMascota = await _context.Mascotas
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.IdMascota == id);

                if (existingMascota == null)
                {
                    return NotFound();
                }

                mascota.IdPropietario = existingMascota.IdPropietario;

                if (fotoFile != null && fotoFile.Length > 0)
                {
                    var imageUrl = await _cloudinaryService.UploadImageToCloudinary(fotoFile);
                    if (imageUrl != null)
                    {
                        mascota.Foto = imageUrl;
                    }
                }
                else
                {
                    mascota.Foto = existingMascota.Foto;
                }

                try
                {
                    _context.Update(mascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotaExists(mascota.IdMascota))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPropietario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", mascota.IdPropietario);
            return View(mascota);
        }




        // GET: Mascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .Include(m => m.IdPropietarioNavigation)
                .FirstOrDefaultAsync(m => m.IdMascota == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

    
        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota != null)
            {
                _context.Mascotas.Remove(mascota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotaExists(int id)
        {
            return _context.Mascotas.Any(e => e.IdMascota == id);
        }
    }
}
