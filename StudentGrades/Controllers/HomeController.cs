using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

using StudentGrades.Models;
using StudentGrades.ViewModels;

namespace StudentGrades.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DBStudentGradesContext _context;

        public HomeController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DBStudentGradesContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            await RoleInitializer.InitializeAsync(_userManager, _roleManager);

            return View();
        }

        public async Task<IActionResult> Personal()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (this.User.IsInRole("admin"))
            {

                return RedirectToAction("Index", "Admin");
            }
            else if (this.User.IsInRole("teacher"))
            {
                UserInfo userInfo = _context.UserInfos.FirstOrDefault(info => info.Login == user.Login);
                Teacher teacher = _context.Teachers.FirstOrDefault(tch => tch.UserInfoId == userInfo.Id);
                return RedirectToAction("Index", "Teachers", new { id = teacher.Id });
            }
            else if (this.User.IsInRole("student"))
            {
                UserInfo userInfo = _context.UserInfos.FirstOrDefault(info => info.Login == user.Login);
                Student student = _context.Students.FirstOrDefault(st => st.UserInfoId == userInfo.Id);
                return RedirectToAction("Index", "Students", new { id = student.Id });
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RedirectToCourses()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            UserInfo userInfo = _context.UserInfos.FirstOrDefault(info => info.Login == user.Login);

            if (this.User.IsInRole("teacher"))
            {
                Teacher teacher = _context.Teachers.FirstOrDefault(tch => tch.UserInfoId == userInfo.Id);
                return RedirectToAction("ShowTeacherCourses", "Courses", new { id = teacher.Id });
            }
            else if (this.User.IsInRole("student"))
            {
                Student student = _context.Students.FirstOrDefault(st => st.UserInfoId == userInfo.Id);
                return RedirectToAction("ShowStudentCourses", "Courses", new { id = student.Id });
            }
            else if (this.User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Courses");
            }

            return NotFound();
        }

        public async Task<IActionResult> RedirectToFinals()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            UserInfo userInfo = _context.UserInfos.FirstOrDefault(info => info.Login == user.Login);

            if (this.User.IsInRole("teacher"))
            {
                Teacher teacher = _context.Teachers.FirstOrDefault(tch => tch.UserInfoId == userInfo.Id);
                return RedirectToAction("ShowTeacherFinals", "CourseFinals", new { id = teacher.Id });
            }
            else if (this.User.IsInRole("student"))
            {
                Student student = _context.Students.FirstOrDefault(st => st.UserInfoId == userInfo.Id);
                return RedirectToAction("ShowStudentFinals", "CourseFinals", new { id = student.Id });
            }
            else if (this.User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "CourseFinals");
            }

            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
