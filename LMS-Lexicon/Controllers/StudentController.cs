using LMS_Lexicon.Core.Models.Entities;
using LMS_Lexicon.Core.Models.ViewModels;
using LMS_Lexicon.Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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
            var user = await _userManager.GetUserAsync(User);
            var currentrole = User.IsInRole("Student") ? "Student" : User.IsInRole("Teacher") ? "Teacher" : "-";

            if (User.IsInRole("Teacher"))
            {
                return RedirectToAction("Index", "Courses");
            }

            var model = new IndexViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = currentrole
            };
            return View(model);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

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
