using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Api.Core.Entities
{
    public class Literature
    {
        //       public int LiteratureId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }

        public int SubjectId { get; set; }

        public int Level { get; set; }
        //public int AutorId { get; set; }

        //public Author author;

        public ICollection<Author> Authors { get; set; }
        public Subject Subject { get; set; }
    }
}
