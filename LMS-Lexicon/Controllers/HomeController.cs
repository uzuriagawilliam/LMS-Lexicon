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

namespace LMS_Lexicon.Controllers
{
    public class HomeController : Controller
    {
        private readonly LmsDbContext db;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, LmsDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            db = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
                var user = await _userManager.GetUserAsync(User);
                var currentrole = User.IsInRole("Student") ? "Student" : User.IsInRole("Teacher") ? "Teacher" : "-";

                var model = new IndexViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = currentrole
                };
                return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> CreateStudentAsync(string firstname, string lastname, string email, string password, int courseid)
        {
           var user = new ApplicationUser
            {     
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                UserName = email,
                CourseId = courseid,
                TimeOfRegistration = DateTime.Now
            };
            var result = await _userManager.CreateAsync(user, password);
            var addtoroleresult = await _userManager.AddToRoleAsync(user, "Student");
            var model = new CreateStudentViewModel
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Password = password,
                CourseId = courseid,
                TimeOfRegistration = DateTime.Now
            };

        try
        {
            db.Add(user);
            await db.SaveChangesAsync();

            return View("CreateStudent", model);
        }catch(Exception ex)
        {
                throw;
        }

    }

    public IActionResult Privacy()
    {
        return View();
    }
}
}
