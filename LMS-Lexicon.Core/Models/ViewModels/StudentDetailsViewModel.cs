using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Lexicon.Core.Models.ViewModels
{
    public class StudentDetailsViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Display(Name = "Epost")]
        //[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        //ErrorMessage = "Felaktig epost adress.")]
        public string Email { get; set; }

        [Display(Name = "Kursnamn")]
        public string CourseName { get; set; }

        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }
        public string Role { get; set; }
    }
}
