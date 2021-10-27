using LMS_Lexicon.Core.Models.Entities;
using LMS_Lexicon.Data.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace LMS_Lexicon.Services
{
    public class CourseIdForUserService : ICourseIdForUserService
    {
        private readonly LmsDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CourseIdForUserService(LmsDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            db = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int?> GetCoursesIdForUserAsync()
        {
            string userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var courseid = _userManager.FindByNameAsync(userName).Result.CourseId;

            return courseid;
        }
    }
}
