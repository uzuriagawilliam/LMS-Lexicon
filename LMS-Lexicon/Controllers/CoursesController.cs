using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS_Lexicon.Data.Data;
using LMS_Lexicon.Core.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using LMS_Lexicon.Core.Models.ViewModels;

namespace LMS_Lexicon.Controllers
{
    public class CoursesController : Controller
    {
        private readonly LmsDbContext db;

        public CoursesController(LmsDbContext context)
        {
           db = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await db.CourseClass.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id, bool expandedModule = true)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.CourseClass
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(m => m.Id == id);


            var modules = await db.ModuleClass
                .Include(a => a.Activities)
                .Where(m => m.CourseId == id)
                .ToListAsync();

            var usersincourse = await db.Users
                .Include(c => c.Course)
                .Where(i => i.CourseId == course.Id)
                .ToListAsync();

            var activities = modules.Select(a => a.Activities).ToList();

            if (course == null)
            {
                return NotFound();
            }
            if (modules == null)
            {
                return NotFound();
            }
            if (activities == null)
            {
                return NotFound();
            }
            if (usersincourse == null)
            {
                return NotFound();
            }

            ViewBag.ShowModule = expandedModule;
            var model = new ModulesDetailsViewModel
            {
                Id = course.Id,
                CourseName = course.CourseName,
                Description = course.Description,
                StartDate = course.StartDate,
                Modules = modules,
                Activities = activities,
                UsersList = usersincourse
            };

            return View(model);
        }

        // GET: Courses/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create([Bind("Id,CourseName,Description,StartDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles ="Teacher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.CourseClass.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Teacher")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseName,Description,StartDate")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(course);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles ="Teacher")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.CourseClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize( Roles = "Teacher")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await db.CourseClass.FindAsync(id);
            db.CourseClass.Remove(course);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Create

        public IActionResult CreateModule(int courseId)
        {
            return View();
        }

        // POST: Module/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateModule(Module module)
        {
            if (ModelState.IsValid)
            {
                db.Add(module);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult EditModule(int courseId)
        {
            return View();
        }

        // POST: Module/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> EditModule(Module module)
        {
            if (ModelState.IsValid)
            {
                db.Add(module);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        private bool CourseExists(int id)
        {
            return db.CourseClass.Any(e => e.Id == id);
        }
    }
}
