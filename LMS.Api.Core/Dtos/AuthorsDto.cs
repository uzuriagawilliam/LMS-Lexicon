using LMS.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Api.Core.Dtos
{
    public class AuthorsDto
    {
       //public int AuthorId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

         public ICollection<Literature> Literatures { get; set; }
    }
}
