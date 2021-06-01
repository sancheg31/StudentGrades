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
    public class CourseModulesController : Controller
    {
        private readonly DBStudentGradesContext _context;
        private string _currentRole;
        private int _courseId;

        public CourseModulesController(DBStudentGradesContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int courseId, string viewParam)
        {
            var courseModules = (
                                from module in _context.CourseModules
                                where module.CourseId == courseId
                                select module
                                )
                                .Include(c => c.Course)
                                .Include(c => c.ModuleType);

            string CourseName = (
                                from discipline in _context.Disciplines
                                where
                                    (
                                    from course in _context.Courses
                                    where course.Id == courseId
                                    select course.DisciplineId
                                    )
                                    .ToList()
                                    .Contains(discipline.Id)
                                    select discipline.Name
                                ).ToList()[0];

            _currentRole = (viewParam == null) ? "teacher" : viewParam;
            _courseId = courseId;

            ViewData["Name"] = CourseName;
            ViewData["Role"] = (viewParam == null) ? "teacher" : viewParam;
            ViewData["CourseId"] = courseId;
            TempData["CourseId"] = courseId;
            TempData["Role"] = _currentRole;

            return View(await courseModules.ToListAsync());
        }

        public IActionResult NavigateToCourse(int courseId)
        {
            string role = (string)ViewData["Role"];
            if (role == "admin")
            {
                return RedirectToAction("Index", "Courses");
            }
            else if (role == "teacher")
            {
                return RedirectToAction("ShowTeacherCourses", "Courses");
            }
            else if (role == "student")
            {
                return RedirectToAction("ShowStudentCourses", "Courses");
            }

            return NotFound();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModule = await _context.CourseModules
                .Include(c => c.Course)
                .Include(c => c.ModuleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseModule == null)
            {
                return NotFound();
            }

            return View(courseModule);
        }

        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Info");
            ViewData["ModuleTypeid"] = new SelectList(_context.ModuleTypes, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModuleTypeid,Name,MaxScore,DueDate,Info")] CourseModule courseModule)
        {
            if (ModelState.IsValid)
            {
                courseModule.CourseId = (int)TempData["CourseId"];
                _context.Add(courseModule);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "CourseModules", new { courseId = (int)TempData["CourseId"], viewParam = (string)TempData["Role"]});
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Info", courseModule.CourseId);
            ViewData["ModuleTypeid"] = new SelectList(_context.ModuleTypes, "Id", "Name", courseModule.ModuleTypeid);
            return View(courseModule);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModule = await _context.CourseModules.FindAsync(id);
            if (courseModule == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Info", courseModule.CourseId);
            ViewData["ModuleTypeid"] = new SelectList(_context.ModuleTypes, "Id", "Name", courseModule.ModuleTypeid);
            return View(courseModule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModuleTypeid,Name,MaxScore,DueDate,Info")] CourseModule courseModule)
        {
            if (id != courseModule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseModule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseModuleExists(courseModule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "CourseModules", new { courseId = (int)TempData["CourseId"], viewParam = (string)TempData["Role"] });
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Info", courseModule.CourseId);
            ViewData["ModuleTypeid"] = new SelectList(_context.ModuleTypes, "Id", "Name", courseModule.ModuleTypeid);
            return View(courseModule);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModule = await _context.CourseModules.FindAsync(id);
            _context.CourseModules.Remove(courseModule);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "CourseModules", new { courseId = (int)TempData["CourseId"], viewParam = (string)TempData["Role"] });
        }

        private bool CourseModuleExists(int id)
        {
            return _context.CourseModules.Any(e => e.Id == id);
        }
    }
}
