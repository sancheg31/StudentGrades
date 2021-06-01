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

            Student student = await _context.Students
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups, "Id", "Name", student.StudentGroupId);
            ViewData["Email"] = new SelectList(_context.UserInfos, "Id", "Email", student.UserInfoId);
            ViewData["Telephone"] = new SelectList(_context.UserInfos, "Id", "Telephone", student.UserInfoId);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int StudentGroupId, string Email, string Telephone, string Login, string Password)
        {

            var studentToUpdate = await _context.Students
                                    .Include(s => s.StudentGroup)
                                    .Include(s => s.StudentNavigation)
                                    .Include(s => s.Term)
                                    .Include(s => s.UserInfo)
                                    .FirstOrDefaultAsync(m => m.Id == id);

            studentToUpdate.StudentGroupId = StudentGroupId;
            studentToUpdate.UserInfo.Email = Email;
            studentToUpdate.UserInfo.Telephone = Telephone;
            studentToUpdate.UserInfo.Login = Login;
            studentToUpdate.UserInfo.Password = Password;

            _context.Update(studentToUpdate);
            await _context.SaveChangesAsync();

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Students", new { id = studentToUpdate.Id });
            }

            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups, "Id", "Name", studentToUpdate.StudentGroupId);
            ViewData["UserInfoId"] = new SelectList(_context.UserInfos, "Id", "Email", studentToUpdate.UserInfoId).ToList();
            return View(studentToUpdate);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
