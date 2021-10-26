using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_.Api.Core.Entities
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }

        public ICollection<Literature> Literatures { get; set; }
    }
}
