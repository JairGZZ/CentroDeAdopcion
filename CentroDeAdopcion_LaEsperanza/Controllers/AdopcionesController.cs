using CentroDeAdopcion_LaEsperanza.DB_Context;
using CentroDeAdopcion_LaEsperanza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CentroDeAdopcion_LaEsperanza.Controllers
{
    public class AdopcionesController : Controller
    {
        private readonly CentroDeAdopcionContext _context;

        public AdopcionesController(CentroDeAdopcionContext context)
        {
            _context = context;
        }

        // GET: Adopciones
        public async Task<IActionResult> Index()
        {
            var centroDeAdopcionContext = _context.Adopciones.Include(a => a.IdAdoptanteNavigation).Include(a => a.IdMascotaNavigation);
            return View(await centroDeAdopcionContext.ToListAsync());
        }

        // GET: Adopciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adopcione = await _context.Adopciones
                .Include(a => a.IdAdoptanteNavigation)
                .Include(a => a.IdMascotaNavigation)
                .FirstOrDefaultAsync(m => m.IdAdopcion == id);
            if (adopcione == null)
            {
                return NotFound();
            }

            return View(adopcione);
        }

        // GET: Adopciones/Create
        public IActionResult Create()
        {
            ViewData["IdAdoptante"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "IdMascota");
            return View();
        }

        // POST: Adopciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAdopcion,IdMascota,IdAdoptante,FechaSolicitud")] Adopcione adopcione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adopcione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAdoptante"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", adopcione.IdAdoptante);
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "IdMascota", adopcione.IdMascota);
            return View(adopcione);
        }

        // GET: Adopciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adopcione = await _context.Adopciones.FindAsync(id);
            if (adopcione == null)
            {
                return NotFound();
            }
            ViewData["IdAdoptante"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", adopcione.IdAdoptante);
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "IdMascota", adopcione.IdMascota);
            return View(adopcione);
        }

        // POST: Adopciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAdopcion,IdMascota,IdAdoptante,FechaSolicitud")] Adopcione adopcione)
        {
            if (id != adopcione.IdAdopcion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adopcione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdopcioneExists(adopcione.IdAdopcion))
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
            ViewData["IdAdoptante"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", adopcione.IdAdoptante);
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "IdMascota", adopcione.IdMascota);
            return View(adopcione);
        }

        // GET: Adopciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adopcione = await _context.Adopciones
                .Include(a => a.IdAdoptanteNavigation)
                .Include(a => a.IdMascotaNavigation)
                .FirstOrDefaultAsync(m => m.IdAdopcion == id);
            if (adopcione == null)
            {
                return NotFound();
            }

            return View(adopcione);
        }

        // POST: Adopciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adopcione = await _context.Adopciones.FindAsync(id);
            if (adopcione != null)
            {
                _context.Adopciones.Remove(adopcione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdopcioneExists(int id)
        {
            return _context.Adopciones.Any(e => e.IdAdopcion == id);
        }
    }
}
