using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using StudentGrades;
using StudentGrades.Models;
using StudentGrades.ViewModels;

namespace StudentGrades.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DBStudentGradesContext _context;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            DBStudentGradesContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                UserInfo userInfo = _context.UserInfos.FirstOrDefault(info => info.Login == model.Login &&
                                                                     info.Password == model.Password);
                if (userInfo == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                Student student = _context.Students.FirstOrDefault(st => st.UserInfoId == userInfo.Id);

                if (student == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                return RedirectToAction("Index", "Home", new { studentId = student.Id });
            }
            else
            {
                ModelState.AddModelError("", "Неправильний логін чи (та) пароль");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { studentId = -1 });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = new User
            {
                UserName = model.Login,
                Login = model.Login,
                Email = model.Email,
                Telephone = model.Telephone
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                UserInfo userInfo = new UserInfo
                {
                    Name = model.Name,
                    Login = model.Login,
                    Email = model.Email,
                    Password = model.Password,
                    Telephone = model.Telephone
                };

                var entry = await _context.UserInfos.AddAsync(userInfo);
                await _context.SaveChangesAsync();

                int id = (int)entry.Property("Id").CurrentValue;
                TempData["UserInfoId"] = id;

                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("RegisterStudent", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult RegisterStudent()
        {
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.StudentIds, "Id", "Code");
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Id");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent([Bind("Id,StudentId,StudentGroupId,TermId,Age,Brirthday")] Student student)
        {
            if (ModelState.IsValid)
            {
                  
                Student st = new Student
                {
                    StudentId = student.StudentId,
                    StudentGroupId = student.StudentGroupId,
                    TermId = student.TermId,
                    UserInfoId = (int)TempData["UserInfoId"],
                    Age = student.Age,
                    Brirthday = student.Brirthday
                }; 

                var entry = await _context.AddAsync(st);
                await _context.SaveChangesAsync();
                int id = (int)entry.Property("Id").CurrentValue;

                return RedirectToAction("Index", "Home", new { studentId = id });
            }

            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups, "Id", "Name", student.StudentGroupId);
            ViewData["StudentId"] = new SelectList(_context.StudentIds, "Id", "Code", student.StudentId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Id", student.TermId);
            return View(student);
        }
    }
}
