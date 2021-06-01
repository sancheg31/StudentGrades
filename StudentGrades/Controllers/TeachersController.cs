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
    public class TeachersController : Controller
    {
        private readonly DBStudentGradesContext _context;

        public TeachersController(DBStudentGradesContext context)
        {
            _context = context;
        }


        // GET: Teachers/Details/5
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.AcademicDegree)
                .Include(t => t.Caphedra)
                .Include(t => t.UserInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "Id", "Name", teacher.AcademicDegreeId);
            ViewData["CaphedraId"] = new SelectList(_context.Caphedras, "Id", "Name", teacher.CaphedraId);
           
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AcademicDegreeId,CaphedraId,Age,Birthday,Info")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
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
            ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "Id", "Name", teacher.AcademicDegreeId);
            ViewData["CaphedraId"] = new SelectList(_context.Caphedras, "Id", "Name", teacher.CaphedraId);
            
            return View(teacher);
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
