using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_.Api.Core.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        //public int LiteraturId { get; set; }

        //public Literature literature;

        public ICollection<Literature> Literatures { get; set; }
    }
}
