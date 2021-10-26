using LMS_Lexicon.Core.Models.Entities;
using LMS_Lexicon.Core.Models.ViewModels;
using LMS_Lexicon.Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace LMS_Lexicon.Controllers
{
    public class StudentController : Controller
    {
        private readonly LmsDbContext db;
        private readonly ILogger<StudentController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(ILogger<StudentController> logger, UserManager<ApplicationUser> userManager, LmsDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            db = context;

        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Teacher"))
            {
                return RedirectToAction("Index", "Courses");
            }
       

            bool expandedModule = false;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var currentrole = User.IsInRole("Student") ? "Student" : User.IsInRole("Teacher") ? "Teacher" : "-";


            var course = await db.CourseClass
            .Include(c => c.Modules)
            .FirstOrDefaultAsync(m => m.Id == user.CourseId);

            var usersincourse = await db.Users
              .Include(c => c.Course)
              .Where(i => i.CourseId == course.Id)
              .ToListAsync();

            if (course == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Student"))
            {
                return RedirectToAction("Details", "Courses", new { id=course.Id });
            }

            ViewBag.ShowModule = expandedModule;

            var model = new IndexStudentViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,

                TimeOfRegistration = user.TimeOfRegistration,
                Role = currentrole,
                CourseName = course.CourseName,
                CourseDescription = course.Description,
                CourseStartDate = course.StartDate,
                Modules = course.Modules,
                UsersList = usersincourse
            };
            return View(model);

        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
