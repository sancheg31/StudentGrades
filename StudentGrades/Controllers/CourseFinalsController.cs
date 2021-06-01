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
    public class CourseFinalsController : Controller
    {
        private readonly DBStudentGradesContext _context;

        public CourseFinalsController(DBStudentGradesContext context)
        {
            _context = context;
        }

        // GET: CourseFinals
        public async Task<IActionResult> Index()
        {
            var dBStudentGradesContext = _context.CourseFinals.Include(c => c.Course).Include(c => c.Student);
            return View(await dBStudentGradesContext.ToListAsync());
        }

        // GET: CourseFinals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseFinal = await _context.CourseFinals
                .Include(c => c.Course)
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseFinal == null)
            {
                return NotFound();
            }

            return View(courseFinal);
        }

        // GET: CourseFinals/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Info");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: CourseFinals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,StudentId,Score,ExamDate")] CourseFinal courseFinal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseFinal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Info", courseFinal.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", courseFinal.StudentId);
            return View(courseFinal);
        }

        // GET: CourseFinals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseFinal = await _context.CourseFinals.FindAsync(id);
            if (courseFinal == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Info", courseFinal.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", courseFinal.StudentId);
            return View(courseFinal);
        }

        // POST: CourseFinals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,StudentId,Score,ExamDate")] CourseFinal courseFinal)
        {
            if (id != courseFinal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseFinal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseFinalExists(courseFinal.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Info", courseFinal.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", courseFinal.StudentId);
            return View(courseFinal);
        }

        // GET: CourseFinals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseFinal = await _context.CourseFinals
                .Include(c => c.Course)
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseFinal == null)
            {
                return NotFound();
            }

            return View(courseFinal);
        }

        // POST: CourseFinals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseFinal = await _context.CourseFinals.FindAsync(id);
            _context.CourseFinals.Remove(courseFinal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseFinalExists(int id)
        {
            return _context.CourseFinals.Any(e => e.Id == id);
        }
    }
}
