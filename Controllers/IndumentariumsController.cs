using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class IndumentariumsController : Controller
    {
        private readonly MydbContext _context;

        public IndumentariumsController(MydbContext context)
        {
            _context = context;
        }

        // GET: Indumentariums
        public async Task<IActionResult> Index()
        {
            var mydbContext = _context.Indumentaria.Include(i => i.Categoria).Include(i => i.Genero);
            return View(await mydbContext.ToListAsync());
        }

        // GET: Indumentariums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Indumentaria == null)
            {
                return NotFound();
            }

            var indumentarium = await _context.Indumentaria
                .Include(i => i.Categoria)
                .Include(i => i.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indumentarium == null)
            {
                return NotFound();
            }

            return View(indumentarium);
        }

        // GET: Indumentariums/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id");
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Idgenero", "Idgenero");
            return View();
        }

        // POST: Indumentariums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Tipo,Detalle,Precio,Talle,Stock,Img,CategoriaId,GeneroId")] Indumentarium indumentarium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(indumentarium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", indumentarium.CategoriaId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Idgenero", "Idgenero", indumentarium.GeneroId);
            return View(indumentarium);
        }

        // GET: Indumentariums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Indumentaria == null)
            {
                return NotFound();
            }

            var indumentarium = await _context.Indumentaria.FindAsync(id);
            if (indumentarium == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", indumentarium.CategoriaId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Idgenero", "Idgenero", indumentarium.GeneroId);
            return View(indumentarium);
        }

        // POST: Indumentariums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Tipo,Detalle,Precio,Talle,Stock,Img,CategoriaId,GeneroId")] Indumentarium indumentarium)
        {
            if (id != indumentarium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(indumentarium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndumentariumExists(indumentarium.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", indumentarium.CategoriaId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Idgenero", "Idgenero", indumentarium.GeneroId);
            return View(indumentarium);
        }

        // GET: Indumentariums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Indumentaria == null)
            {
                return NotFound();
            }

            var indumentarium = await _context.Indumentaria
                .Include(i => i.Categoria)
                .Include(i => i.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indumentarium == null)
            {
                return NotFound();
            }

            return View(indumentarium);
        }

        // POST: Indumentariums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Indumentaria == null)
            {
                return Problem("Entity set 'MydbContext.Indumentaria'  is null.");
            }
            var indumentarium = await _context.Indumentaria.FindAsync(id);
            if (indumentarium != null)
            {
                _context.Indumentaria.Remove(indumentarium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndumentariumExists(int id)
        {
          return (_context.Indumentaria?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
