using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS_Lexicon.Core.Models.Entities;
using LMS_Lexicon.Data.Data;
using LMS_Lexicon.Core.Models.ViewModels;

namespace LMS_Lexicon.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly LmsDbContext _context;

        public ActivitiesController(LmsDbContext context)
        {
            _context = context;
        }

        // GET: Activities
        public async Task<IActionResult> Index()
        {
            var lmsDbContext = _context.ActivityClass.Include(a => a.ActivityType).Include(a => a.Module);
            return View(await lmsDbContext.ToListAsync());
        }


        // GET: Activities/Create
        public IActionResult Create(int courseId, int moduleId)
        {
            var model = new CreateActivityViewModel
            {
                CourseId = courseId,
                ModuleId = moduleId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            return View(model);
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateActivityViewModel vm)
        {
            var activity = new Activity
            {
                ModuleId = vm.ModuleId,
                Name = vm.Name,
                Description = vm.Description,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                ActivityTypeId = vm.ActivityTypeId
            };

            if (ModelState.IsValid)
            {
                _context.Add(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Courses", new { Id=vm.CourseId });
            }

            var model = new CreateActivityViewModel
            {
                CourseId = vm.CourseId,
                ModuleId = vm.ModuleId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            return View(model);

        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.ActivityClass.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Name", activity.ActivityTypeId);
            ViewData["ModuleId"] = new SelectList(_context.ModuleClass, "Id", "Name", activity.ModuleId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate,Description,ActivityTypeId,ModuleId")] Activity activity)
        {
            if (id != activity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(activity.Id))
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
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Name", activity.ActivityTypeId);
            ViewData["ModuleId"] = new SelectList(_context.ModuleClass, "Id", "Name", activity.ModuleId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.ActivityClass
                .Include(a => a.ActivityType)
                .Include(a => a.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activity = await _context.ActivityClass.FindAsync(id);
            _context.ActivityClass.Remove(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityExists(int id)
        {
            return _context.ActivityClass.Any(e => e.Id == id);
        }
    }
}
