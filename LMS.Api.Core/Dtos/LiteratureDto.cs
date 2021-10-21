using LMS.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Api.Dtos
{
    public class LiteratureDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }

        public int SubjectId { get; set; }

        public int Level { get; set; }

        public ICollection<Author> Authors { get; set; }

    }
}
