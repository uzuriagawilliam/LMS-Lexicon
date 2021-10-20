using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Api.Core.Dto
{
    public class AuthorDto
    {
        public int AuthorId { get; set; }
        public string FullName { get; set; }
        //       public string FirstName { get; set; }
        //       public string LastName { get; set; }
        public int AuthorAge { get; set; }
        //       public DateTime BirthDate { get; set; }

        //public int LiteraturId { get; set; }

        //public Literature literature;

        public ICollection<LiteratureDto> LiteraturesDto { get; set; }
    }
}
