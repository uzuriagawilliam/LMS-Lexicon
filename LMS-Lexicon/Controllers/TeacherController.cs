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
        private readonly RoleManager<IdentityRole> _roleManager;

        public TeacherController(ILogger<TeacherController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, LmsDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            db = context;
        }
        // GET: TeacherController
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Index(string role)
        {
            role =  string.IsNullOrEmpty(role) ? "Student" : role ;
            var users = GetUsers(role);

            var model = new IndexViewModel();
            model.RoleName = role;
            model.UserList = users.Result;

            return View(model);
        }

        private async Task<IEnumerable<IndexUsersViewModel>> GetUsers(string role)
        {
            var usersList = new List<IndexUsersViewModel>();
            var users = await db.Users
                .Include(c => c.Course).OrderBy(u => u.FirstName).ToListAsync();
          
            foreach (var u in users)
            {
                var currentuser = await db.Users.Where(x => x.Id == u.Id).FirstOrDefaultAsync();
                var userrole = await _userManager.GetRolesAsync(currentuser);

                if (userrole.Single() == role)
                {
               
                    usersList.Add(new IndexUsersViewModel
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        FullName = u.FirstName + " " + u.LastName,
                        Email = u.Email,
                        CourseName = u.Course.CourseName,
                        Role = userrole[0]
                    });
                }
            }
            return usersList;
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult GetUsersByRole(string roleid)
        {
            var model = _userManager.Users.Include(um => um.Course)
                    .Join(db.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                    .Join(db.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
                    .ToList().Where(u => u.ur.ur.RoleId == roleid)
                    .Select(r => new IndexUsersViewModel()
                    {
                        Id = r.ur.u.Id,
                        FirstName = r.ur.u.FirstName,
                        LastName = r.ur.u.LastName,
                        FullName = r.ur.u.FirstName + " " + r.ur.u.LastName,
                        Email = r.ur.u.Email,
                        CourseName = r.ur.u.Course.CourseName,
                        Role = r.r.Name
                    }).OrderBy(m => m.FirstName);

            return PartialView("UsersPartial", model);
        }
        // GET: TeacherController/Details/5
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DetailsUser(string id, string role)
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
                StartDate = student.Course.StartDate,
                Role = role
            };
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult CreateUser(string role)
        {
            var model = new CreateUsersViewModel();
            model.Role = role;
            return PartialView("CreateUserPartial", model);
        }
        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateUser(CreateUsersViewModel vm)
        {
            bool userIdExist = false;
            string defaultpassword = "PassWord";
            userIdExist = db.Users.Any(u => u.UserName == vm.Email);
            var userId = await db.Users.Where(u => u.UserName == vm.Email).SingleOrDefaultAsync();
            if (userId == null)
            {
                if (userIdExist)
                {
                    TempData["UserExists"] = "Studenten finns redan!";
                    var model = await GetUsers(vm.Role);
                    return PartialView("UsersPartial");
                }
                else
                {
                    if (ModelState.IsValid)
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
                            if(vm.Role == "Student")
                            {
                                var result = await _userManager.CreateAsync(user, defaultpassword);
                                var addtoroleresult = await _userManager.AddToRoleAsync(user, vm.Role);
                                TempData["UserSuccess"] = "Studenten " + user.FirstName + " är nu tillagd";
                            }
                            else
                            {
                                var result = await _userManager.CreateAsync(user, defaultpassword);
                                var addtoroleresult = await _userManager.AddToRoleAsync(user, vm.Role);
                                TempData["UserSuccess"] = "Läraren " + user.FirstName + " är nu tillagd";
                            }
                       
                            var model = await GetUsers(vm.Role);
                            return PartialView("UsersPartial", model);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        TempData["UserExists"] = "Ingen databaslagring gjordes!";
                    }
                    var defaultmodel = await GetUsers(vm.Role);
                    return PartialView("UsersPartial", defaultmodel);
                }
            }
            else
            {
                if (userIdExist)
                {
                    TempData["UserExists"] = "Studenten finns redan!";
                }
                var model = await GetUsers(vm.Role);
                return PartialView("UsersPartial", model);
            }
        }

        public async Task<IActionResult> EditUser(string id, string role)
        {
            if (id == "")
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            CreateUsersViewModel vm = new CreateUsersViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CourseId = user.CourseId,
                TimeOfRegistration = user.TimeOfRegistration,
                Role = role
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // GET: TeacherController/Edit/5
        public async Task<IActionResult> EditUser(string id, CreateUsersViewModel vm)
        {
            var std = await _userManager.FindByIdAsync(id);
            std.FirstName = vm.FirstName;
            std.LastName = vm.LastName;
            std.Email = vm.Email;
            std.UserName = vm.Email;
            std.CourseId = vm.CourseId;
            std.TimeOfRegistration = std.TimeOfRegistration;

            if (!Equals(std, vm))
            { 
                if(vm.Role == "Student")
                {
                    var result = await _userManager.UpdateAsync(std);
                    TempData["UserSuccess"] = "Studenten " + std.FirstName + " är nu ändrad";
                }
                else
                {
                    var result = await _userManager.UpdateAsync(std);
                    TempData["UserSuccess"] = "Läraren " + std.FirstName + " är nu ändrad";
                }
              
            }
            else
            {
                TempData["UserExists"] = "Ingen ändring gjordes för " + std.FirstName;
            }
            return RedirectToAction(nameof(Index), new { @role = vm.Role });
        }

        private bool Equals(ApplicationUser std, CreateStudentViewModel vm)
        {
            if (std.FirstName == vm.FirstName
                && std.LastName == vm.LastName
                && std.Email == vm.Email
                && std.CourseId == vm.CourseId)
                return true;
            else
                return false;
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteUser(string id, string role)
        {

            var std = await _userManager.FindByIdAsync(id);
            var student = await db.Users
            .Include(c => c.Course).Where(u => u.Id == std.Id).FirstOrDefaultAsync();

            var model = new StudentDetailsViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                CourseName = student.Course.CourseName,
                Role = role
            };
            return View(model);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteConfirmed(string id, string role)
        {
            try
            {
                var std = await _userManager.FindByIdAsync(id);
                var result = await _userManager.DeleteAsync(std);
                if (result.Succeeded)
                {
                    if (role == "Student")
                    {
                        TempData["UserSuccess"] = "Studenten " + std.FirstName + " är nu borttagen";
                    }
                    else
                    {
                        TempData["UserSuccess"] = "Läraren " + std.FirstName + " är nu borttagen";
                    }
                }
                else
                {
                    TempData["UserExists"] = "Borttagningen misslyckades ";
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }
    }
}

