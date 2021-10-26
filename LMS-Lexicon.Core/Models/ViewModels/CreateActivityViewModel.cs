using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Lexicon.Core.Models.ViewModels
{
    public class CreateActivityViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25), MinLength(2)]
        [Display(Name = "Aktivitetsnamn")]
        public string Name { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Slutdatum")]
        public DateTime EndDate { get; set; }

        [MaxLength(300)]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Aktivitetstyp")]
        public int ActivityTypeId { get; set; }
        public int ModuleId { get; set; }
        public int? CourseId { get; set; }
    }
}
