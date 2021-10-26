using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Lexicon.Core.Models.ViewModels
{
    public class ActivityViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25), MinLength(2)]
        public string Name { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
        public int ActivityTypeId { get; set; }
        public int? ActivityId { get; set; }
        public int? CourseId { get; set; }
    }
}
