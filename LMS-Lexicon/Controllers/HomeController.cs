using LMS_Lexicon.Core.Models;
using LMS_Lexicon.Core.Models.Entities;
using LMS_Lexicon.Core.Models.ViewModels;
using LMS_Lexicon.Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LMS_Lexicon.Areas.Identity.Pages.Account;
using System.Security.Claims;

namespace LMS_Lexicon.Controllers
{
    public class HomeController : Controller
    {
        private readonly LmsDbContext db;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ClaimsPrincipal claimsPrincipal;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, LmsDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            db = context;
            //this.claimsPrincipal = claimsPrincipal;
        }

        public async Task<IActionResult> Index()
        {
            var user =await _userManager.GetUserAsync(User);
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
        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentViewModel vm)
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var username = currentuser.Email;
            if(vm.Email != username)
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
                var result = await _userManager.CreateAsync(user, vm.Password);
                var addtoroleresult = await _userManager.AddToRoleAsync(user, "Student");

                try
                {
                    db.Add(user);
                    await db.SaveChangesAsync();

                    return View(nameof(StudentDetails), new { id = user.Id });
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                //new { id = currentuser.Id }
                TempData["StudentExists"] = "The student already exist!";
                return RedirectToAction(nameof(Index));
            }


    }

        private object StudentDetails()
        {
            throw new NotImplementedException();
        }

        public IActionResult Privacy()
    {
        return View();
    }
}
}
