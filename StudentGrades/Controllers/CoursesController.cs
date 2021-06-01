using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using StudentGrades;
using StudentGrades.ViewModels;
namespace StudentGrades.Controllers
{
    public class CoursesController : Controller
    {
        private readonly DBStudentGradesContext _context;

        public CoursesController(DBStudentGradesContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = _context.Courses.Include(c => c.AssessmentType)
                                          .Include(c => c.Discipline)
                                          .Include(c => c.Term);
            ViewData["Role"] = "admin";
            TempData["Role"] = "admin";
            TempData["Id"] = -1;
            return View(await courses.ToListAsync());
        }

        public async Task<IActionResult> ShowStudentCourses(int? id)
        {
            var courseIds = from value in _context.CoursesStudents
                            where value.StudentId == id
                            select value.CourseId;

            var courses = from course in _context.Courses
                          where courseIds.Contains(course.Id)
                          select course;

            var result = courses.Include(c => c.AssessmentType)
                                .Include(c => c.Discipline)
                                .Include(c => c.Term);

            ViewData["Role"] = "student";
            TempData["Role"] = "student";
            TempData["Id"] = (int)id;
            return View("Index", await result.ToListAsync());
        }

        public async Task<IActionResult> ShowTeacherCourses(int? id)
        {
            var courseIds = from value in _context.CoursesTeachers
                            where value.TeacherId == id
                            select value.CourseId;

            var courses = from course in _context.Courses
                          where courseIds.Contains(course.Id)
                          select course;

            var result = courses.Include(c => c.AssessmentType)
                    .Include(c => c.Discipline)
                    .Include(c => c.Term);

            ViewData["Role"] = "teacher";
            TempData["Role"] = "teacher";
            TempData["Id"] = (int)id;
            return View("Index", await result.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.AssessmentType)
                .Include(c => c.Discipline)
                .Include(c => c.Term)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        public IActionResult NavigateToCourseModules(int? id)
        {
            return RedirectToAction("Index", "CourseModules", new { courseId = id, viewParam = (string)TempData["Role"] });
        }

        public IActionResult Create()
        {
            ViewData["AssessmentTypeId"] = new SelectList(_context.AssessmentTypes, "Id", "Name");
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Name");
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DisciplineId,TermId,AssessmentTypeId,LectureHours,PracticeHours,SelfDevelopmentHours,StartDate,EndDate,Info")] Course course)
        {
            if (ModelState.IsValid)
            {
                var entry = _context.Add(course);
                await _context.SaveChangesAsync();
                
                int id = (int)entry.Property("Id").CurrentValue;
                TempData["CourseId"] = id;

                return RedirectToAction("CreateCourseConnection", "Courses");
            }
            ViewData["AssessmentTypeId"] = new SelectList(_context.AssessmentTypes, "Id", "Name", course.AssessmentTypeId);
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Name", course.DisciplineId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Id", course.TermId);
            return View(course);
        }

        public IActionResult CreateCourseConnection()
        {
            var infoIds = from teacher in _context.Teachers
                             select teacher.UserInfoId;

            var infos = from info in _context.UserInfos
                        where infoIds.Contains(info.Id)
                        select info;

            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(infos.ToList(), "Id", "Name");
            ViewData["TeacherRoleId"] = new SelectList(_context.TeacherRoles, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourseConnection(NewCourseView model)
        {
            if (ModelState.IsValid)
            {
                var teacherId = from teacher in _context.Teachers
                              where teacher.UserInfoId == model.TeacherId
                              select teacher.Id;

                CoursesTeacher teacherEntry = new CoursesTeacher
                {
                    CourseId = (int)TempData["CourseId"],
                    TeacherId = teacherId.ToList()[0],
                    TeacherRoleId = model.TeacherRoleId
                };

                _context.CoursesTeachers.Add(teacherEntry);

                var students = from student in _context.Students
                               where student.StudentGroupId == model.StudentGroupId
                               select student.Id;

                foreach(int studentId in students.ToList())
                {
                    CoursesStudent studentEntry = new CoursesStudent
                    {
                        CourseId = (int)TempData["CourseId"],
                        StudentId = studentId
                    };

                    _context.CoursesStudents.Add(studentEntry);
                }

                await _context.SaveChangesAsync();

                if ((string)TempData["Role"] == "admin")
                {
                    return RedirectToAction("Index", "Courses");
                }
                else if ((string)TempData["Role"] == "teacher")
                {
                    return RedirectToAction("ShowTeacherCourses", "Courses", new { id = (int)TempData["Id"] });
                }
                else if ((string)TempData["Role"] == "teacher")
                {
                    return RedirectToAction("ShowStudentCourses", "Courses", new { id = (int)TempData["Id"] });
                }
            }

            var infoIds = from teacher in _context.Teachers
                          select teacher.UserInfoId;

            var infos = from info in _context.UserInfos
                        where infoIds.Contains(info.Id)
                        select info;

            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(infos, "Id", "Name");
            ViewData["TeacherRoleId"] = new SelectList(_context.TeacherRoles, "Id", "Name");
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["AssessmentTypeId"] = new SelectList(_context.AssessmentTypes, "Id", "Name", course.AssessmentTypeId);
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Name", course.DisciplineId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Id", course.TermId);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DisciplineId,TermId,AssessmentTypeId,LectureHours,PracticeHours,SelfDevelopmentHours,StartDate,EndDate,Info")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["AssessmentTypeId"] = new SelectList(_context.AssessmentTypes, "Id", "Name", course.AssessmentTypeId);
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Name", course.DisciplineId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Id", course.TermId);
            return View(course);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.AssessmentType)
                .Include(c => c.Discipline)
                .Include(c => c.Term)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var courseStudentEntries = from value in _context.CoursesStudents
                                   where value.CourseId == id
                                   select value;

            var courseTeacherEntries = from value in _context.CoursesTeachers
                                   where value.CourseId == id
                                   select value;

            foreach(var entry in courseStudentEntries)
            {
                _context.CoursesStudents.Remove(entry);
            }
           
            foreach(var entry in courseTeacherEntries)
            {
                _context.CoursesTeachers.Remove(entry);
            }
            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Courses");
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
