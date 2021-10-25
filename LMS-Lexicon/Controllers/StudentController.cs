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


        //public async Task<IActionResult> Index2()
        //{
        //    return View(await db.CourseClass.ToListAsync());
        //}

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            //var course = await db.Users.Include(c => c.Course).Where(i => i.CourseId == user.CourseId).Select(n => n.Course.CourseName).FirstOrDefaultAsync();
            var currentrole = User.IsInRole("Student") ? "Student" : User.IsInRole("Teacher") ? "Teacher" : "-";
            var coursename = await db.CourseClass.Where(i => i.Id == user.CourseId).Select(n => n.CourseName).FirstOrDefaultAsync();
            var coursedescription = await db.CourseClass.Where(i => i.Id == user.CourseId).Select(d => d.Description).FirstOrDefaultAsync();
            var coursestartdate = await db.CourseClass.Where(i => i.Id == user.CourseId).Select(d => d.StartDate).FirstOrDefaultAsync();

            if (User.IsInRole("Teacher"))
            {
                return RedirectToAction("Index", "Courses");
            }
            //var studentDetails = new StudentDetailsViewModel
            //    (
            //    f = user.FirstName,
            //    LastName = user.LastName

            //    );
            //return View(studentDetails);


            var model = new IndexViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                TimeOfRegistration = user.TimeOfRegistration,
                Role = currentrole,
                CourseName = coursename,
                CourseDescription = coursedescription,
                CourseStartDate = coursestartdate
            };
            return View(model);

        }
        // GET: StudentController/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    //var @module = await context.ModuleClass
        //    //    .FirstOrDefaultAsync(m => m.Id == id);
        //    //if (@module == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //return View(@module);
        //}

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: StudentController/Create
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
