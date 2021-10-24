using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS_Lexicon.Core.Models.Entities
{
    public class Module
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25), MinLength(2)]
        public string Name { get; set; }
        [Required]
        //[DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Required]
        //[DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
       
        [MaxLength(300)]
        public string Description { get; set; }
        //Forign Key
        public int CourseId { get; set; }
        //NAV Properties
        public ICollection<Document> Documents { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public Course Course { get; set; }
    }
}
