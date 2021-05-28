using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentGrades.Models;
using StudentGrades.ViewModels;

namespace StudentGrades.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBStudentGradesContext _context;

        public HomeController(DBStudentGradesContext context)
        {
            _context = context;
        }

        //bottle neck, opens landing student page or if studentId is invalid - go to login/register
        public IActionResult Index(int? studentId)
        {
            ViewData["StudentId"] = studentId;
            if (studentId == null || studentId == -1)
            {
                return View();
            }

            Student student = _context.Students.Find(studentId);
            if (student == null)
            {
                return View();
            }

            UserInfo userInfo = _context.UserInfos.Find(student.UserInfoId);
            ViewData["Code"] = student.UserInfoId;
            ViewData["Login"] = userInfo.Login;
            ViewData["Email"] = userInfo.Email;
            
            return View();
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
