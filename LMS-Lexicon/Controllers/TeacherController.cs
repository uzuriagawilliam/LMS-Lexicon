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
                .Include(c => c.Course).ToListAsync();

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
        public async Task<IActionResult> CreateStudent()
        {
            var currentId = db.Users.OrderBy(u => u.Id).Select(u => u.Id).LastOrDefault();
            var user = new CreateStudentViewModel
            {
                Id = currentId + 1
            };
            return View(user);
        }
        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateStudent(CreateStudentViewModel vm)
        {
            var userId = await db.Users.Where(u => u.UserName == vm.Email).SingleOrDefaultAsync();
            if (userId.Email != vm.Email || userId == null)
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

                    return RedirectToAction(nameof(Index), new { id = user.Id });
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                TempData["StudentExists"] = "The student already exist!";
                return RedirectToAction(nameof(Index));
            }
        }
        public async Task<IActionResult> EditStudent(string id)
        {
            if (id == "")
            {
                return NotFound();
            }
            var student = await _userManager.FindByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            CreateStudentViewModel vm = new CreateStudentViewModel
            {
                //Id = id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                CourseId = student.CourseId,
                TimeOfRegistration = student.TimeOfRegistration
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // GET: TeacherController/Edit/5
        public async Task<IActionResult> EditStudent(string id, CreateStudentViewModel vm)
        {


            var std = await _userManager.FindByIdAsync(id);

            //ApplicationUser student = new ApplicationUser();

            //Id = id,

            std.FirstName = vm.FirstName;
            std.LastName = vm.LastName;
            std.Email = vm.Email;
            std.UserName = vm.Email;
            std.CourseId = vm.CourseId;
            std.TimeOfRegistration = std.TimeOfRegistration;

            var result = await _userManager.UpdateAsync(std);
         
            TempData["StudentExists"] = "User [" + std.FirstName + "] edited";

            return RedirectToAction(nameof(Index));
    

        }

        public async Task<IActionResult> DeleteStudent(string id)
        {


            var std = await _userManager.FindByIdAsync(id);

            var result = await _userManager.DeleteAsync(std);
            TempData["StudentExists"] = "Delete " + result.ToString();
            return RedirectToAction(nameof(Index));


        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]


        // POST: TeacherController/Edit/5




        //public async Task<IActionResult> EditStudent(String id, [Bind("Id,FirstName,LastName, Email, UserName,CourseId, TimeOfRegistration ")] Index collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View(User);
        //    }
        //}

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
