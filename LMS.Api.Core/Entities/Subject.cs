﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Api.Core.Entities
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }

        public ICollection<Literature> Literatures { get; set; }
    }
}
