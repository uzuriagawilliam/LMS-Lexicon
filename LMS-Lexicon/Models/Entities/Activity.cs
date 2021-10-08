using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Models.Entities
{
    public class Activity
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
        public int DocumentId { get; set; }
        public int ModuleId { get; set; }
    }
}
