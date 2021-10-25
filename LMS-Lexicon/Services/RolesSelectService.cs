using LMS_Lexicon.Core.Models.Entities;
using LMS_Lexicon.Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Services
{
    public class RolesSelectService : IRolesSelectService
    {
        private readonly LmsDbContext db;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesSelectService(LmsDbContext context, RoleManager<IdentityRole> roleManager)
        {
            db = context;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<SelectListItem>> GetUserRolessAsync()
        {
            var rolesList = await _roleManager.Roles?
                .Select(r => r)
                .Distinct()
                    .Select(role => new SelectListItem()
                    {
                        Text = role.Name,
                        Value = role.Id.ToString(),
                    }).ToListAsync();

            return rolesList;
        }
    }
}
