using LMS_Lexicon.Core.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Core.Models.Entities
{
    public class ApplicationUser: IdentityUser
    {
        [MaxLength(25)]
        [MinLength(3)]
        public string FirstName { get; set; }
        [MaxLength(25)]
        [MinLength(3)]
        public string LastName { get; set; }
        public DateTime TimeOfRegistration { get; set; }
        // ForignKey
        public int CourseId { get; set; }  //ToDo Fix!!!!!! remove ?
        //Nav Properties
        //public ICollection<Document> Documents { get; set; }
        public Course Course { get; set; }
    }
}
