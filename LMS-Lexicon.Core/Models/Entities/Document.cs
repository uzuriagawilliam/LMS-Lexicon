using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Core.Models.Entities
{
    public class Document
    {
        public int Id { get; set; }
        [MaxLength(25), MinLength(3)]
        [Display(Name = "Dokumentnamn")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Tidsstämpel")]
        public DateTime TimeStamp { get; set; }
        //Forignkeys
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }
        public int? CourseId { get; set; }
        public string ApplicationUserId { get; set; }

        //Navigation property
        public ApplicationUser ApplicationUser { get; set; }
        public Module Module { get; set; }
        public Course Course { get; set; }
        public Activity Activity { get; set; }


    }
}
