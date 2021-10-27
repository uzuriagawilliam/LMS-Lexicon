using LMS_Lexicon.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Lexicon.Core.Models.ViewModels
{
    public class ModulesDetailsViewModel
    {
        public int Id { get; set; }
        [MaxLength(25)]
        [MinLength(3)]
        [Required]
        [Display(Name = "Kursnamn")]
        public string CourseName { get; set; }
        [MaxLength(45)]
        [MinLength(3)]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Startdatum")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        public List<Activity> Activities { get; set; }
        public List<Module> Modules { get; set; }
        public List<ApplicationUser> UsersList { get; set; }
    }
}
