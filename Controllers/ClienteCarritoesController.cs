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
    public class ClienteCarritoesController : Controller
    {
        private readonly MydbContext _context;

        public ClienteCarritoesController(MydbContext context)
        {
            _context = context;
        }

        // GET: ClienteCarritoes
        public async Task<IActionResult> Index()
        {
            var mydbContext = _context.ClienteCarritos.Include(c => c.IdCarritoNavigation).Include(c => c.IdClienteNavigation);
            return View(await mydbContext.ToListAsync());
        }

        // GET: ClienteCarritoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClienteCarritos == null)
            {
                return NotFound();
            }

            var clienteCarrito = await _context.ClienteCarritos
                .Include(c => c.IdCarritoNavigation)
                .Include(c => c.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (clienteCarrito == null)
            {
                return NotFound();
            }

            return View(clienteCarrito);
        }

        // GET: ClienteCarritoes/Create
        public IActionResult Create()
        {
            ViewData["IdCarrito"] = new SelectList(_context.Carritos, "IdCarrito", "IdCarrito");
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id");
            return View();
        }

        // POST: ClienteCarritoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,IdCarrito,Cantidad")] ClienteCarrito clienteCarrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteCarrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCarrito"] = new SelectList(_context.Carritos, "IdCarrito", "IdCarrito", clienteCarrito.IdCarrito);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", clienteCarrito.IdCliente);
            return View(clienteCarrito);
        }

        // GET: ClienteCarritoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClienteCarritos == null)
            {
                return NotFound();
            }

            var clienteCarrito = await _context.ClienteCarritos.FindAsync(id);
            if (clienteCarrito == null)
            {
                return NotFound();
            }
            ViewData["IdCarrito"] = new SelectList(_context.Carritos, "IdCarrito", "IdCarrito", clienteCarrito.IdCarrito);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", clienteCarrito.IdCliente);
            return View(clienteCarrito);
        }

        // POST: ClienteCarritoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,IdCarrito,Cantidad")] ClienteCarrito clienteCarrito)
        {
            if (id != clienteCarrito.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteCarrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteCarritoExists(clienteCarrito.IdCliente))
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
            ViewData["IdCarrito"] = new SelectList(_context.Carritos, "IdCarrito", "IdCarrito", clienteCarrito.IdCarrito);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", clienteCarrito.IdCliente);
            return View(clienteCarrito);
        }

        // GET: ClienteCarritoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClienteCarritos == null)
            {
                return NotFound();
            }

            var clienteCarrito = await _context.ClienteCarritos
                .Include(c => c.IdCarritoNavigation)
                .Include(c => c.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (clienteCarrito == null)
            {
                return NotFound();
            }

            return View(clienteCarrito);
        }

        // POST: ClienteCarritoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClienteCarritos == null)
            {
                return Problem("Entity set 'MydbContext.ClienteCarritos'  is null.");
            }
            var clienteCarrito = await _context.ClienteCarritos.FindAsync(id);
            if (clienteCarrito != null)
            {
                _context.ClienteCarritos.Remove(clienteCarrito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteCarritoExists(int id)
        {
          return (_context.ClienteCarritos?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
