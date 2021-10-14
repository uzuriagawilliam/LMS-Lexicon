using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Lexicon.Core.Models.ViewModels
{
    public class CreateStudentViewModel
    {
  
        public DateTime TimeOfRegistration { get; set; }
        // ForignKey
     
        [MaxLength(25), MinLength(3)]
        public string FirstName { get; set; }
        [MaxLength(25), MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Felaktig emil adress.")]
        public string Email { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public int? CourseId { get; set; }  //ToDo Fix!!!!!! remove ?




    }
}
