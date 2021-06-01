using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentGrades;

namespace StudentGrades.Controllers
{
    public class CaphedrasController : Controller
    {
        private readonly DBStudentGradesContext _context;

        public CaphedrasController(DBStudentGradesContext context)
        {
            _context = context;
        }

        // GET: Caphedras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Caphedras.ToListAsync());
        }

        // GET: Caphedras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caphedra = await _context.Caphedras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caphedra == null)
            {
                return NotFound();
            }

            return View(caphedra);
        }

        // GET: Caphedras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Caphedras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Info")] Caphedra caphedra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caphedra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caphedra);
        }

        // GET: Caphedras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caphedra = await _context.Caphedras.FindAsync(id);
            if (caphedra == null)
            {
                return NotFound();
            }
            return View(caphedra);
        }

        // POST: Caphedras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Info")] Caphedra caphedra)
        {
            if (id != caphedra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caphedra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaphedraExists(caphedra.Id))
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
            return View(caphedra);
        }

        // GET: Caphedras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caphedra = await _context.Caphedras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caphedra == null)
            {
                return NotFound();
            }

            return View(caphedra);
        }

        // POST: Caphedras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caphedra = await _context.Caphedras.FindAsync(id);
            _context.Caphedras.Remove(caphedra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaphedraExists(int id)
        {
            return _context.Caphedras.Any(e => e.Id == id);
        }
    }
}
