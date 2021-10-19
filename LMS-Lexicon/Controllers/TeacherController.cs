using LMS_Lexicon.Core.Models.Entities;
using LMS_Lexicon.Core.Models.ViewModels;
using LMS_Lexicon.Data.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Controllers
{
    public class TeacherController : Controller
    {
        private readonly LmsDbContext db;
        private readonly ILogger<TeacherController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherController(ILogger<TeacherController> logger, UserManager<ApplicationUser> userManager, LmsDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            db = context;
        }
        // GET: TeacherController
        public async Task<IActionResult> Index()
        {
            var studentsList = new List<IndexStudentsViewModel>();
            var students = await db.Users
                .Include(c => c.Course).OrderBy(u => u.FirstName).ToListAsync();

            foreach (var student in students)
            {

                var user = await db.Users.Where(x => x.Id == student.Id).FirstOrDefaultAsync();
                var role = await _userManager.GetRolesAsync(user);

                if (role.Single() == "Student")
                {
                    studentsList.Add(new IndexStudentsViewModel
                    {
                        Id = student.Id,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Email = student.Email,
                        CourseName = student.Course.CourseName
                    });
                }
            }
            return View(studentsList);
        }


        // GET: TeacherController/Details/5
        public async Task<IActionResult> DetailsStudent(string id)
        {
            var student = await db.Users
            .Include(c => c.Course).Where(u => u.Id == id).FirstOrDefaultAsync();

            var model = new StudentDetailsViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                CourseName = student.Course.CourseName,
                Description = student.Course.Description,
                StartDate = student.Course.StartDate
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            return PartialView("CreateStudentPartial");
        }
        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateStudent(CreateStudentViewModel vm)
        {
            bool userIdExist = false;
            if (ModelState.IsValid)
            {
                userIdExist = db.Users.Any(u => u.UserName == vm.Email);

                if (userIdExist)
                {
                    TempData["StudentExists"] = "The student already exist!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var user = new ApplicationUser
                    {
                        FirstName = vm.FirstName,
                        LastName = vm.LastName,
                        Email = vm.Email,
                        UserName = vm.Email,
                        CourseId = vm.CourseId,
                        TimeOfRegistration = DateTime.Now
                    };


                    try
                    {
                        var result = await _userManager.CreateAsync(user, vm.Password);
                        var addtoroleresult = await _userManager.AddToRoleAsync(user, "Student");

                        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index", db.Users.ToList()) });
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
               
            }
            userIdExist = db.Users.Any(u => u.UserName == vm.Email);

            if (userIdExist)
            {
                TempData["StudentExists"] = "The student already exist!";
               
            }
            //return RedirectToAction(nameof(Index));
            //return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateStudentPartial", vm) });
            //return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Index", db.Users.ToList()) });
            return RedirectToAction(nameof(Index));
        }


        // GET: TeacherController/Edit/5
        public ActionResult EditStudent(string id)
        {
            return View();
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        // POST: TeacherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
