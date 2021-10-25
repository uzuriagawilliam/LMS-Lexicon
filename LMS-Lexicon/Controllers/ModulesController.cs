using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS_Lexicon.Core.Models.Entities;
using LMS_Lexicon.Data.Data;

namespace LMS_Lexicon.Controllers
{
    public class ModulesController : Controller
    {
        private readonly LmsDbContext _context;

        public ModulesController(LmsDbContext context)
        {
            _context = context;
        }

        // GET: Modules
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModuleClass.ToListAsync());
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.ModuleClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Modules/Create
        public IActionResult Create(int courseId)
        {
            //ViewData["CourseId"] = courseId;
            var model = new Module 
            { 
                CourseId = courseId,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date
            };
            return View(model);
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate,Description,CourseId")] Module @module)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@module);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Courses", new { id = module.CourseId, expandedModule = true });
            }
            ViewData["CourseId"] = new SelectList(_context.CourseClass, "Id", "CourseName", @module.CourseId);
            return View(@module);
        }

        // GET: Modules/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var @module = await _context.ModuleClass.FindAsync(id);
        //    if (@module == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CourseId"] = new SelectList(_context.CourseClass, "Id", "CourseName", @module.CourseId);
        //    return View(@module);
        //}
        // Post: Modules/Edit
        public async Task<IActionResult> Edit(int moduleId)
        {
            var model  = await _context.ModuleClass.FindAsync(moduleId);
            //ViewData["CourseId"] = courseId;
            //var model = new Module { CourseId = courseId };
            return View(model);
        }
        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate,Description,CourseId")] Module @module)
        {
            //if (id != @module.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@module);
                    _context.Entry(@module).Property(m => m.CourseId).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Courses", new { id = @module.CourseId, expandedModule = true });
            }
           // ViewData["CourseId"] = new SelectList(_context.CourseClass, "Id", "CourseName", @module.CourseId);
            return View(@module);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.ModuleClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @module = await _context.ModuleClass.FindAsync(id);
            _context.ModuleClass.Remove(@module);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Courses", new { id = @module.CourseId, expandedModule = true });
        }

        private bool ModuleExists(int id)
        {
            return _context.ModuleClass.Any(e => e.Id == id);
        }
    }
}
