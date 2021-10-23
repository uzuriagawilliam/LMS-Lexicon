using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Lexicon.Core.Models.ViewModels
{
    public class CreateUsersViewModel
    {
        public string Id { get; set; }
        public DateTime TimeOfRegistration { get; set; }
        // ForignKey
        [Required(ErrorMessage = "Du måste ange förnamn")]
        [Display(Name = "Förnamn")]
        [MaxLength(25), MinLength(3)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Du måste ange efternamn")]
        [Display(Name = "Efternamn")]
        [MaxLength(25), MinLength(3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Du måste ange en epost adress")]
        [EmailAddress]
        [Display(Name = "Epost")]
        //[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        //ErrorMessage = "Felaktig epost adress.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Du måste ange ett lösenord")]
        //[StringLength(25, ErrorMessage = "The {0} must be at least {1} characters long.", MinimumLength = 3)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Lösenord")]
        //public string Password { get; set; }

        [Required(ErrorMessage = "Du måste välja en kurs för studenten")]
        [Display(Name = "Kursnamn")]
        public int? CourseId { get; set; }
        public string Role { get; set; }

        //public IEnumerable<SelectListItem> Courses { get; set; }
    }
}
