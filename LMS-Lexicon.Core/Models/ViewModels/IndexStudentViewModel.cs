using LMS_Lexicon.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Lexicon.Core.Models.ViewModels
{
    public class IndexStudentViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime TimeOfRegistration { get; set; }
        public string Role { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public string CourseDescription { get; set; }

        public DateTime CourseStartDate { get; set; }
        public string FullName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public List<ApplicationUser> UsersList { get; set; }
        public ICollection<Module> Modules { get; set; }


    }
}
