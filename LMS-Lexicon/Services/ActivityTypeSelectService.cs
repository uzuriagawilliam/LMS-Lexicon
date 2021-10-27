using LMS_Lexicon.Data.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Services
{
    public class ActivityTypeSelectService : IActivityTypeSelectService
    {
        private readonly LmsDbContext db;
        public ActivityTypeSelectService(LmsDbContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<SelectListItem>> GetActivityTypeAsync()
        {
            var activitytype = await db.ActivityClass
                .Include(t => t.ActivityType)
                .Select(a => a.ActivityType)
                .Distinct()
                .Select(activitytype => new SelectListItem()
                {
                    Text = activitytype.Name,
                    Value = activitytype.Id.ToString()
                }).ToArrayAsync();
            return activitytype;
        }

    }
}
