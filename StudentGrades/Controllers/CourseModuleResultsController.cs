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
    public class CourseModuleResultsController : Controller
    {
        private readonly DBStudentGradesContext _context;

        public CourseModuleResultsController(DBStudentGradesContext context)
        {
            _context = context;
        }

        // GET: CourseModuleResults
        public async Task<IActionResult> Index()
        {
            var dBStudentGradesContext = _context.CourseModuleResults.Include(c => c.CourseModule).Include(c => c.Student);
            return View(await dBStudentGradesContext.ToListAsync());
        }

        // GET: CourseModuleResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModuleResult = await _context.CourseModuleResults
                .Include(c => c.CourseModule)
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseModuleResult == null)
            {
                return NotFound();
            }

            return View(courseModuleResult);
        }

        // GET: CourseModuleResults/Create
        public IActionResult Create()
        {
            ViewData["CourseModuleId"] = new SelectList(_context.CourseModules, "Id", "Info");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: CourseModuleResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseModuleId,StudentId,Score")] CourseModuleResult courseModuleResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseModuleResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseModuleId"] = new SelectList(_context.CourseModules, "Id", "Info", courseModuleResult.CourseModuleId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", courseModuleResult.StudentId);
            return View(courseModuleResult);
        }

        // GET: CourseModuleResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModuleResult = await _context.CourseModuleResults.FindAsync(id);
            if (courseModuleResult == null)
            {
                return NotFound();
            }
            ViewData["CourseModuleId"] = new SelectList(_context.CourseModules, "Id", "Info", courseModuleResult.CourseModuleId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", courseModuleResult.StudentId);
            return View(courseModuleResult);
        }

        // POST: CourseModuleResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseModuleId,StudentId,Score")] CourseModuleResult courseModuleResult)
        {
            if (id != courseModuleResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseModuleResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseModuleResultExists(courseModuleResult.Id))
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
            ViewData["CourseModuleId"] = new SelectList(_context.CourseModules, "Id", "Info", courseModuleResult.CourseModuleId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", courseModuleResult.StudentId);
            return View(courseModuleResult);
        }

        // GET: CourseModuleResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModuleResult = await _context.CourseModuleResults
                .Include(c => c.CourseModule)
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseModuleResult == null)
            {
                return NotFound();
            }

            return View(courseModuleResult);
        }

        // POST: CourseModuleResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseModuleResult = await _context.CourseModuleResults.FindAsync(id);
            _context.CourseModuleResults.Remove(courseModuleResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseModuleResultExists(int id)
        {
            return _context.CourseModuleResults.Any(e => e.Id == id);
        }
    }
}
