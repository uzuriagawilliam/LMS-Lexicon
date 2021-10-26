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
        public IActionResult Create(int courseid, int moduleid)
        {
            var model = new CreateActivityViewModel
            {
                CourseId = courseid,
                ModuleId = moduleid,
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
                ModuleId= vm.ModuleId,
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
                return RedirectToAction("Details", "Courses", new { Id=vm.CourseId, expandedModule = true });
            }

            var model = new CreateActivityViewModel
            {
                CourseId = vm.CourseId,
                ModuleId = vm.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            return View(model);

        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? courseid, int? activityid)
        {
            if (activityid == null)
            {
                return NotFound();
            }

            var activity = await _context.ActivityClass.FindAsync(activityid);
            if (activity == null)
            {
                return NotFound();
            }
            var model = new ActivityViewModel
            {
                CourseId = courseid,
                Id = activity.Id,
                ModuleId = activity.ModuleId,
                Name = activity.Name,
                StartDate = activity.StartDate,
                EndDate = activity.EndDate,
                Description = activity.Description,
                ActivityTypeId = activity.ActivityTypeId,
            };

            return View(model);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActivityViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }
            var currentactivity = await _context.ActivityClass.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

            var activity = new Activity
            {
                Id = currentactivity.Id,
                Name = vm.Name,
                Description = vm.Description,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                ModuleId = currentactivity.ModuleId,
                ActivityTypeId = vm.ActivityTypeId
            };
            if(!Equals(currentactivity, vm))
            {
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
                    return RedirectToAction("Details", "Courses", new { Id = vm.CourseId, expandedModule = true });
                }
            }
    
            var model = new ActivityViewModel
            {
                CourseId = vm.CourseId,
                Id = vm.Id,
                Name = vm.Name,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                Description = vm.Description,
                ActivityTypeId = vm.ActivityTypeId,
            };
            TempData["ErrorOnSave"] = "Ingen ändring utförd, ändra något och försök igen";
            return View(model);
        }

        private bool Equals(Activity act, ActivityViewModel vm)
        {
            if (act.Name == vm.Name
                && act.StartDate == vm.StartDate
                && act.EndDate == vm.EndDate
                && act.Description == vm.Description
                && act.ActivityTypeId == vm.ActivityTypeId)
                return true;
            else
                return false;
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? courseid,  int? activityid)
        {

            if (activityid == null)
            {
                return NotFound();
            }

            var activity = await _context.ActivityClass
                //.Include(a => a.ActivityType)
                //.Include(a => a.Module)
                .FirstOrDefaultAsync(m => m.Id == activityid);
            if (activityid == null)
            {
                return NotFound();
            }

            var model = new ActivityViewModel
            {
                Id = activity.Id,
                CourseId = courseid,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            return View(model);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int courseid)
        {
            var activity = await _context.ActivityClass.FindAsync(id);
            _context.ActivityClass.Remove(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Courses", new { Id = courseid });
        }

        private bool ActivityExists(int id)
        {
            return _context.ActivityClass.Any(e => e.Id == id);
        }
    }
}
