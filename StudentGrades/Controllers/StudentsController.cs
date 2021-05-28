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
    public class StudentsController : Controller
    {
        private readonly DBStudentGradesContext _context;

        public StudentsController(DBStudentGradesContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _context.Students
                                    .Include(s => s.StudentGroup)
                                    .Include(s => s.StudentNavigation)
                                    .Include(s => s.Term)
                                    .Include(s => s.UserInfo)
                                    .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.StudentGroup)
                .Include(s => s.StudentNavigation)
                .Include(s => s.Term)
                .Include(s => s.UserInfo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create(int? userInfoId)
        {
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.StudentIds, "Id", "Code");
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Id");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,StudentGroupId,TermId,UserInfoId,Age,Brirthday")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups, "Id", "Name", student.StudentGroupId);
            ViewData["StudentId"] = new SelectList(_context.StudentIds, "Id", "Code", student.StudentId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Id", student.TermId);
            ViewData["UserInfoId"] = new SelectList(_context.UserInfos, "Id", "Email", student.UserInfoId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups, "Id", "Name", student.StudentGroupId);
            ViewData["StudentId"] = new SelectList(_context.StudentIds, "Id", "Code", student.StudentId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Id", student.TermId);
            ViewData["UserInfoId"] = new SelectList(_context.UserInfos, "Id", "Email", student.UserInfoId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,StudentGroupId,TermId,UserInfoId,Age,Brirthday")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups, "Id", "Name", student.StudentGroupId);
            ViewData["StudentId"] = new SelectList(_context.StudentIds, "Id", "Code", student.StudentId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Id", student.TermId);
            ViewData["UserInfoId"] = new SelectList(_context.UserInfos, "Id", "Email", student.UserInfoId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.StudentGroup)
                .Include(s => s.StudentNavigation)
                .Include(s => s.Term)
                .Include(s => s.UserInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
