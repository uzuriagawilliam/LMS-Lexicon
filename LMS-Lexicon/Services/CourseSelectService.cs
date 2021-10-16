using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS_Lexicon.Data.Data;
using System.Linq;

namespace LMS_Lexicon.Services
{
    public class CourseSelectService : ICourseSelectService
    {
        private readonly LmsDbContext db;
        public CourseSelectService(LmsDbContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetCoursesAsync()
        {
            var courseList = await db.CourseClass
                .Select(c => c)
                .Distinct()
                    .Select(course => new SelectListItem()
                    {
                        Text = course.CourseName,
                        Value = course.Id.ToString(),
                    }).ToListAsync();
            return courseList;
        }
    }
}
