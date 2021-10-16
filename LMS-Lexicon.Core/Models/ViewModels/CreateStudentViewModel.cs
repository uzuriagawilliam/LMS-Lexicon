﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS_Lexicon.Core.Models.ViewModels
{
    public class CreateStudentViewModel
    {
        public string Id { get; set; }
        public DateTime TimeOfRegistration { get; set; }
        // ForignKey
        [Display(Name = "Förnamn")]
        [MaxLength(25), MinLength(3)]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        [MaxLength(25), MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Epost")]
        //[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        //ErrorMessage = "Felaktig epost adress.")]
        public string Email { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }


        [Display(Name = "Kursnamn")]
        public int? CourseId { get; set; }  //ToDo Fix!!!!!! remove ?

        //public IEnumerable<SelectListItem> Courses { get; set; }
    }
}
